using Doctor.Forms;
using Doctor.Model;
using Doctor.Properties;
using Doctor.UI.Forms;
using Doctor.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctor.Panels
{
    public partial class SelfCheckForm : Form
    {
        private delegate void FlushClient(Cursor cursor);
        private delegate void FlushDataRowIndex();
        private delegate void FlushDataGridView(List<ExRecordModel> list);
        Dictionary<long, SelfCheckDetailForm> subForms = new Dictionary<long, SelfCheckDetailForm>();
        private int curIdx;

        /// <summary>
        /// 窗体：自检列表
        /// </summary>
        public SelfCheckForm()
        {
            InitializeComponent();
        }

        private void ChangeFormCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
            {
                FlushClient client = new FlushClient(ChangeFormCursor);
                this.Invoke(client, cursor);
            }
            else
            {
                this.Cursor = cursor;
            }
        }

        private void BindDataGridView(List<ExRecordModel> list)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                FlushDataGridView client = new FlushDataGridView(BindDataGridView);

                try
                {
                    this.Invoke(client, list);
                }
                catch (ObjectDisposedException)
                {
                    
                }
            }
            else
            {
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = list;
                UpdateSeverity();
                
            }
        }

        private void UpdateSeverity()
        {
            List<ExRecordModel> list = dataGridView1.DataSource as List<ExRecordModel>;
            for (int i = 0; i < list.Count; i++)
            {
                switch (list[i].Degree)
                {
                    case Severity.SeverityEnum.Normal:
                        dataGridView1.Rows[i].Cells["col_image"].Value = Resources.normal;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(96, 102, 30);
                        break;
                    case Severity.SeverityEnum.Light:
                        dataGridView1.Rows[i].Cells["col_image"].Value = Resources.light;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(218, 221, 30);
                        break;
                    case Severity.SeverityEnum.Medium:
                        dataGridView1.Rows[i].Cells["col_image"].Value = Resources.medium;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(176, 102, 30);
                        break;
                    case Severity.SeverityEnum.Severe:
                        dataGridView1.Rows[i].Cells["col_image"].Value = Resources.severe;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(186, 27, 10);
                        break;
                    default:
                        dataGridView1.Rows[i].Cells["col_image"].Value = Resources.unanalyzed;
                        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.FromArgb(245, 252, 255);
                        break;
                }
                dataGridView1.Rows[i].Cells["DegreeStr"].Value = ResourceCulture.GetString(dataGridView1.Rows[i].Cells["Degree"].Value.ToString().ToLower());
            }
        }

        public void RefreshSelfChecks()
        {
            new Thread((a) =>
            {
                ChangeFormCursor(Cursors.WaitCursor);
                //查看本地是否存在缓存，如果缓存文件上次写入时间超过5分钟则重新获取
                string selfCheck = null;
                if (File.Exists(GeneralHelper.SelfCheckCache))
                {
                    string fileName = GeneralHelper.SelfCheckCache;
                    DateTime timeLastModified = File.GetCreationTime(fileName);
                    TimeSpan span = DateTime.Now - timeLastModified;
                    if (span.Minutes >= 5)
                    {
                        selfCheck = HttpHelper.ConnectionForResult("SelfCheckHandler.ashx", "ListAll");
                        if (!string.IsNullOrEmpty(selfCheck))
                        {
                            using (FileStream stream = new FileStream(GeneralHelper.SelfCheckCache, FileMode.OpenOrCreate))
                            {
                                byte[] bytes = Encoding.UTF8.GetBytes(selfCheck);
                                stream.Write(bytes, 0, bytes.Length);
                            }
                        }
                    }
                    else
                    {
                        using (FileStream stream = new FileStream(fileName, FileMode.Open))
                        {
                            selfCheck = stream.ToUTF8String();
                        }
                    }
                }
                else
                {
                    selfCheck = HttpHelper.ConnectionForResult("SelfCheckHandler.ashx", "ListAll");
                    using (FileStream stream = new FileStream(GeneralHelper.SelfCheckCache, FileMode.OpenOrCreate))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(selfCheck);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }

                if (!string.IsNullOrEmpty(selfCheck))
                {
                    JObject jObjResult = JObject.Parse(selfCheck);
                    int count = (int)jObjResult["count"];
                    if (count != 0)
                    {
                        List<ExRecordModel> list = new List<ExRecordModel>();

                        JArray jlist = JArray.Parse(jObjResult["content"].ToString());
                        for (int i = 0; i < jlist.Count; ++i)
                        {
                            ExRecordModel record = JsonConvert.DeserializeObject<ExRecordModel>(jlist[i].ToString());
                            record.Area = GeneralHelper.GetAreaName(record.Citycode, ResourceCulture.GetCurrentCultureName());
                            record.Degree = Severity.Group(record.Score);
                            list.Add(record);
                        }

                        //JArray str_content = (JArray)jObjResult.Property("content");
                        // JArray ja = (JArray)JsonConvert.DeserializeObject(str_content);

                        list.Sort(SeverityDecComparison);

                        //首次刷新curIdx为-1
                        curIdx = dataGridView1.FirstDisplayedScrollingRowIndex;

                        BindDataGridView(list);

                        if (curIdx >= 0)
                        {
                            try
                            {
                                this.Invoke(new FlushDataRowIndex(() =>
                                {
                                    dataGridView1.FirstDisplayedScrollingRowIndex = curIdx;
                                }));
                            }
                            catch (ObjectDisposedException)
                            {

                            }
                        }
                    }
                }
                ChangeFormCursor(Cursors.Default);
            }).Start();
        }

        /// <summary>
        /// 窗体加载时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelfCheckForm_Load(object sender, EventArgs e)
        {
            InitLanguage();

            ResourceCulture.LanguageChanged += ResourceCulture_LanguageChanged;

            RefreshSelfChecks();
        }

        void ResourceCulture_LanguageChanged(EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            dataGridView1.Columns["DegreeStr"].HeaderText = ResourceCulture.GetString("degree");
            dataGridView1.Columns["Score"].HeaderText = ResourceCulture.GetString("score");
            dataGridView1.Columns["Area"].HeaderText = ResourceCulture.GetString("location");
            dataGridView1.Columns["Time"].HeaderText = ResourceCulture.GetString("upload_time");
            dataGridView1.Columns["Username"].HeaderText = ResourceCulture.GetString("username");
            dataGridView1.Columns["Description"].HeaderText = ResourceCulture.GetString("description");

            menuItem_viewHistory.Text = ResourceCulture.GetString("btn_view_history");

            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["DegreeStr"].Value = ResourceCulture.GetString(dataGridView1.Rows[i].Cells["Degree"].Value.ToString().ToLower());
                    dataGridView1.Rows[i].Cells["Area"].Value = GeneralHelper.GetAreaName(((ExRecordModel)dataGridView1.Rows[i].DataBoundItem).Citycode, ResourceCulture.GetCurrentCultureName());
                }
            }
        }

        private class ExRecordComparer : IComparer<ExRecordModel>
        {
            private int? local;
            private bool localFirst;

            public ExRecordComparer(string localId, bool localFirst = true)
            {
                this.localFirst = localFirst;
                int temp;
                if (int.TryParse(localId, out temp))
                {
                    local = temp;
                }
                else
                {
                    local = null;
                }
            }

            public int Compare(ExRecordModel x, ExRecordModel y)
            {
                if (null == local)
                {
                    return 0;
                }

                ExRecordModel r1 = x as ExRecordModel;
                ExRecordModel r2 = y as ExRecordModel;

                int diff = Math.Abs((int)local - int.Parse(r1.Citycode)) - Math.Abs((int)local - int.Parse(r2.Citycode));
                return (this.localFirst ? diff : -diff);
            }
        }

        /// <summary>
        /// 自检记录的扩展类
        /// </summary>
        private class ExRecordModel : RecordModel
        {
            public string Area { get; set; }
            public string Username { get; set; }
            public Severity.SeverityEnum Degree { get; set; }
        }

        /// <summary>
        /// 双击事件：点击某一自检项查看详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0)
            {
                return;
            }

            DataGridViewRow dgr = this.dataGridView1.Rows[index];
            if (null != dgr)
            {
                RecordModel record = dgr.DataBoundItem as RecordModel;
                if (subForms.ContainsKey(record.Record_id))
                {
                    SelfCheckDetailForm f;
                    if (subForms.TryGetValue(record.Record_id, out f))
                    {
                        f.BringToFront();
                    }
                }
                else
                {
                    SelfCheckDetailForm form = new SelfCheckDetailForm(record);
                    subForms.Add(record.Record_id, form);
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.FormClosed += form_FormClosed;
                    form.Show();
                }
            }
        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            SelfCheckDetailForm form = sender as SelfCheckDetailForm;
            subForms.Remove(form.RecordId);
        }

        /// <summary>
        /// 点击列标题触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dataGridView1.Columns[e.ColumnIndex];
            List<ExRecordModel> list = dataGridView1.DataSource as List<ExRecordModel>;

            ClearDirGlyphsExcept(col);

            //根据不同的列标题选择不同的排序方式
            string name = col.DataPropertyName;
            switch (name)
            {
                case "Time":
                    if (col.HeaderCell.SortGlyphDirection == SortOrder.Ascending || col.HeaderCell.SortGlyphDirection == SortOrder.None)
                    {
                        list.Sort(TimeDecComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        list.Sort(TimeAscComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    break;
                case "Score":
                    if (col.HeaderCell.SortGlyphDirection == SortOrder.Descending || col.HeaderCell.SortGlyphDirection == SortOrder.None)
                    {
                        list.Sort(ScoreAscComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        list.Sort(ScoreDecComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    break;
                case "DegreeStr":
                    if (col.HeaderCell.SortGlyphDirection == SortOrder.Descending || col.HeaderCell.SortGlyphDirection == SortOrder.None)
                    {
                        list.Sort(SeverityAscComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        list.Sort(SeverityDecComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    break;
                case "Username":
                    if (col.HeaderCell.SortGlyphDirection == SortOrder.Descending || col.HeaderCell.SortGlyphDirection == SortOrder.None)
                    {
                        list.Sort(UsernameAscComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        list.Sort(UsernameDecComparison);
                        col.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    break;
                case "Area":
                    string localId = LoginStatus.GetLocalId();
                    if (col.HeaderCell.SortGlyphDirection == SortOrder.Descending || col.HeaderCell.SortGlyphDirection == SortOrder.None)
                    {
                        list.Sort(new ExRecordComparer(localId, true));
                        col.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else
                    {
                        list.Sort(new ExRecordComparer(localId, false));
                        col.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    break;
                default:
                    break;
            }

            dataGridView1.Refresh();
            UpdateSeverity();
        }

        /// <summary>
        /// 清除DataGridView中所有的排序符号
        /// </summary>
        private void ClearDirGlyphsExcept(DataGridViewColumn curCol)
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (!col.Equals(curCol))
                {
                    col.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
        }

        /// <summary>
        /// 自检时间按倒序排列
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int TimeDecComparison(ExRecordModel x, ExRecordModel y)
        {
            return -x.Time.CompareTo(y.Time);
        }

        /// <summary>
        /// 自检时间按顺序排列
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int TimeAscComparison(ExRecordModel x, ExRecordModel y)
        {
            return x.Time.CompareTo(y.Time);
        }

        /// <summary>
        /// 自检用户名按字母顺序排列
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int UsernameAscComparison(ExRecordModel x, ExRecordModel y)
        {
            return x.Username.CompareTo(y.Username);
        }

        /// <summary>
        /// 自检用户名按字母降序排列
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int UsernameDecComparison(ExRecordModel x, ExRecordModel y)
        {
            return -x.Username.CompareTo(y.Username);
        }

        /// <summary>
        /// 分数从小到大排列：null排在最后
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int ScoreAscComparison(ExRecordModel x, ExRecordModel y)
        {
            if (x.Score == null)
            {
                return 1;
            }
            else if (y.Score == null)
            {
                return -1;
            }
            else
            {
                return x.Score.Value.CompareTo(y.Score.Value);
            }
        }

        /// <summary>
        /// 分数从大到小排列：null排在最后
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int ScoreDecComparison(ExRecordModel x, ExRecordModel y)
        {
            if (x.Score == null)
            {
                return 1;
            }
            else if (y.Score == null)
            {
                return -1;
            }
            else
            {
                return -x.Score.Value.CompareTo(y.Score.Value);
            }
        }

        private static int SeverityDecComparison(ExRecordModel x, ExRecordModel y)
        {
            if (x.Degree == Severity.SeverityEnum.Unanalyzed)
            {
                return 1;
            }
            else if (y.Degree == Severity.SeverityEnum.Unanalyzed)
            {
                return -1;
            }
            return -(x.Degree - y.Degree);
        }

        private static int SeverityAscComparison(ExRecordModel x, ExRecordModel y)
        {
            if (x.Degree == Severity.SeverityEnum.Unanalyzed)
            {
                return 1;
            }
            else if (y.Degree == Severity.SeverityEnum.Unanalyzed)
            {
                return -1;
            }
            return x.Degree - y.Degree;
        }

        private void SelfCheckForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResourceCulture.LanguageChanged -= ResourceCulture_LanguageChanged;

            var forms = subForms.Values.ToArray();
            for (int i = forms.Length - 1; i >= 0; i--)
            {
                forms[i].Close();
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "menuItem_viewHistory":
                    string patName = dataGridView1.SelectedRows[0].Cells["Username"].Value as string;
                    var form = new SelfCheckListForm(patName);
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Selected = true;
                    //dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }

        }
    }
}
