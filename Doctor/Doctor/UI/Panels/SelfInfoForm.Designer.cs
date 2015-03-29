namespace Doctor.Panels
{
    partial class SelfInfoForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.link_modifyPassword = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_username = new System.Windows.Forms.Label();
            this.picBox_photo = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picBox_license = new System.Windows.Forms.PictureBox();
            this.lbl_ifAuth = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_realname = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_hospital = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_license = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.link_modifyPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbl_username);
            this.groupBox1.Controls.Add(this.picBox_photo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 436);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // link_modifyPassword
            // 
            this.link_modifyPassword.AutoSize = true;
            this.link_modifyPassword.Location = new System.Drawing.Point(15, 79);
            this.link_modifyPassword.Name = "link_modifyPassword";
            this.link_modifyPassword.Size = new System.Drawing.Size(53, 12);
            this.link_modifyPassword.TabIndex = 4;
            this.link_modifyPassword.TabStop = true;
            this.link_modifyPassword.Text = "修改密码";
            this.link_modifyPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_modifyPassword_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "个人照片";
            // 
            // lbl_username
            // 
            this.lbl_username.AutoSize = true;
            this.lbl_username.Location = new System.Drawing.Point(15, 42);
            this.lbl_username.Name = "lbl_username";
            this.lbl_username.Size = new System.Drawing.Size(77, 12);
            this.lbl_username.TabIndex = 3;
            this.lbl_username.Text = "账户名称填此";
            // 
            // picBox_photo
            // 
            this.picBox_photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_photo.Location = new System.Drawing.Point(17, 189);
            this.picBox_photo.Name = "picBox_photo";
            this.picBox_photo.Size = new System.Drawing.Size(133, 187);
            this.picBox_photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_photo.TabIndex = 2;
            this.picBox_photo.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picBox_license);
            this.groupBox2.Controls.Add(this.lbl_ifAuth);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbl_realname);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbl_hospital);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.lbl_license);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(268, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(523, 436);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "认证信息";
            // 
            // picBox_license
            // 
            this.picBox_license.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_license.Location = new System.Drawing.Point(156, 189);
            this.picBox_license.Name = "picBox_license";
            this.picBox_license.Size = new System.Drawing.Size(133, 187);
            this.picBox_license.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_license.TabIndex = 2;
            this.picBox_license.TabStop = false;
            // 
            // lbl_ifAuth
            // 
            this.lbl_ifAuth.AutoSize = true;
            this.lbl_ifAuth.ForeColor = System.Drawing.Color.Red;
            this.lbl_ifAuth.Location = new System.Drawing.Point(41, 42);
            this.lbl_ifAuth.Name = "lbl_ifAuth";
            this.lbl_ifAuth.Size = new System.Drawing.Size(41, 12);
            this.lbl_ifAuth.TabIndex = 0;
            this.lbl_ifAuth.Text = "未认证";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "真实姓名：";
            // 
            // lbl_realname
            // 
            this.lbl_realname.AutoSize = true;
            this.lbl_realname.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_realname.Location = new System.Drawing.Point(154, 79);
            this.lbl_realname.Name = "lbl_realname";
            this.lbl_realname.Size = new System.Drawing.Size(77, 12);
            this.lbl_realname.TabIndex = 0;
            this.lbl_realname.Text = "真实姓名填此";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "所属医院：";
            // 
            // lbl_hospital
            // 
            this.lbl_hospital.AutoSize = true;
            this.lbl_hospital.Location = new System.Drawing.Point(154, 114);
            this.lbl_hospital.Name = "lbl_hospital";
            this.lbl_hospital.Size = new System.Drawing.Size(41, 12);
            this.lbl_hospital.TabIndex = 0;
            this.lbl_hospital.Text = "载入中";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "执业医师证编码：";
            // 
            // lbl_license
            // 
            this.lbl_license.AutoSize = true;
            this.lbl_license.Location = new System.Drawing.Point(154, 151);
            this.lbl_license.Name = "lbl_license";
            this.lbl_license.Size = new System.Drawing.Size(113, 12);
            this.lbl_license.TabIndex = 0;
            this.lbl_license.Text = "执业医师证编码填此";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "执业医师证照片：";
            // 
            // SelfInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 460);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelfInfoForm";
            this.Text = "个人信息";
            this.Load += new System.EventHandler(this.SelfInfoForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_photo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_license)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_username;
        private System.Windows.Forms.PictureBox picBox_photo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picBox_license;
        private System.Windows.Forms.Label lbl_realname;
        private System.Windows.Forms.Label lbl_hospital;
        private System.Windows.Forms.Label lbl_license;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel link_modifyPassword;
        private System.Windows.Forms.Label lbl_ifAuth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label label1;

    }
}