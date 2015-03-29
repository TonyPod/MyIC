using Doctor.Model;
using Doctor.Util;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doctor.UI.Forms
{
    public partial class NewRegisterForm : Form
    {
        public NewRegisterForm()
        {
            InitializeComponent();
        }

        private int LoadForTheFirstTime;
        private string photoPath;
        private string licensePath;

        //异步检查用户名存在的线程
        private Thread thread;
        private CancellationTokenSource tokenSource;

        //跨线程修改用户名存在Label的委托
        private delegate void SafeChangeLabelVisibilityHandler(bool visibility);
        private delegate void SafeCloseHandler();
        private delegate void FlushHospitals(List<Hospital> hospitals);
        private delegate void SafeChangeCursorHandler(Cursor cursor);

        public const int MAX_HEIGHT = 768;
        public const int MAX_WIDTH = 1024;


        private void ChangeUsernameExistVisibility(bool visibility)
        {
            if (lbl_usernameExist.InvokeRequired)
            {
                SafeChangeLabelVisibilityHandler client = new SafeChangeLabelVisibilityHandler(ChangeUsernameExistVisibility);
                this.Invoke(client, visibility);
            }
            else
            {
                lbl_usernameExist.Visible = visibility;
            }
        }

        private void SafeClose()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SafeCloseHandler(SafeClose));
            }
            else
            {
                this.Close();
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
        private void BindHospitals(List<Hospital> hospitals)
        {
            if (cb_hospital.InvokeRequired)
            {
                FlushHospitals client = new FlushHospitals(BindHospitals);
                this.Invoke(client, hospitals);
            }
            else
            {
                if (hospitals.Count == 0)
                {
                    cb_hospital.Text = "";
                }
                else
                {
                    cb_hospital.DataSource = hospitals;
                    cb_hospital.DisplayMember = "name";
                    cb_hospital.ValueMember = "hospital_id";
                }
                if (LoadForTheFirstTime == 0)
                {
                    cb_hospital.Focus();
                }
                else
                {
                    tb_username.Focus();
                    LoadForTheFirstTime--;
                }
            }
        }

        private void InitLanguage()
        {
            //窗口标题栏
            this.Text = ResourceCulture.GetString("RegisterForm_text");
        }

        /// <summary>     
        /// 窗体载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewRegisterForm_Load(object sender, EventArgs e)
        {
            InitLanguage();

            //窗口启动时BindHospitals居然要执行4次（每次都会让cb_hospital获取焦点）
            LoadForTheFirstTime = 4;

            //加载省市区数据
            GeneralHelper.LoadLocationData();

            cb_province.DataSource = GeneralHelper.Provinces;
            cb_province.DisplayMember = "province";

            lbl_usernameExist.Visible = false;

            //获取焦点时删除“用户名已存在”
            tb_username.GotFocus += tb_username_GotFocus;

            tb_username.LostFocus += tb_username_LostFocus;
        }

        void tb_username_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_username.Text.Trim()))
            {
                return;
            }

            thread = new Thread(() =>
            {
                string result = null;
                JObject jObj = new JObject();
                jObj.Add("username", tb_username.Text.Trim());
                result = HttpHelper.ConnectionForResult("CheckUsernameExistHandler.ashx", jObj.ToString());
                if (result != null)
                {
                    JObject jObjResponse = JObject.Parse(result);
                    string state = (string)jObjResponse["state"];
                    ChangeUsernameExistVisibility("exist".Equals(state));
                }
            });
            thread.Start();
            //检查用户名是否存在
        }

        void tb_username_GotFocus(object sender, EventArgs e)
        {
            lbl_usernameExist.Visible = false;
        }

        private void Confirm()
        {
            //基本信息
            //1.用户名
            string username = tb_username.Text.Trim();
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("请输入用户名");
                tb_username.Focus();
                return;
            }

            //2.密码
            string password = tb_password.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码");
                tb_password.Focus();
                return;
            }

            //3.再次输入密码
            string passwordAgain = tb_passwordAgain.Text;
            if (string.IsNullOrEmpty(passwordAgain))
            {
                MessageBox.Show("请再次输入密码");
                tb_passwordAgain.Focus();
                return;
            }
            if (!password.Equals(passwordAgain))
            {
                MessageBox.Show("两次输入密码不一致");
                tb_passwordAgain.Focus();
                return;
            }

            //4.个人照片
            if (string.IsNullOrEmpty(photoPath))
            {
                MessageBox.Show("请选择个人照片");
                return;
            }

            //认证信息
            //1.真实姓名
            string realName = tb_realname.Text.Trim();
            if (string.IsNullOrEmpty(realName))
            {
                MessageBox.Show("请输入真实姓名");
                tb_realname.Focus();
                return;
            }

            //2.省市县医院选择
            if (cb_province.SelectedIndex < 0)
            {
                MessageBox.Show("请选择省份");
                cb_province.Focus();
                return;
            }

            if (cb_city.SelectedIndex < 0)
            {
                MessageBox.Show("请选择城市");
                cb_city.Focus();
                return;
            }

            if (cb_area.SelectedIndex < 0)
            {
                MessageBox.Show("请选择区域");
                cb_area.Focus();
                return;
            }

            if (cb_hospital.SelectedIndex < 0)
            {
                MessageBox.Show("请选择医院");
                cb_hospital.Focus();
                return;
            }

            //3.执业医师证编码
            string licenseNo = tb_license.Text.Trim();
            if (string.IsNullOrEmpty(licenseNo))
            {
                MessageBox.Show("请输入执业医师证编码");
                tb_license.Focus();
                return;
            }
            if (licenseNo.Length != 24 && licenseNo.Length != 27)
            {
                MessageBox.Show("执业医师证编码不合法，请重新输入");
                tb_license.Focus();
                return;
            }

            if (string.IsNullOrEmpty(licensePath))
            {
                MessageBox.Show("请选择执业医师证图片");
                return;
            }


            DoctorModel model = new DoctorModel();

            //必须信息
            model.Name = username;
            model.Password = MD5.GetMD5(tb_passwordAgain.Text);

            //非必须信息（认证信息）
            this.Cursor = Cursors.WaitCursor;

            tokenSource = new CancellationTokenSource();

            Hospital selectedHospital = cb_hospital.SelectedItem as Hospital;

            //先发送图片，保证图片上传完成再发送注册表单
            Task<string> task1 = Task.Factory.StartNew(() => 
            {
                //图片直到发送成功为止，或点击取消取消发送
                string result = null;
                while (true)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        return null;
                    }

                    result = HttpHelper.UploadFile("PicUploadHandler.ashx", photoPath);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }
            }, tokenSource.Token);

            Task<string> task2 = Task.Factory.StartNew(() => 
            {
                string result = null;
                while (true)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        return null;
                    }

                    result = HttpHelper.UploadFile("PicUploadHandler.ashx", licensePath);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                }
            }, tokenSource.Token);

            Task.Factory.StartNew(() =>
            {
                Task.WaitAll(task1, task2);
                if (task1 == null || task2 == null || tokenSource.IsCancellationRequested)
                {
                    return;
                }

                model.PhotoPath = task1.Result;
                model.LicensePath = task2.Result;

                model.RealName = tb_realname.Text.Trim();
                model.LicenseNo = tb_license.Text;

                model.Hospital_id = selectedHospital.Hospital_id;


                string result = HttpHelper.ConnectionForResult("RegisterHandler.ashx", JsonConvert.SerializeObject(model));

                SafeChangeCursor(Cursors.Default);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("网络异常");
                }
                else
                {
                    JObject jObj = JObject.Parse(result);
                    string state = (string)jObj["state"];
                    if ("username exist".Equals(state))
                    {
                        MessageBox.Show("用户名已存在");
                    }
                    else if ("failed".Equals(state))
                    {
                        MessageBox.Show("注册失败，请重新尝试！");
                    }
                    else if ("success".Equals(state))
                    {
                        MessageBox.Show("注册成功，审核成功会在第一时间予以通知！");
                        SafeClose();
                    }
                }
            }, tokenSource.Token);
        }

        /// <summary>
        /// 点击事件：确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        /// <summary>
        /// 点击事件：取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            if (tokenSource != null)
            {
                tokenSource.Cancel();
                
            }
        }

        /// <summary>
        /// 点击事件：选择相片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selectPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                photoPath = openFileDialog.FileName;
                //文件大小是否低于2M
                FileInfo fileInfo = new FileInfo(photoPath);
                if (fileInfo.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show("文件不能超过2M");
                    return;
                }

                //图片是否低于1024*768
                using (Image image = Image.FromFile(photoPath))
                {
                    if (image.Width > MAX_WIDTH || image.Height > MAX_HEIGHT)
                    {
                        MessageBox.Show("图片大小不能超过1024x768");
                        return;
                    }
                }
                
                picBox_photo.Image = Image.FromFile(photoPath);
                lbl_photo.Text = Path.GetFileName(photoPath);
                lbl_photo.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 点击事件：选择医师证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selectLicense_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                licensePath = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(licensePath);
                if (fileInfo.Length > 2 * 1024 * 1024)
                {
                    MessageBox.Show("文件不能超过2M");
                    return;
                }

                //图片是否低于1024*768
                using (Image image = Image.FromFile(licensePath))
                {
                    if (image.Width > MAX_WIDTH || image.Height > MAX_HEIGHT)
                    {
                        MessageBox.Show("图片大小不能超过1024x768");
                        return;
                    }
                }

                picBox_license.Image = Image.FromFile(licensePath);

                lbl_license.Text = licensePath;
                lbl_license.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 选择省
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_province_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hat_provinceModel province = (Hat_provinceModel)cb_province.SelectedItem;
            cb_city.DataSource = GeneralHelper.GetCitiesByProvince(province);
            cb_city.DisplayMember = "city";
        }

        /// <summary>
        /// 选择市
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_city_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hat_cityModel city = (Hat_cityModel)cb_city.SelectedItem;
            cb_area.DataSource = GeneralHelper.GetAreasByCity(city); 
            cb_area.DisplayMember = "area";
        }

        private class Hospital 
        {
            public int Hospital_id { get; set; }
            public string Name { get; set; }
        }

        private void cb_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cb_area.Text))
            {
                Hat_areaModel area = cb_area.SelectedItem as Hat_areaModel;
                int area_id = area.Id;
                //string hospitalList = HttpHelper.ConnectionForResult("HospitalListHandler.ashx", 2262 + "");
                
                //异步操作
                //清除原来的医院列表
                new Thread(() =>
                {
                    string hospitalList = HttpHelper.ConnectionForResult("HospitalListHandler.ashx", area_id.ToString()); 
                    if (!string.IsNullOrEmpty(hospitalList))
                    {
                        JObject jObjResult = JObject.Parse(hospitalList);
                        int count = (int)jObjResult.Property("count");
                        List<Hospital> hospitals = new List<Hospital>();
                        if (count > 0)
                        {
                            JArray jlist = JArray.Parse(jObjResult["content"].ToString());
                            for (int i = 0; i < jlist.Count; ++i)
                            {
                                Hospital hospital = new Hospital();
                                hospital.Hospital_id = int.Parse(jlist[i]["hospital_id"].ToString());
                                hospital.Name = jlist[i]["name"].ToString();
                                hospitals.Add(hospital);
                            }
                        }

                        BindHospitals(hospitals);
                    }
                }).Start();
            }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Confirm();
            }
        }
    }
}
