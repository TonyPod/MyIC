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
                        ChangeAgeLabel(age + "岁");
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
                        MessageBox.Show("网络异常");
                    }
                }

                ChangeFormCursor(Cursors.Default);
            }).Start();
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
                this.Area = GeneralHelper.GetAreaName(this.Citycode);
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
                MessageBox.Show("请选择数据后重试！");
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
            MessageBox.Show("添加成功");
        }
    }
}
