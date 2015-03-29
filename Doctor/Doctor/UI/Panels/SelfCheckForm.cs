using Doctor.Forms;
using Doctor.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            }
        }
        
        public void RefreshSelfChecks()
        {
            //首次刷新curIdx为-1
            curIdx = dataGridView1.FirstDisplayedScrollingRowIndex;
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
                            record.Area = GeneralHelper.GetAreaName(record.Citycode);
                            list.Add(record);
                        }

                        //JArray str_content = (JArray)jObjResult.Property("content");
                        // JArray ja = (JArray)JsonConvert.DeserializeObject(str_content);

                        if (null != LoginStatus.UserIP)
                        {
                            string localId = LoginStatus.GetLocalId();
                            //string localId = "510100";
                            list.Sort(new ExRecordComparer(localId));
                        }

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
            RefreshSelfChecks();
        }

        private class ExRecordComparer : IComparer<ExRecordModel>
        {
            private int? local;

            public ExRecordComparer(string localId)
            {
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

                return Math.Abs((int)local - int.Parse(r1.Citycode) - Math.Abs((int)local - int.Parse(r2.Citycode)));
            }
        }

        private class ExRecordModel : RecordModel
        {
            public string Area { get; set; }
            public string Username { get; set; }
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
            if (null != dgr && !string.IsNullOrEmpty(dgr.Cells[0].Value.ToString()))
            {
                RecordModel record = dgr.DataBoundItem as RecordModel;
                //SelfCheckDetailForm.Record_id = int.Parse(dgr.Cells[0].Value.ToString());
                new SelfCheckDetailForm(record).Show();
            }
            else
            {
                MessageBox.Show("请选择数据后重试！");
            }
          
        }
    }
}
