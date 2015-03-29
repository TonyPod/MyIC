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
            this.picBox_license = new System.Windows.Forms.PictureBox();
            this.cb_area = new System.Windows.Forms.ComboBox();
            this.cb_city = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_province = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_selectLicense = new System.Windows.Forms.Button();
            this.tb_realname = new System.Windows.Forms.TextBox();
            this.tb_license = new System.Windows.Forms.TextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 372);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
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
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbl_usernameExist
            // 
            this.lbl_usernameExist.AutoSize = true;
            this.lbl_usernameExist.ForeColor = System.Drawing.Color.Red;
            this.lbl_usernameExist.Location = new System.Drawing.Point(281, 16);
            this.lbl_usernameExist.Name = "lbl_usernameExist";
            this.lbl_usernameExist.Size = new System.Drawing.Size(77, 12);
            this.lbl_usernameExist.TabIndex = 23;
            this.lbl_usernameExist.Text = "用户名已存在";
            // 
            // lbl_photo
            // 
            this.lbl_photo.AutoSize = true;
            this.lbl_photo.ForeColor = System.Drawing.Color.Red;
            this.lbl_photo.Location = new System.Drawing.Point(210, 298);
            this.lbl_photo.Name = "lbl_photo";
            this.lbl_photo.Size = new System.Drawing.Size(65, 12);
            this.lbl_photo.TabIndex = 22;
            this.lbl_photo.Text = "未选择图片";
            // 
            // picBox_photo
            // 
            this.picBox_photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.lbl_username.Location = new System.Drawing.Point(30, 16);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(53, 12);
            this.lbl_username.TabIndex = 13;
            this.lbl_username.Text = "用户名：";
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(30, 44);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(41, 12);
            this.lbl_password.TabIndex = 14;
            this.lbl_password.Text = "密码：";
            // 
            // lbl_passwordAgain
            // 
            this.lbl_passwordAgain.AutoSize = true;
            this.lbl_passwordAgain.Location = new System.Drawing.Point(30, 70);
            this.lbl_passwordAgain.Name = "lbl_passwordAgain";
            this.lbl_passwordAgain.Size = new System.Drawing.Size(89, 12);
            this.lbl_passwordAgain.TabIndex = 15;
            this.lbl_passwordAgain.Text = "再次输入密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "个人照片：";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(134, 13);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(132, 21);
            this.tb_username.TabIndex = 17;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(134, 40);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(132, 21);
            this.tb_password.TabIndex = 19;
            // 
            // tb_passwordAgain
            // 
            this.tb_passwordAgain.Location = new System.Drawing.Point(135, 67);
            this.tb_passwordAgain.Name = "tb_passwordAgain";
            this.tb_passwordAgain.PasswordChar = '*';
            this.tb_passwordAgain.Size = new System.Drawing.Size(131, 21);
            this.tb_passwordAgain.TabIndex = 20;
            // 
            // btn_selectPhoto
            // 
            this.btn_selectPhoto.Location = new System.Drawing.Point(134, 293);
            this.btn_selectPhoto.Name = "btn_selectPhoto";
            this.btn_selectPhoto.Size = new System.Drawing.Size(65, 23);
            this.btn_selectPhoto.TabIndex = 21;
            this.btn_selectPhoto.Text = "选择图片";
            this.btn_selectPhoto.UseVisualStyleBackColor = true;
            this.btn_selectPhoto.Click += new System.EventHandler(this.btn_selectPhoto_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbl_license);
            this.tabPage2.Controls.Add(this.cb_hospital);
            this.tabPage2.Controls.Add(this.picBox_license);
            this.tabPage2.Controls.Add(this.cb_area);
            this.tabPage2.Controls.Add(this.cb_city);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.cb_province);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.btn_selectLicense);
            this.tabPage2.Controls.Add(this.tb_realname);
            this.tabPage2.Controls.Add(this.tb_license);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "认证信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbl_license
            // 
            this.lbl_license.AutoSize = true;
            this.lbl_license.ForeColor = System.Drawing.Color.Red;
            this.lbl_license.Location = new System.Drawing.Point(195, 322);
            this.lbl_license.Name = "lbl_license";
            this.lbl_license.Size = new System.Drawing.Size(65, 12);
            this.lbl_license.TabIndex = 17;
            this.lbl_license.Text = "未选择图片";
            // 
            // cb_hospital
            // 
            this.cb_hospital.FormattingEnabled = true;
            this.cb_hospital.Location = new System.Drawing.Point(262, 68);
            this.cb_hospital.Name = "cb_hospital";
            this.cb_hospital.Size = new System.Drawing.Size(132, 20);
            this.cb_hospital.TabIndex = 22;
            this.cb_hospital.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // picBox_license
            // 
            this.picBox_license.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_license.Location = new System.Drawing.Point(124, 124);
            this.picBox_license.Name = "picBox_license";
            this.picBox_license.Size = new System.Drawing.Size(133, 187);
            this.picBox_license.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_license.TabIndex = 16;
            this.picBox_license.TabStop = false;
            // 
            // cb_area
            // 
            this.cb_area.FormattingEnabled = true;
            this.cb_area.Location = new System.Drawing.Point(124, 68);
            this.cb_area.Name = "cb_area";
            this.cb_area.Size = new System.Drawing.Size(133, 20);
            this.cb_area.TabIndex = 21;
            this.cb_area.Text = "请选择县/区";
            this.cb_area.SelectedIndexChanged += new System.EventHandler(this.cb_area_SelectedIndexChanged);
            // 
            // cb_city
            // 
            this.cb_city.FormattingEnabled = true;
            this.cb_city.Location = new System.Drawing.Point(262, 42);
            this.cb_city.Name = "cb_city";
            this.cb_city.Size = new System.Drawing.Size(132, 20);
            this.cb_city.TabIndex = 20;
            this.cb_city.Text = "请选择市";
            this.cb_city.SelectedIndexChanged += new System.EventHandler(this.cb_city_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "真实姓名：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "医院选择：";
            // 
            // cb_province
            // 
            this.cb_province.FormattingEnabled = true;
            this.cb_province.Location = new System.Drawing.Point(124, 42);
            this.cb_province.Name = "cb_province";
            this.cb_province.Size = new System.Drawing.Size(132, 20);
            this.cb_province.TabIndex = 19;
            this.cb_province.Text = "请选择省/直辖市";
            this.cb_province.SelectedIndexChanged += new System.EventHandler(this.cb_province_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "执业医师证编码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "执业医师证照片：";
            // 
            // btn_selectLicense
            // 
            this.btn_selectLicense.Location = new System.Drawing.Point(124, 317);
            this.btn_selectLicense.Name = "btn_selectLicense";
            this.btn_selectLicense.Size = new System.Drawing.Size(65, 23);
            this.btn_selectLicense.TabIndex = 24;
            this.btn_selectLicense.Text = "选择图片";
            this.btn_selectLicense.UseVisualStyleBackColor = true;
            this.btn_selectLicense.Click += new System.EventHandler(this.btn_selectLicense_Click);
            // 
            // tb_realname
            // 
            this.tb_realname.Location = new System.Drawing.Point(124, 11);
            this.tb_realname.Name = "tb_realname";
            this.tb_realname.Size = new System.Drawing.Size(132, 21);
            this.tb_realname.TabIndex = 18;
            this.tb_realname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // tb_license
            // 
            this.tb_license.Location = new System.Drawing.Point(125, 97);
            this.tb_license.Name = "tb_license";
            this.tb_license.Size = new System.Drawing.Size(132, 21);
            this.tb_license.TabIndex = 23;
            this.tb_license.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(388, 386);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 15;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(307, 386);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 14;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // NewRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 414);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.tabControl1);
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
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_province;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_selectLicense;
        private System.Windows.Forms.TextBox tb_realname;
        private System.Windows.Forms.TextBox tb_license;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}