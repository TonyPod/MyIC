namespace Doctor.UI.Forms
{
    partial class NewRegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbl_usernameExist = new System.Windows.Forms.Label();
            this.lbl_photo = new System.Windows.Forms.Label();
            this.picBox_photo = new System.Windows.Forms.PictureBox();
            this.lbl_username = new System.Windows.Forms.Label();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_passwordAgain = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_passwordAgain = new System.Windows.Forms.TextBox();
            this.btn_selectPhoto = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbl_license = new System.Windows.Forms.Label();
            this.cb_hospital = new System.Windows.Forms.ComboBox();
            this.cb_area = new System.Windows.Forms.ComboBox();
            this.cb_city = new System.Windows.Forms.ComboBox();
            this.lbl_realname = new System.Windows.Forms.Label();
            this.lbl_hospital = new System.Windows.Forms.Label();
            this.cb_province = new System.Windows.Forms.ComboBox();
            this.lbl_licenseNo = new System.Windows.Forms.Label();
            this.lbl_licensePhoto = new System.Windows.Forms.Label();
            this.btn_selectLicense = new System.Windows.Forms.Button();
            this.tb_realname = new System.Windows.Forms.TextBox();
            this.tb_license = new System.Windows.Forms.TextBox();
            this.picBox_license = new System.Windows.Forms.PictureBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.picBox_close = new System.Windows.Forms.PictureBox();
            this.picBox_minimize = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ItemSize = new System.Drawing.Size(60, 18);
            this.tabControl1.Location = new System.Drawing.Point(12, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 372);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.tabPage1.Controls.Add(this.lbl_usernameExist);
            this.tabPage1.Controls.Add(this.lbl_photo);
            this.tabPage1.Controls.Add(this.picBox_photo);
            this.tabPage1.Controls.Add(this.lbl_username);
            this.tabPage1.Controls.Add(this.lbl_password);
            this.tabPage1.Controls.Add(this.lbl_passwordAgain);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tb_username);
            this.tabPage1.Controls.Add(this.tb_password);
            this.tabPage1.Controls.Add(this.tb_passwordAgain);
            this.tabPage1.Controls.Add(this.btn_selectPhoto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(443, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本信息";
            // 
            // lbl_usernameExist
            // 
            this.lbl_usernameExist.AutoSize = true;
            this.lbl_usernameExist.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(185)))), ((int)(((byte)(153)))));
            this.lbl_usernameExist.Location = new System.Drawing.Point(281, 16);
            this.lbl_usernameExist.Name = "lbl_usernameExist";
            this.lbl_usernameExist.Size = new System.Drawing.Size(77, 12);
            this.lbl_usernameExist.TabIndex = 23;
            this.lbl_usernameExist.Text = "用户名已存在";
            // 
            // lbl_photo
            // 
            this.lbl_photo.AutoSize = true;
            this.lbl_photo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(185)))), ((int)(((byte)(153)))));
            this.lbl_photo.Location = new System.Drawing.Point(210, 307);
            this.lbl_photo.Name = "lbl_photo";
            this.lbl_photo.Size = new System.Drawing.Size(65, 12);
            this.lbl_photo.TabIndex = 22;
            this.lbl_photo.Text = "未选择图片";
            // 
            // picBox_photo
            // 
            this.picBox_photo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.picBox_photo.Image = global::Doctor.Properties.Resources.医生_灰_;
            this.picBox_photo.InitialImage = null;
            this.picBox_photo.Location = new System.Drawing.Point(135, 100);
            this.picBox_photo.Name = "picBox_photo";
            this.picBox_photo.Size = new System.Drawing.Size(133, 187);
            this.picBox_photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_photo.TabIndex = 18;
            this.picBox_photo.TabStop = false;
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_username.Location = new System.Drawing.Point(30, 16);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(41, 12);
            this.lbl_username.TabIndex = 13;
            this.lbl_username.Text = "用户名";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_password.Location = new System.Drawing.Point(30, 44);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(29, 12);
            this.lbl_password.TabIndex = 14;
            this.lbl_password.Text = "密码";
            // 
            // lbl_passwordAgain
            // 
            this.lbl_passwordAgain.AutoSize = true;
            this.lbl_passwordAgain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_passwordAgain.Location = new System.Drawing.Point(30, 70);
            this.lbl_passwordAgain.Name = "lbl_passwordAgain";
            this.lbl_passwordAgain.Size = new System.Drawing.Size(77, 12);
            this.lbl_passwordAgain.TabIndex = 15;
            this.lbl_passwordAgain.Text = "再次输入密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label4.Location = new System.Drawing.Point(30, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "个人照片";
            // 
            // tb_username
            // 
            this.tb_username.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_username.Location = new System.Drawing.Point(134, 13);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(132, 21);
            this.tb_username.TabIndex = 17;
            // 
            // tb_password
            // 
            this.tb_password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_password.Location = new System.Drawing.Point(134, 40);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(132, 21);
            this.tb_password.TabIndex = 19;
            // 
            // tb_passwordAgain
            // 
            this.tb_passwordAgain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_passwordAgain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_passwordAgain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_passwordAgain.Location = new System.Drawing.Point(135, 67);
            this.tb_passwordAgain.Name = "tb_passwordAgain";
            this.tb_passwordAgain.PasswordChar = '*';
            this.tb_passwordAgain.Size = new System.Drawing.Size(131, 21);
            this.tb_passwordAgain.TabIndex = 20;
            // 
            // btn_selectPhoto
            // 
            this.btn_selectPhoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_selectPhoto.FlatAppearance.BorderSize = 0;
            this.btn_selectPhoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_selectPhoto.ForeColor = System.Drawing.Color.Black;
            this.btn_selectPhoto.Location = new System.Drawing.Point(134, 302);
            this.btn_selectPhoto.Name = "btn_selectPhoto";
            this.btn_selectPhoto.Size = new System.Drawing.Size(65, 23);
            this.btn_selectPhoto.TabIndex = 21;
            this.btn_selectPhoto.Text = "选择图片";
            this.btn_selectPhoto.UseVisualStyleBackColor = false;
            this.btn_selectPhoto.Click += new System.EventHandler(this.btn_selectPhoto_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.tabPage2.Controls.Add(this.lbl_license);
            this.tabPage2.Controls.Add(this.cb_hospital);
            this.tabPage2.Controls.Add(this.cb_area);
            this.tabPage2.Controls.Add(this.cb_city);
            this.tabPage2.Controls.Add(this.lbl_realname);
            this.tabPage2.Controls.Add(this.lbl_hospital);
            this.tabPage2.Controls.Add(this.cb_province);
            this.tabPage2.Controls.Add(this.lbl_licenseNo);
            this.tabPage2.Controls.Add(this.lbl_licensePhoto);
            this.tabPage2.Controls.Add(this.btn_selectLicense);
            this.tabPage2.Controls.Add(this.tb_realname);
            this.tabPage2.Controls.Add(this.tb_license);
            this.tabPage2.Controls.Add(this.picBox_license);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "认证信息";
            // 
            // lbl_license
            // 
            this.lbl_license.AutoSize = true;
            this.lbl_license.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(185)))), ((int)(((byte)(153)))));
            this.lbl_license.Location = new System.Drawing.Point(205, 320);
            this.lbl_license.Name = "lbl_license";
            this.lbl_license.Size = new System.Drawing.Size(65, 12);
            this.lbl_license.TabIndex = 17;
            this.lbl_license.Text = "未选择图片";
            // 
            // cb_hospital
            // 
            this.cb_hospital.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.cb_hospital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_hospital.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.cb_hospital.FormattingEnabled = true;
            this.cb_hospital.Location = new System.Drawing.Point(272, 66);
            this.cb_hospital.Name = "cb_hospital";
            this.cb_hospital.Size = new System.Drawing.Size(132, 20);
            this.cb_hospital.TabIndex = 22;
            this.cb_hospital.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // cb_area
            // 
            this.cb_area.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.cb_area.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_area.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.cb_area.FormattingEnabled = true;
            this.cb_area.Location = new System.Drawing.Point(135, 66);
            this.cb_area.Name = "cb_area";
            this.cb_area.Size = new System.Drawing.Size(131, 20);
            this.cb_area.TabIndex = 21;
            this.cb_area.Text = "请选择县/区";
            this.cb_area.SelectedIndexChanged += new System.EventHandler(this.cb_area_SelectedIndexChanged);
            // 
            // cb_city
            // 
            this.cb_city.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.cb_city.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_city.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.cb_city.FormattingEnabled = true;
            this.cb_city.Location = new System.Drawing.Point(272, 40);
            this.cb_city.Name = "cb_city";
            this.cb_city.Size = new System.Drawing.Size(132, 20);
            this.cb_city.TabIndex = 20;
            this.cb_city.Text = "请选择市";
            this.cb_city.SelectedIndexChanged += new System.EventHandler(this.cb_city_SelectedIndexChanged);
            // 
            // lbl_realname
            // 
            this.lbl_realname.AutoSize = true;
            this.lbl_realname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_realname.Location = new System.Drawing.Point(30, 16);
            this.lbl_realname.Name = "lbl_realname";
            this.lbl_realname.Size = new System.Drawing.Size(53, 12);
            this.lbl_realname.TabIndex = 12;
            this.lbl_realname.Text = "真实姓名";
            // 
            // lbl_hospital
            // 
            this.lbl_hospital.AutoSize = true;
            this.lbl_hospital.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_hospital.Location = new System.Drawing.Point(30, 45);
            this.lbl_hospital.Name = "lbl_hospital";
            this.lbl_hospital.Size = new System.Drawing.Size(53, 12);
            this.lbl_hospital.TabIndex = 13;
            this.lbl_hospital.Text = "医院选择";
            // 
            // cb_province
            // 
            this.cb_province.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.cb_province.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_province.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_province.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.cb_province.FormattingEnabled = true;
            this.cb_province.Location = new System.Drawing.Point(135, 40);
            this.cb_province.Name = "cb_province";
            this.cb_province.Size = new System.Drawing.Size(131, 20);
            this.cb_province.TabIndex = 19;
            this.cb_province.SelectedIndexChanged += new System.EventHandler(this.cb_province_SelectedIndexChanged);
            // 
            // lbl_licenseNo
            // 
            this.lbl_licenseNo.AutoSize = true;
            this.lbl_licenseNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_licenseNo.Location = new System.Drawing.Point(30, 99);
            this.lbl_licenseNo.Name = "lbl_licenseNo";
            this.lbl_licenseNo.Size = new System.Drawing.Size(89, 12);
            this.lbl_licenseNo.TabIndex = 14;
            this.lbl_licenseNo.Text = "执业医师证编码";
            // 
            // lbl_licensePhoto
            // 
            this.lbl_licensePhoto.AutoSize = true;
            this.lbl_licensePhoto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.lbl_licensePhoto.Location = new System.Drawing.Point(30, 124);
            this.lbl_licensePhoto.Name = "lbl_licensePhoto";
            this.lbl_licensePhoto.Size = new System.Drawing.Size(89, 12);
            this.lbl_licensePhoto.TabIndex = 15;
            this.lbl_licensePhoto.Text = "执业医师证照片";
            // 
            // btn_selectLicense
            // 
            this.btn_selectLicense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_selectLicense.FlatAppearance.BorderSize = 0;
            this.btn_selectLicense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_selectLicense.Location = new System.Drawing.Point(134, 315);
            this.btn_selectLicense.Name = "btn_selectLicense";
            this.btn_selectLicense.Size = new System.Drawing.Size(65, 23);
            this.btn_selectLicense.TabIndex = 24;
            this.btn_selectLicense.Text = "选择图片";
            this.btn_selectLicense.UseVisualStyleBackColor = false;
            this.btn_selectLicense.Click += new System.EventHandler(this.btn_selectLicense_Click);
            // 
            // tb_realname
            // 
            this.tb_realname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_realname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_realname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_realname.Location = new System.Drawing.Point(134, 13);
            this.tb_realname.Name = "tb_realname";
            this.tb_realname.Size = new System.Drawing.Size(132, 21);
            this.tb_realname.TabIndex = 18;
            this.tb_realname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // tb_license
            // 
            this.tb_license.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_license.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_license.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_license.Location = new System.Drawing.Point(135, 95);
            this.tb_license.Name = "tb_license";
            this.tb_license.Size = new System.Drawing.Size(132, 21);
            this.tb_license.TabIndex = 23;
            this.tb_license.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // picBox_license
            // 
            this.picBox_license.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.picBox_license.Image = global::Doctor.Properties.Resources.license;
            this.picBox_license.Location = new System.Drawing.Point(134, 122);
            this.picBox_license.Name = "picBox_license";
            this.picBox_license.Size = new System.Drawing.Size(133, 187);
            this.picBox_license.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_license.TabIndex = 16;
            this.picBox_license.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(388, 403);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 15;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_confirm.FlatAppearance.BorderSize = 0;
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Location = new System.Drawing.Point(307, 403);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 14;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.picBox_close);
            this.flowLayoutPanel1.Controls.Add(this.picBox_minimize);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(465, 26);
            this.flowLayoutPanel1.TabIndex = 16;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.flowLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // picBox_close
            // 
            this.picBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_close.Image = global::Doctor.Properties.Resources.close2;
            this.picBox_close.Location = new System.Drawing.Point(438, 0);
            this.picBox_close.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.picBox_close.Name = "picBox_close";
            this.picBox_close.Size = new System.Drawing.Size(24, 24);
            this.picBox_close.TabIndex = 7;
            this.picBox_close.TabStop = false;
            this.picBox_close.Click += new System.EventHandler(this.picBox_close_Click);
            // 
            // picBox_minimize
            // 
            this.picBox_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_minimize.Image = global::Doctor.Properties.Resources.minimize;
            this.picBox_minimize.Location = new System.Drawing.Point(408, 0);
            this.picBox_minimize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.picBox_minimize.Name = "picBox_minimize";
            this.picBox_minimize.Size = new System.Drawing.Size(24, 24);
            this.picBox_minimize.TabIndex = 7;
            this.picBox_minimize.TabStop = false;
            this.picBox_minimize.Click += new System.EventHandler(this.picBox_minimize_Click);
            // 
            // NewRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(475, 438);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewRegisterForm";
            this.Text = "注册";
            this.Load += new System.EventHandler(this.NewRegisterForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lbl_usernameExist;
        private System.Windows.Forms.Label lbl_photo;
        private System.Windows.Forms.PictureBox picBox_photo;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_passwordAgain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_passwordAgain;
        private System.Windows.Forms.Button btn_selectPhoto;
        private System.Windows.Forms.Label lbl_license;
        private System.Windows.Forms.ComboBox cb_hospital;
        private System.Windows.Forms.PictureBox picBox_license;
        private System.Windows.Forms.ComboBox cb_area;
        private System.Windows.Forms.ComboBox cb_city;
        private System.Windows.Forms.Label lbl_realname;
        private System.Windows.Forms.Label lbl_hospital;
        private System.Windows.Forms.ComboBox cb_province;
        private System.Windows.Forms.Label lbl_licenseNo;
        private System.Windows.Forms.Label lbl_licensePhoto;
        private System.Windows.Forms.Button btn_selectLicense;
        private System.Windows.Forms.TextBox tb_realname;
        private System.Windows.Forms.TextBox tb_license;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox picBox_close;
        private System.Windows.Forms.PictureBox picBox_minimize;
    }
}