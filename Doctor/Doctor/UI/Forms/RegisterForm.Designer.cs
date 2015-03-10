namespace Doctor.Forms
{
    partial class RegisterForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_passwordAgain = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_selectPhoto = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_selectLicense = new System.Windows.Forms.Button();
            this.tb_license = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.picBox_license = new System.Windows.Forms.PictureBox();
            this.picBox_photo = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_province = new System.Windows.Forms.ComboBox();
            this.cb_city = new System.Windows.Forms.ComboBox();
            this.cb_hospital = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_photo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_license = new System.Windows.Forms.Label();
            this.cb_area = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_realname = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.bgWorker_username = new System.ComponentModel.BackgroundWorker();
            this.lbl_usernameExist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "再次输入密码：";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(109, 24);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(132, 21);
            this.tb_username.TabIndex = 1;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(109, 52);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(132, 21);
            this.tb_password.TabIndex = 2;
            // 
            // tb_passwordAgain
            // 
            this.tb_passwordAgain.Location = new System.Drawing.Point(110, 81);
            this.tb_passwordAgain.Name = "tb_passwordAgain";
            this.tb_passwordAgain.PasswordChar = '*';
            this.tb_passwordAgain.Size = new System.Drawing.Size(131, 21);
            this.tb_passwordAgain.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "个人照片：";
            // 
            // btn_selectPhoto
            // 
            this.btn_selectPhoto.Location = new System.Drawing.Point(110, 305);
            this.btn_selectPhoto.Name = "btn_selectPhoto";
            this.btn_selectPhoto.Size = new System.Drawing.Size(65, 23);
            this.btn_selectPhoto.TabIndex = 4;
            this.btn_selectPhoto.Text = "选择图片";
            this.btn_selectPhoto.UseVisualStyleBackColor = true;
            this.btn_selectPhoto.Click += new System.EventHandler(this.btn_selectPhoto_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "执业医师证编码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "执业医师证照片：";
            // 
            // btn_selectLicense
            // 
            this.btn_selectLicense.Location = new System.Drawing.Point(121, 361);
            this.btn_selectLicense.Name = "btn_selectLicense";
            this.btn_selectLicense.Size = new System.Drawing.Size(65, 23);
            this.btn_selectLicense.TabIndex = 11;
            this.btn_selectLicense.Text = "选择图片";
            this.btn_selectLicense.UseVisualStyleBackColor = true;
            this.btn_selectLicense.Click += new System.EventHandler(this.btn_selectLicense_Click);
            // 
            // tb_license
            // 
            this.tb_license.Location = new System.Drawing.Point(121, 143);
            this.tb_license.Name = "tb_license";
            this.tb_license.Size = new System.Drawing.Size(132, 21);
            this.tb_license.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(10, 415);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(275, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "(注：图片上传大小：2M,图片最大尺寸：1024*768)";
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(417, 410);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 12;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(498, 410);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 13;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // picBox_license
            // 
            this.picBox_license.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_license.Location = new System.Drawing.Point(121, 170);
            this.picBox_license.Name = "picBox_license";
            this.picBox_license.Size = new System.Drawing.Size(133, 187);
            this.picBox_license.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_license.TabIndex = 2;
            this.picBox_license.TabStop = false;
            // 
            // picBox_photo
            // 
            this.picBox_photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_photo.Location = new System.Drawing.Point(110, 111);
            this.picBox_photo.Name = "picBox_photo";
            this.picBox_photo.Size = new System.Drawing.Size(133, 187);
            this.picBox_photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_photo.TabIndex = 2;
            this.picBox_photo.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "医院选择：";
            // 
            // cb_province
            // 
            this.cb_province.FormattingEnabled = true;
            this.cb_province.Location = new System.Drawing.Point(121, 42);
            this.cb_province.Name = "cb_province";
            this.cb_province.Size = new System.Drawing.Size(132, 20);
            this.cb_province.TabIndex = 6;
            this.cb_province.Text = "请选择省/直辖市";
            this.cb_province.SelectedIndexChanged += new System.EventHandler(this.cb_province_SelectedIndexChanged);
            // 
            // cb_city
            // 
            this.cb_city.FormattingEnabled = true;
            this.cb_city.Location = new System.Drawing.Point(121, 67);
            this.cb_city.Name = "cb_city";
            this.cb_city.Size = new System.Drawing.Size(132, 20);
            this.cb_city.TabIndex = 7;
            this.cb_city.Text = "请选择市";
            this.cb_city.SelectedIndexChanged += new System.EventHandler(this.cb_city_SelectedIndexChanged);
            // 
            // cb_hospital
            // 
            this.cb_hospital.FormattingEnabled = true;
            this.cb_hospital.Location = new System.Drawing.Point(122, 117);
            this.cb_hospital.Name = "cb_hospital";
            this.cb_hospital.Size = new System.Drawing.Size(132, 20);
            this.cb_hospital.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_usernameExist);
            this.groupBox1.Controls.Add(this.lbl_photo);
            this.groupBox1.Controls.Add(this.picBox_photo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_username);
            this.groupBox1.Controls.Add(this.tb_password);
            this.groupBox1.Controls.Add(this.tb_passwordAgain);
            this.groupBox1.Controls.Add(this.btn_selectPhoto);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 395);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // lbl_photo
            // 
            this.lbl_photo.AutoSize = true;
            this.lbl_photo.ForeColor = System.Drawing.Color.Red;
            this.lbl_photo.Location = new System.Drawing.Point(181, 310);
            this.lbl_photo.Name = "lbl_photo";
            this.lbl_photo.Size = new System.Drawing.Size(65, 12);
            this.lbl_photo.TabIndex = 5;
            this.lbl_photo.Text = "未选择图片";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_license);
            this.groupBox2.Controls.Add(this.cb_hospital);
            this.groupBox2.Controls.Add(this.picBox_license);
            this.groupBox2.Controls.Add(this.cb_area);
            this.groupBox2.Controls.Add(this.cb_city);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cb_province);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btn_selectLicense);
            this.groupBox2.Controls.Add(this.tb_realname);
            this.groupBox2.Controls.Add(this.tb_license);
            this.groupBox2.Location = new System.Drawing.Point(277, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 395);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "认证信息";
            // 
            // lbl_license
            // 
            this.lbl_license.AutoSize = true;
            this.lbl_license.ForeColor = System.Drawing.Color.Red;
            this.lbl_license.Location = new System.Drawing.Point(194, 366);
            this.lbl_license.Name = "lbl_license";
            this.lbl_license.Size = new System.Drawing.Size(65, 12);
            this.lbl_license.TabIndex = 5;
            this.lbl_license.Text = "未选择图片";
            // 
            // cb_area
            // 
            this.cb_area.FormattingEnabled = true;
            this.cb_area.Location = new System.Drawing.Point(121, 91);
            this.cb_area.Name = "cb_area";
            this.cb_area.Size = new System.Drawing.Size(133, 20);
            this.cb_area.TabIndex = 8;
            this.cb_area.Text = "请选择县/区";
            this.cb_area.SelectedIndexChanged += new System.EventHandler(this.cb_area_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "真实姓名：";
            // 
            // tb_realname
            // 
            this.tb_realname.Location = new System.Drawing.Point(121, 19);
            this.tb_realname.Name = "tb_realname";
            this.tb_realname.Size = new System.Drawing.Size(132, 21);
            this.tb_realname.TabIndex = 5;
            // 
            // lbl_usernameExist
            // 
            this.lbl_usernameExist.AutoSize = true;
            this.lbl_usernameExist.ForeColor = System.Drawing.Color.Red;
            this.lbl_usernameExist.Location = new System.Drawing.Point(107, 9);
            this.lbl_usernameExist.Name = "lbl_usernameExist";
            this.lbl_usernameExist.Size = new System.Drawing.Size(77, 12);
            this.lbl_usernameExist.TabIndex = 12;
            this.lbl_usernameExist.Text = "用户名已存在";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 436);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.Text = "注册";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_passwordAgain;
        private System.Windows.Forms.PictureBox picBox_photo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_selectPhoto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox picBox_license;
        private System.Windows.Forms.Button btn_selectLicense;
        private System.Windows.Forms.TextBox tb_license;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_province;
        private System.Windows.Forms.ComboBox cb_city;
        private System.Windows.Forms.ComboBox cb_hospital;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_realname;
        private System.Windows.Forms.ComboBox cb_area;
        private System.Windows.Forms.Label lbl_photo;
        private System.Windows.Forms.Label lbl_license;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.ComponentModel.BackgroundWorker bgWorker_username;
        private System.Windows.Forms.Label lbl_usernameExist;
    }
}