using Doctor.Model;
using Doctor.UI.Forms;
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

namespace Doctor.Forms
{
    public partial class SelfCheckDetailForm : Form
    {
        private RecordModel record;
        private long record_id;
        private delegate void SafeChangeCursorHandler(Cursor cursor);
        private delegate void SafeSetTextBoxHandler(TextBox textBox, string str);
        private delegate void SafeSetLinkLabelHandler(LinkLabel link, string str);
        private delegate void SafeChangeAccordingToIfAuthHandler();
        private delegate void FlushClient();

        public long RecordId { get { return record == null ? record_id : record.Record_id; } }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="record"></param>
        public SelfCheckDetailForm(RecordModel record)
        {
            InitializeComponent();
            this.record = record;
        }

        public SelfCheckDetailForm(long record_id)
        {
            InitializeComponent();
            this.record_id = record_id;
        }

        private void SafeChangeAccordingToIfAuth()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SafeChangeAccordingToIfAuthHandler(SafeChangeAccordingToIfAuth));
            }
            else
            {
                if (LoginStatus.UserInfo.IfAuth)
                {
                    tb_comment.ReadOnly = false;
                    lbl_ifAuth.Visible = false;
                    btn_submit.Enabled = true;
                }
                else
                {
                    tb_comment.ReadOnly = true;
                    lbl_ifAuth.Visible = true;
                    btn_submit.Enabled = false;
                }
            }
        }

        private void SafeSetTextBox(TextBox textBox, string str)
        {
            if (textBox.InvokeRequired)
            {
                this.Invoke(new SafeSetTextBoxHandler(SafeSetTextBox), textBox, str);
            }
            else
            {
                textBox.Text = str;
            }
        }

        private void SafeSetLinkLabel(LinkLabel link, string str)
        {
            if (link.InvokeRequired)
            {
                this.Invoke(new SafeSetLinkLabelHandler(SafeSetLinkLabel), link, str);
            }
            else
            {
                link.Text = str;
            }
        }

        private void SafeChangeCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SafeChangeCursorHandler(SafeChangeCursor), cursor);
            }
            else
            {
                this.Cursor = cursor;
            }
        }

        /// <summary>
        /// 窗口载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelfCheckDetailForm_Load(object sender, EventArgs e)
        {
            //国际化
            InitLanguage();

            ResourceCulture.LanguageChanged += ResourceCulture_LanguageChanged;

            //标题栏文字
            this.Text = ResourceCulture.GetString("SelfCheckDetailForm_text");

            //用户名设为空
            btn_username.Text = "";

            new Thread(() =>
            {
                SafeChangeCursor(Cursors.WaitCursor);

                SafeChangeAccordingToIfAuth();

                //如果没有拿到自检记录则取自检记录
                if (record == null)
                {
                    string recordResult = HttpHelper.ConnectionForResult("SelfCheckHandler.ashx",
                        "Record_id: " + record_id);
                    if (recordResult != null)
                    {
                        record = JsonConvert.DeserializeObject<RecordModel>(recordResult);
                    }
                    else
                    {
                        MyMessageBox.Show(ResourceCulture.GetString("network_error"));
                        return;
                    }
                }

                JObject jObjRcv = new JObject();
                jObjRcv.Add("user_id", record.User_id);

                //获取与病人有关的信息
                string result = HttpHelper.ConnectionForResult("PatientInfoHandler.ashx",
                    jObjRcv.ToString());
                if (result != null)
                {
                    UserModel patient = JsonConvert.DeserializeObject<UserModel>(result);
                    SafeSetLinkLabel(btn_username, patient.Name);
                    //if (patient.Date_of_birth != null)
                    //{
                    //    TimeSpan timeSpan = DateTime.Now - (DateTime)patient.Date_of_birth;
                    //    int age = timeSpan.Days / 365;
                    //    lbl_age.Text = age + "岁";
                    //}
                    //else
                    //{
                    //    lbl_age.Text = "";
                    //}
                }

                //获取自检图片
                string photosResult = HttpHelper.ConnectionForResult("SelfCheckPhotosHandler.ashx",
                    record.Record_id.ToString());
                if (photosResult != null)
                {
                    JObject jObj = JObject.Parse(photosResult);
                    int nbPhotos = (int)jObj["count"];

                    for (int i = 0; i < nbPhotos; i++)
                    {
                        string photoPath = jObj["content"][i].ToString();
                        if (!HttpHelper.DownloadFile("SelfCheckPhotoDownloadHandler.ashx", photoPath))
                        {
                            ////这里应该显示连接不上的图片
                            //MyMessageBox.Show("获取图片失败");
                        }
                        else
                        {
                            //因为可能关闭了窗体才执行到这里，因此先检查窗体是否已经Disposed
                            if (!this.IsDisposed)
                            {
                                try
                                {
                                    this.Invoke(new FlushClient(() =>
                                    {
                                        try
                                        {
                                            PictureBox picBox = new PictureBox();
                                            picBox.Image = Image.FromFile(Path.Combine(GeneralHelper.DownloadPicFolder,
                                                photoPath));
                                            picBox.SizeMode = PictureBoxSizeMode.Zoom;
                                            picBox.Cursor = Cursors.Hand;
                                            picBox.MouseClick += (obj, ev) => { new PicLargeForm(photoPath).ShowDialog(); };
                                            flowLayoutPanel.Controls.Add(picBox);
                                        }
                                        catch (OutOfMemoryException)
                                        {
                                            MyMessageBox.Show(ResourceCulture.GetString("file_corrupted"));
                                        }
                                    }));
                                }
                                catch (ObjectDisposedException)
                                {
                                    
                                }
                            }
                        }
                    }
                }

                //获取其他医生的历史诊断
                string prevCommentResult = HttpHelper.ConnectionForResult("DiagnosisHandler.ashx",
                    record.Record_id.ToString());
                if (prevCommentResult != null)
                {
                    JObject jObjComment = JObject.Parse(prevCommentResult);
                    int nbDiagnoses = (int)jObjComment["count"];

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < nbDiagnoses; i++)
                    {
                        var item = jObjComment["content"][i];
                        string docName = (string)item["realname"];
                        string comment = (string)item["comment"];
                        DateTime time = (DateTime)item["time"];

                        builder.AppendFormat("{0} {1}", time.ToString(), docName).AppendLine();
                        builder.AppendLine(comment);
                        builder.AppendLine();
                    }
                    builder.RemoveLine();
                    SafeSetTextBox(tb_prevComment, builder.ToString());
                }
                SafeSetTextBox(tb_description, record.Description);
                SafeChangeCursor(Cursors.Default);
            }).Start();
        }

        void ResourceCulture_LanguageChanged(EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            label1.Text = ResourceCulture.GetString("username");
            label2.Text = ResourceCulture.GetString("illness_description");
            label3.Text = ResourceCulture.GetString("self_check_photo");

            groupBox1.Text = ResourceCulture.GetString("self_check_info");
            groupBox2.Text = ResourceCulture.GetString("doc_opinion");

            btn_submit.Text = ResourceCulture.GetString("btn_submit");
            btn_cancel.Text = ResourceCulture.GetString("btn_cancel");

            lbl_ifAuth.Text = ResourceCulture.GetString("not_auth_unable_to_comment");
        }

        private void Submit()
        {
            if (!LoginStatus.UserInfo.IfAuth)
            {
                return;
            }

            //检查输入的诊断是否为空
            string comment = tb_comment.Text;
            if (string.IsNullOrEmpty(comment))
            {
                MyMessageBox.Show(ResourceCulture.GetString("please_input_your_comment"));
                tb_comment.Focus();
                return;
            }

            //组装诊断信息类实例
            DiagnosisModel model = new DiagnosisModel();
            //model.Diagnosis_id = 1;
            model.Record_id = record.Record_id;
            model.Result = comment;
            model.Time = System.DateTime.Now;
            model.Doc_id = LoginStatus.UserInfo.Doc_id;

            this.Cursor = Cursors.WaitCursor;
            string result = HttpHelper.ConnectionForResult("DiagnosisHandler.ashx", JsonConvert.SerializeObject(model));
            this.Cursor = Cursors.Default;
            if (null == result)
            {
                MyMessageBox.Show(ResourceCulture.GetString("network_error"));
                return;
            }

            JObject jObjResult = JObject.Parse(result);
            string state = (string)jObjResult.Property("state");
            if (state != "success")
            {
                MyMessageBox.Show(ResourceCulture.GetString("submit_failed"));
            }
            else
            {
                MyMessageBox.Show(ResourceCulture.GetString("submit_complete"));
                this.Close();
            }
        }

        /// <summary>
        /// 点击事件：提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_submit_Click(object sender, EventArgs e)
        {
            Submit();
        }

        /// <summary>
        /// 点击事件：取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_comment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Submit();
            }
        }

        private void btn_username_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelfCheckListForm form = new SelfCheckListForm(btn_username.Text);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
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

        private void SelfCheckDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResourceCulture.LanguageChanged -= ResourceCulture_LanguageChanged;
        }
    }
}
