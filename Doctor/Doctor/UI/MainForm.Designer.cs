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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_status = new System.Windows.Forms.Label();
            this.picBox_login = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_quit = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_about = new System.Windows.Forms.Panel();
            this.picBox_help = new System.Windows.Forms.PictureBox();
            this.lbl_about = new System.Windows.Forms.Label();
            this.panel_settings = new System.Windows.Forms.Panel();
            this.picBox_settings = new System.Windows.Forms.PictureBox();
            this.lbl_settings = new System.Windows.Forms.Label();
            this.lbl_logout = new System.Windows.Forms.Label();
            this.lbl_register = new System.Windows.Forms.Label();
            this.lbl_selfCheck = new System.Windows.Forms.Label();
            this.lbl_contacts = new System.Windows.Forms.Label();
            this.lbl_selfInfo = new System.Windows.Forms.Label();
            this.panel_selfCheck = new System.Windows.Forms.Panel();
            this.picBox_check = new System.Windows.Forms.PictureBox();
            this.panel_contacts = new System.Windows.Forms.Panel();
            this.picBox_message = new System.Windows.Forms.PictureBox();
            this.panel_selfInfo = new System.Windows.Forms.Panel();
            this.picBox_selfInfo = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_register = new System.Windows.Forms.Panel();
            this.picBox_register = new System.Windows.Forms.PictureBox();
            this.panel_logout = new System.Windows.Forms.Panel();
            this.picBox_logout = new System.Windows.Forms.PictureBox();
            this.lbl_loc = new System.Windows.Forms.Label();
            this.lbl_imStatus = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_login)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel_about.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_help)).BeginInit();
            this.panel_settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_settings)).BeginInit();
            this.panel_selfCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_check)).BeginInit();
            this.panel_contacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_message)).BeginInit();
            this.panel_selfInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_selfInfo)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel_register.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_register)).BeginInit();
            this.panel_logout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_logout)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.panel1.Controls.Add(this.lbl_status);
            this.panel1.Controls.Add(this.picBox_login);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 95);
            this.panel1.TabIndex = 7;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoEllipsis = true;
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_status.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_status.Location = new System.Drawing.Point(40, 73);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(41, 12);
            this.lbl_status.TabIndex = 1;
            this.lbl_status.Text = "未登录";
            this.lbl_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBox_login
            // 
            this.picBox_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_login.Image = global::Doctor.Properties.Resources.登陆1;
            this.picBox_login.Location = new System.Drawing.Point(32, 12);
            this.picBox_login.Name = "picBox_login";
            this.picBox_login.Size = new System.Drawing.Size(58, 58);
            this.picBox_login.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox_login.TabIndex = 0;
            this.picBox_login.TabStop = false;
            this.picBox_login.Click += new System.EventHandler(this.picBox_login_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))));
            this.panel2.Controls.Add(this.panel_register);
            this.panel2.Controls.Add(this.panel_logout);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel_about);
            this.panel2.Controls.Add(this.panel_settings);
            this.panel2.Location = new System.Drawing.Point(4, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(119, 463);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))));
            this.panel3.Controls.Add(this.lbl_quit);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel3.Location = new System.Drawing.Point(0, 198);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(119, 59);
            this.panel3.TabIndex = 4;
            this.panel3.Click += new System.EventHandler(this.picBox_exit_Click);
            // 
            // lbl_quit
            // 
            this.lbl_quit.AutoSize = true;
            this.lbl_quit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_quit.Location = new System.Drawing.Point(55, 21);
            this.lbl_quit.Name = "lbl_quit";
            this.lbl_quit.Size = new System.Drawing.Size(40, 16);
            this.lbl_quit.TabIndex = 1;
            this.lbl_quit.Text = "退出";
            this.lbl_quit.Click += new System.EventHandler(this.picBox_exit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Doctor.Properties.Resources.close;
            this.pictureBox1.Location = new System.Drawing.Point(15, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.picBox_exit_Click);
            // 
            // panel_about
            // 
            this.panel_about.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))));
            this.panel_about.Controls.Add(this.picBox_help);
            this.panel_about.Controls.Add(this.lbl_about);
            this.panel_about.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_about.Location = new System.Drawing.Point(0, 369);
            this.panel_about.Name = "panel_about";
            this.panel_about.Size = new System.Drawing.Size(119, 94);
            this.panel_about.TabIndex = 3;
            this.panel_about.Click += new System.EventHandler(this.picBox_help_Click);
            // 
            // picBox_help
            // 
            this.picBox_help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_help.Image = global::Doctor.Properties.Resources.关于1;
            this.picBox_help.Location = new System.Drawing.Point(42, 23);
            this.picBox_help.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_help.Name = "picBox_help";
            this.picBox_help.Size = new System.Drawing.Size(34, 34);
            this.picBox_help.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_help.TabIndex = 0;
            this.picBox_help.TabStop = false;
            this.picBox_help.Click += new System.EventHandler(this.picBox_help_Click);
            // 
            // lbl_about
            // 
            this.lbl_about.AutoSize = true;
            this.lbl_about.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_about.Location = new System.Drawing.Point(39, 60);
            this.lbl_about.Name = "lbl_about";
            this.lbl_about.Size = new System.Drawing.Size(40, 16);
            this.lbl_about.TabIndex = 1;
            this.lbl_about.Text = "关于";
            this.lbl_about.Click += new System.EventHandler(this.picBox_help_Click);
            // 
            // panel_settings
            // 
            this.panel_settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))));
            this.panel_settings.Controls.Add(this.picBox_settings);
            this.panel_settings.Controls.Add(this.lbl_settings);
            this.panel_settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_settings.Location = new System.Drawing.Point(0, 133);
            this.panel_settings.Name = "panel_settings";
            this.panel_settings.Size = new System.Drawing.Size(119, 59);
            this.panel_settings.TabIndex = 2;
            this.panel_settings.Click += new System.EventHandler(this.picBox_settings_Click);
            // 
            // picBox_settings
            // 
            this.picBox_settings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_settings.Image = global::Doctor.Properties.Resources.设置1;
            this.picBox_settings.Location = new System.Drawing.Point(15, 13);
            this.picBox_settings.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_settings.Name = "picBox_settings";
            this.picBox_settings.Size = new System.Drawing.Size(34, 34);
            this.picBox_settings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_settings.TabIndex = 0;
            this.picBox_settings.TabStop = false;
            this.picBox_settings.Click += new System.EventHandler(this.picBox_settings_Click);
            // 
            // lbl_settings
            // 
            this.lbl_settings.AutoSize = true;
            this.lbl_settings.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_settings.Location = new System.Drawing.Point(55, 22);
            this.lbl_settings.Name = "lbl_settings";
            this.lbl_settings.Size = new System.Drawing.Size(40, 16);
            this.lbl_settings.TabIndex = 1;
            this.lbl_settings.Text = "设置";
            this.lbl_settings.Click += new System.EventHandler(this.picBox_settings_Click);
            // 
            // lbl_logout
            // 
            this.lbl_logout.AutoSize = true;
            this.lbl_logout.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_logout.Location = new System.Drawing.Point(55, 21);
            this.lbl_logout.Name = "lbl_logout";
            this.lbl_logout.Size = new System.Drawing.Size(40, 16);
            this.lbl_logout.TabIndex = 1;
            this.lbl_logout.Text = "注销";
            this.lbl_logout.Click += new System.EventHandler(this.picBox_logout_Click);
            // 
            // lbl_register
            // 
            this.lbl_register.AutoSize = true;
            this.lbl_register.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_register.Location = new System.Drawing.Point(55, 22);
            this.lbl_register.Name = "lbl_register";
            this.lbl_register.Size = new System.Drawing.Size(40, 16);
            this.lbl_register.TabIndex = 1;
            this.lbl_register.Text = "注册";
            this.lbl_register.Click += new System.EventHandler(this.picBox_register_Click);
            // 
            // lbl_selfCheck
            // 
            this.lbl_selfCheck.AutoSize = true;
            this.lbl_selfCheck.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_selfCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_selfCheck.Location = new System.Drawing.Point(17, 64);
            this.lbl_selfCheck.Name = "lbl_selfCheck";
            this.lbl_selfCheck.Size = new System.Drawing.Size(63, 14);
            this.lbl_selfCheck.TabIndex = 1;
            this.lbl_selfCheck.Text = "自检信息";
            this.lbl_selfCheck.Click += new System.EventHandler(this.picBox_check_Click);
            // 
            // lbl_contacts
            // 
            this.lbl_contacts.AutoSize = true;
            this.lbl_contacts.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_contacts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_contacts.Location = new System.Drawing.Point(23, 64);
            this.lbl_contacts.Name = "lbl_contacts";
            this.lbl_contacts.Size = new System.Drawing.Size(49, 14);
            this.lbl_contacts.TabIndex = 1;
            this.lbl_contacts.Text = "联系人";
            this.lbl_contacts.Click += new System.EventHandler(this.picBox_message_Click);
            // 
            // lbl_selfInfo
            // 
            this.lbl_selfInfo.AutoSize = true;
            this.lbl_selfInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_selfInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_selfInfo.Location = new System.Drawing.Point(14, 64);
            this.lbl_selfInfo.Name = "lbl_selfInfo";
            this.lbl_selfInfo.Size = new System.Drawing.Size(63, 14);
            this.lbl_selfInfo.TabIndex = 1;
            this.lbl_selfInfo.Text = "个人信息";
            this.lbl_selfInfo.Click += new System.EventHandler(this.picBox_selfInfo_Click);
            // 
            // panel_selfCheck
            // 
            this.panel_selfCheck.Controls.Add(this.picBox_check);
            this.panel_selfCheck.Controls.Add(this.lbl_selfCheck);
            this.panel_selfCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_selfCheck.Location = new System.Drawing.Point(509, 0);
            this.panel_selfCheck.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel_selfCheck.Name = "panel_selfCheck";
            this.panel_selfCheck.Size = new System.Drawing.Size(95, 95);
            this.panel_selfCheck.TabIndex = 0;
            this.panel_selfCheck.Click += new System.EventHandler(this.picBox_check_Click);
            // 
            // picBox_check
            // 
            this.picBox_check.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_check.Image = global::Doctor.Properties.Resources.自检信息2;
            this.picBox_check.Location = new System.Drawing.Point(29, 21);
            this.picBox_check.Name = "picBox_check";
            this.picBox_check.Size = new System.Drawing.Size(40, 40);
            this.picBox_check.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox_check.TabIndex = 1;
            this.picBox_check.TabStop = false;
            this.picBox_check.Click += new System.EventHandler(this.picBox_check_Click);
            // 
            // panel_contacts
            // 
            this.panel_contacts.Controls.Add(this.picBox_message);
            this.panel_contacts.Controls.Add(this.lbl_contacts);
            this.panel_contacts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_contacts.Location = new System.Drawing.Point(607, 0);
            this.panel_contacts.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel_contacts.Name = "panel_contacts";
            this.panel_contacts.Size = new System.Drawing.Size(95, 95);
            this.panel_contacts.TabIndex = 2;
            this.panel_contacts.Click += new System.EventHandler(this.picBox_message_Click);
            // 
            // picBox_message
            // 
            this.picBox_message.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_message.Image = global::Doctor.Properties.Resources.联系人2;
            this.picBox_message.Location = new System.Drawing.Point(26, 21);
            this.picBox_message.Name = "picBox_message";
            this.picBox_message.Size = new System.Drawing.Size(40, 40);
            this.picBox_message.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox_message.TabIndex = 1;
            this.picBox_message.TabStop = false;
            this.picBox_message.Click += new System.EventHandler(this.picBox_message_Click);
            // 
            // panel_selfInfo
            // 
            this.panel_selfInfo.Controls.Add(this.picBox_selfInfo);
            this.panel_selfInfo.Controls.Add(this.lbl_selfInfo);
            this.panel_selfInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_selfInfo.Location = new System.Drawing.Point(705, 0);
            this.panel_selfInfo.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel_selfInfo.Name = "panel_selfInfo";
            this.panel_selfInfo.Size = new System.Drawing.Size(95, 95);
            this.panel_selfInfo.TabIndex = 3;
            this.panel_selfInfo.Click += new System.EventHandler(this.picBox_selfInfo_Click);
            // 
            // picBox_selfInfo
            // 
            this.picBox_selfInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_selfInfo.Image = global::Doctor.Properties.Resources.个人信息2;
            this.picBox_selfInfo.Location = new System.Drawing.Point(27, 21);
            this.picBox_selfInfo.Name = "picBox_selfInfo";
            this.picBox_selfInfo.Size = new System.Drawing.Size(40, 40);
            this.picBox_selfInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox_selfInfo.TabIndex = 1;
            this.picBox_selfInfo.TabStop = false;
            this.picBox_selfInfo.Click += new System.EventHandler(this.picBox_selfInfo_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.flowLayoutPanel1.Controls.Add(this.panel_selfInfo);
            this.flowLayoutPanel1.Controls.Add(this.panel_contacts);
            this.flowLayoutPanel1.Controls.Add(this.panel_selfCheck);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(122, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(803, 95);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.flowLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // panel_register
            // 
            this.panel_register.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))));
            this.panel_register.Controls.Add(this.lbl_register);
            this.panel_register.Controls.Add(this.picBox_register);
            this.panel_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_register.Location = new System.Drawing.Point(0, 68);
            this.panel_register.Name = "panel_register";
            this.panel_register.Size = new System.Drawing.Size(119, 59);
            this.panel_register.TabIndex = 0;
            this.panel_register.Click += new System.EventHandler(this.picBox_register_Click);
            // 
            // picBox_register
            // 
            this.picBox_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_register.Image = global::Doctor.Properties.Resources.注册2;
            this.picBox_register.Location = new System.Drawing.Point(15, 13);
            this.picBox_register.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_register.Name = "picBox_register";
            this.picBox_register.Size = new System.Drawing.Size(34, 34);
            this.picBox_register.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_register.TabIndex = 0;
            this.picBox_register.TabStop = false;
            this.picBox_register.Click += new System.EventHandler(this.picBox_register_Click);
            // 
            // panel_logout
            // 
            this.panel_logout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(97)))), ((int)(((byte)(94)))));
            this.panel_logout.Controls.Add(this.lbl_logout);
            this.panel_logout.Controls.Add(this.picBox_logout);
            this.panel_logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel_logout.Location = new System.Drawing.Point(0, 3);
            this.panel_logout.Name = "panel_logout";
            this.panel_logout.Size = new System.Drawing.Size(119, 59);
            this.panel_logout.TabIndex = 3;
            this.panel_logout.Click += new System.EventHandler(this.picBox_logout_Click);
            // 
            // picBox_logout
            // 
            this.picBox_logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_logout.Image = global::Doctor.Properties.Resources.注销1;
            this.picBox_logout.Location = new System.Drawing.Point(15, 12);
            this.picBox_logout.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.picBox_logout.Name = "picBox_logout";
            this.picBox_logout.Size = new System.Drawing.Size(34, 34);
            this.picBox_logout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox_logout.TabIndex = 0;
            this.picBox_logout.TabStop = false;
            this.picBox_logout.Click += new System.EventHandler(this.picBox_logout_Click);
            // 
            // lbl_loc
            // 
            this.lbl_loc.AutoSize = true;
            this.lbl_loc.Location = new System.Drawing.Point(11, 563);
            this.lbl_loc.Name = "lbl_loc";
            this.lbl_loc.Size = new System.Drawing.Size(41, 12);
            this.lbl_loc.TabIndex = 0;
            this.lbl_loc.Text = "定位中";
            // 
            // lbl_imStatus
            // 
            this.lbl_imStatus.AutoSize = true;
            this.lbl_imStatus.Location = new System.Drawing.Point(836, 563);
            this.lbl_imStatus.Name = "lbl_imStatus";
            this.lbl_imStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_imStatus.Size = new System.Drawing.Size(89, 12);
            this.lbl_imStatus.TabIndex = 0;
            this.lbl_imStatus.Text = "即时通讯未连接";
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BackgroundImage = global::Doctor.Properties.Resources.背景2;
            this.panel.Location = new System.Drawing.Point(122, 97);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(803, 463);
            this.panel.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(929, 578);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_imStatus);
            this.Controls.Add(this.lbl_loc);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel);
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "医生端";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_login)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel_about.ResumeLayout(false);
            this.panel_about.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_help)).EndInit();
            this.panel_settings.ResumeLayout(false);
            this.panel_settings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_settings)).EndInit();
            this.panel_selfCheck.ResumeLayout(false);
            this.panel_selfCheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_check)).EndInit();
            this.panel_contacts.ResumeLayout(false);
            this.panel_contacts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_message)).EndInit();
            this.panel_selfInfo.ResumeLayout(false);
            this.panel_selfInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_selfInfo)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel_register.ResumeLayout(false);
            this.panel_register.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_register)).EndInit();
            this.panel_logout.ResumeLayout(false);
            this.panel_logout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_logout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_login;
        private System.Windows.Forms.PictureBox picBox_logout;
        private System.Windows.Forms.PictureBox picBox_message;
        private System.Windows.Forms.PictureBox picBox_check;
        private System.Windows.Forms.PictureBox picBox_selfInfo;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.PictureBox picBox_register;
        private System.Windows.Forms.PictureBox picBox_help;
        private System.Windows.Forms.PictureBox picBox_settings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_about;
        private System.Windows.Forms.Label lbl_logout;
        private System.Windows.Forms.Label lbl_settings;
        private System.Windows.Forms.Label lbl_register;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.Label lbl_selfInfo;
        private System.Windows.Forms.Label lbl_contacts;
        private System.Windows.Forms.Label lbl_selfCheck;
        private System.Windows.Forms.Panel panel_selfCheck;
        private System.Windows.Forms.Panel panel_contacts;
        private System.Windows.Forms.Panel panel_selfInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel_about;
        private System.Windows.Forms.Panel panel_settings;
        private System.Windows.Forms.Panel panel_register;
        private System.Windows.Forms.Panel panel_logout;
        private System.Windows.Forms.Label lbl_loc;
        private System.Windows.Forms.Label lbl_imStatus;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_quit;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

