using Doctor.Forms;
using Doctor.Model;
using DoctorClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.UI.Forms
{
    public partial class SelfCheckListForm : Form
    {
        private string patientName;

        private delegate void FlushClient(Cursor cursor);

        private delegate void FlushDataGridView(List<ExRecordModel> list);

        private delegate void FlushAge(string age);

        public SelfCheckListForm(string patientName)
        {
            InitializeComponent();
            this.patientName = patientName;
            this.Text = ResourceCulture.GetString("SelfCheckListForm_text") + ": " + patientName;
        }

        private void ChangeAgeLabel(string age)
        {
            if (lbl_age.InvokeRequired)
            {
                FlushAge client = new FlushAge(ChangeAgeLabel);
                this.Invoke(client, age);
            }
            else
            {
                if (string.IsNullOrEmpty(age))
                {
                    lbl_age.Visible = false;
                }
                else
                {
                    lbl_age.Visible = true;
                    lbl_age.Text = age;
                }
            }
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
                this.Invoke(client, list);
            }
            else
            {
                this.dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.DataSource = list;
            }
        }
        

        /// <summary>
        /// 窗体加载时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelfCheckListForm_Load(object sender, EventArgs e)
        {
            //国际化
            InitLanguage();

            //显示用户名
            lbl_username.Text = patientName;

            //年龄清空
            lbl_age.Text = "";

            //加载所有与该用户相关的自检信息
            new Thread((a) =>
            {
                ChangeFormCursor(Cursors.WaitCursor);

                //获取与病人有关的信息
                JObject jObj = new JObject();
                jObj.Add("username", patientName);
                string result = HttpHelper.ConnectionForResult("PatientInfoHandler.ashx",
                    jObj.ToString());
                if (result != null && !result.Equals("null"))
                {
                    UserModel patient = JsonConvert.DeserializeObject<UserModel>(result);
                    if (patient.Date_of_birth != null)
                    {
                        TimeSpan timeSpan = DateTime.Now - (DateTime)patient.Date_of_birth;
                        int age = timeSpan.Days / 365;
                        if (ResourceCulture.GetCurrentCultureName().Equals("zh-CN"))
                        {
                            ChangeAgeLabel(age + "岁");
                        }
                        else
                        {
                            ChangeAgeLabel(age.ToString());
                        }
                    }
                    else
                    {
                        ChangeAgeLabel("");
                    }

                    string requestContent = "Patient: " + patientName;
                    string selfCheck = HttpHelper.ConnectionForResult("SelfCheckHandler.ashx", requestContent);
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
                                ExRecordModel record = new ExRecordModel(JsonConvert.DeserializeObject<RecordModel>(jlist[i].ToString()));
                                list.Add(record);
                            }

                            //JArray str_content = (JArray)jObjResult.Property("content");
                            // JArray ja = (JArray)JsonConvert.DeserializeObject(str_content);

                            BindDataGridView(list);
                        }
                    }
                    else
                    {
                        MyMessageBox.Show(ResourceCulture.GetString("network_error"));
                    }
                }

                ChangeFormCursor(Cursors.Default);
            }).Start();
        }

        private void InitLanguage()
        {
            groupBox1.Text = ResourceCulture.GetString("self_info");

            link_addContact.Text = ResourceCulture.GetString("add_contact");

            dataGridView1.Columns["Time"].HeaderText = ResourceCulture.GetString("upload_time");
            dataGridView1.Columns["Description"].HeaderText = ResourceCulture.GetString("description");
        }

        private class ExRecordModel : RecordModel
        {
            public ExRecordModel(RecordModel record)
            {
                this.Description = record.Description;
                this.Record_id = record.Record_id;
                this.Time = record.Time;
                this.User_id = record.User_id;
                this.Citycode = record.Citycode;
                this.Area = GeneralHelper.GetAreaName(this.Citycode, ResourceCulture.GetCurrentCultureName());
            }

            public string Area { get; set; }
        }

        /// <summary>
        /// 双击事件：点击某一自检项查看详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                MyMessageBox.Show(ResourceCulture.GetString("please_select_data"));
            }
        }

        /// <summary>
        /// 点击事件：添加为联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void link_addContact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MyIMClient.AddContact(patientName);
            MyMessageBox.Show(ResourceCulture.GetString("insert_complete"));
        }

        /// <summary>
        /// 列标题点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dataGridView1.Columns[e.ColumnIndex];

            if (!col.DataPropertyName.Equals("Time"))
            {
                return;
            }

            List<ExRecordModel> list = dataGridView1.DataSource as List<ExRecordModel>;
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
            dataGridView1.Refresh();
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

        private Point mPoint = new Point();

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            FlowLayoutPanel panel = sender as FlowLayoutPanel;

            mPoint.X = e.X + panel.Left;
            mPoint.Y = e.Y + panel.Top;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
           if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point newPoint = MousePosition;
                newPoint.Offset(-mPoint.X, -mPoint.Y);
                Location = newPoint;
            }
        }

        private void picBox_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picBox_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
