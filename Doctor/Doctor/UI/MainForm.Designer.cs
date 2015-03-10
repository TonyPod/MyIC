namespace Doctor
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.picBox_check = new System.Windows.Forms.PictureBox();
            this.picBox_message = new System.Windows.Forms.PictureBox();
            this.picBox_selfInfo = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.picBox_login = new System.Windows.Forms.PictureBox();
            this.picBox_logout = new System.Windows.Forms.PictureBox();
            this.picBox_register = new System.Windows.Forms.PictureBox();
            this.picBox_settings = new System.Windows.Forms.PictureBox();
            this.picBox_help = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lbl_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_loc = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_check)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_message)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_selfInfo)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_login)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_logout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_register)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_settings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_help)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.picBox_check);
            this.flowLayoutPanel1.Controls.Add(this.picBox_message);
            this.flowLayoutPanel1.Controls.Add(this.picBox_selfInfo);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 93);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(127, 322);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // picBox_check
            // 
            this.picBox_check.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_check.Image = global::Doctor.Properties.Resources.查看自检;
            this.picBox_check.Location = new System.Drawing.Point(3, 3);
            this.picBox_check.Name = "picBox_check";
            this.picBox_check.Size = new System.Drawing.Size(120, 60);
            this.picBox_check.TabIndex = 1;
            this.picBox_check.TabStop = false;
            this.picBox_check.Click += new System.EventHandler(this.picBox_check_Click);
            // 
            // picBox_message
            // 
            this.picBox_message.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_message.Image = global::Doctor.Properties.Resources.联系人;
            this.picBox_message.Location = new System.Drawing.Point(3, 69);
            this.picBox_message.Name = "picBox_message";
            this.picBox_message.Size = new System.Drawing.Size(120, 60);
            this.picBox_message.TabIndex = 1;
            this.picBox_message.TabStop = false;
            this.picBox_message.Click += new System.EventHandler(this.picBox_message_Click);
            // 
            // picBox_selfInfo
            // 
            this.picBox_selfInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_selfInfo.Image = global::Doctor.Properties.Resources.个人信息;
            this.picBox_selfInfo.Location = new System.Drawing.Point(3, 135);
            this.picBox_selfInfo.Name = "picBox_selfInfo";
            this.picBox_selfInfo.Size = new System.Drawing.Size(120, 60);
            this.picBox_selfInfo.TabIndex = 1;
            this.picBox_selfInfo.TabStop = false;
            this.picBox_selfInfo.Click += new System.EventHandler(this.picBox_selfInfo_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.picBox_login);
            this.flowLayoutPanel2.Controls.Add(this.picBox_logout);
            this.flowLayoutPanel2.Controls.Add(this.picBox_register);
            this.flowLayoutPanel2.Controls.Add(this.picBox_settings);
            this.flowLayoutPanel2.Controls.Add(this.picBox_help);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(606, 87);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // picBox_login
            // 
            this.picBox_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_login.Image = global::Doctor.Properties.Resources.登录;
            this.picBox_login.Location = new System.Drawing.Point(3, 3);
            this.picBox_login.Name = "picBox_login";
            this.picBox_login.Size = new System.Drawing.Size(80, 80);
            this.picBox_login.TabIndex = 0;
            this.picBox_login.TabStop = false;
            this.picBox_login.Click += new System.EventHandler(this.picBox_login_Click);
            // 
            // picBox_logout
            // 
            this.picBox_logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_logout.Image = global::Doctor.Properties.Resources.注销;
            this.picBox_logout.Location = new System.Drawing.Point(92, 3);
            this.picBox_logout.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_logout.Name = "picBox_logout";
            this.picBox_logout.Size = new System.Drawing.Size(80, 80);
            this.picBox_logout.TabIndex = 0;
            this.picBox_logout.TabStop = false;
            this.picBox_logout.Click += new System.EventHandler(this.picBox_logout_Click);
            // 
            // picBox_register
            // 
            this.picBox_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_register.Image = ((System.Drawing.Image)(resources.GetObject("picBox_register.Image")));
            this.picBox_register.Location = new System.Drawing.Point(181, 3);
            this.picBox_register.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_register.Name = "picBox_register";
            this.picBox_register.Size = new System.Drawing.Size(80, 80);
            this.picBox_register.TabIndex = 0;
            this.picBox_register.TabStop = false;
            this.picBox_register.Click += new System.EventHandler(this.picBox_register_Click);
            // 
            // picBox_settings
            // 
            this.picBox_settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_settings.Image = global::Doctor.Properties.Resources.设置;
            this.picBox_settings.Location = new System.Drawing.Point(270, 3);
            this.picBox_settings.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_settings.Name = "picBox_settings";
            this.picBox_settings.Size = new System.Drawing.Size(80, 80);
            this.picBox_settings.TabIndex = 0;
            this.picBox_settings.TabStop = false;
            // 
            // picBox_help
            // 
            this.picBox_help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_help.Image = global::Doctor.Properties.Resources.关于;
            this.picBox_help.Location = new System.Drawing.Point(359, 3);
            this.picBox_help.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_help.Name = "picBox_help";
            this.picBox_help.Size = new System.Drawing.Size(80, 80);
            this.picBox_help.TabIndex = 0;
            this.picBox_help.TabStop = false;
            this.picBox_help.Click += new System.EventHandler(this.picBox_help_Click);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.Location = new System.Drawing.Point(133, 93);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(473, 322);
            this.panel.TabIndex = 5;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_status,
            this.lbl_loc});
            this.statusStrip.Location = new System.Drawing.Point(0, 418);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(606, 22);
            this.statusStrip.TabIndex = 6;
            // 
            // lbl_status
            // 
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(212, 17);
            this.lbl_status.Text = "账户名显示于此（未登录显示未登录）";
            // 
            // lbl_loc
            // 
            this.lbl_loc.Name = "lbl_loc";
            this.lbl_loc.Size = new System.Drawing.Size(92, 17);
            this.lbl_loc.Text = "请检查网络连接";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 440);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "医生端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_check)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_message)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_selfInfo)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_login)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_logout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_register)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_settings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_help)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_login;
        private System.Windows.Forms.PictureBox picBox_logout;
        private System.Windows.Forms.PictureBox picBox_settings;
        private System.Windows.Forms.PictureBox picBox_message;
        private System.Windows.Forms.PictureBox picBox_check;
        private System.Windows.Forms.PictureBox picBox_selfInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.PictureBox picBox_register;
        private System.Windows.Forms.ToolStripStatusLabel lbl_status;
        private System.Windows.Forms.PictureBox picBox_help;
        private System.Windows.Forms.ToolStripStatusLabel lbl_loc;
    }
}

