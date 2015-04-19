namespace Doctor.Forms
{
    partial class SelfCheckDetailForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_description = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_comment = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_ifAuth = new System.Windows.Forms.Label();
            this.tb_prevComment = new System.Windows.Forms.TextBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_username = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.picBox_close = new System.Windows.Forms.PictureBox();
            this.picBox_minimize = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(11, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "症状描述";
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(15, 114);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(474, 122);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(13, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "自检图片";
            // 
            // tb_description
            // 
            this.tb_description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.tb_description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_description.Cursor = System.Windows.Forms.Cursors.Default;
            this.tb_description.ForeColor = System.Drawing.Color.White;
            this.tb_description.Location = new System.Drawing.Point(15, 37);
            this.tb_description.Multiline = true;
            this.tb_description.Name = "tb_description";
            this.tb_description.ReadOnly = true;
            this.tb_description.Size = new System.Drawing.Size(470, 50);
            this.tb_description.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_description);
            this.groupBox1.Controls.Add(this.flowLayoutPanel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.groupBox1.Location = new System.Drawing.Point(15, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 248);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自检信息";
            // 
            // tb_comment
            // 
            this.tb_comment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_comment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_comment.Location = new System.Drawing.Point(28, 450);
            this.tb_comment.Multiline = true;
            this.tb_comment.Name = "tb_comment";
            this.tb_comment.Size = new System.Drawing.Size(474, 81);
            this.tb_comment.TabIndex = 5;
            this.tb_comment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_comment_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_ifAuth);
            this.groupBox2.Controls.Add(this.tb_prevComment);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.groupBox2.Location = new System.Drawing.Point(15, 314);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 233);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "医生意见";
            // 
            // lbl_ifAuth
            // 
            this.lbl_ifAuth.AutoSize = true;
            this.lbl_ifAuth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(185)))), ((int)(((byte)(153)))));
            this.lbl_ifAuth.Location = new System.Drawing.Point(13, 122);
            this.lbl_ifAuth.Name = "lbl_ifAuth";
            this.lbl_ifAuth.Size = new System.Drawing.Size(173, 12);
            this.lbl_ifAuth.TabIndex = 0;
            this.lbl_ifAuth.Text = "您没有通过认证，不能发布意见";
            // 
            // tb_prevComment
            // 
            this.tb_prevComment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.tb_prevComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_prevComment.Cursor = System.Windows.Forms.Cursors.Default;
            this.tb_prevComment.ForeColor = System.Drawing.Color.White;
            this.tb_prevComment.Location = new System.Drawing.Point(15, 20);
            this.tb_prevComment.Multiline = true;
            this.tb_prevComment.Name = "tb_prevComment";
            this.tb_prevComment.ReadOnly = true;
            this.tb_prevComment.Size = new System.Drawing.Size(472, 99);
            this.tb_prevComment.TabIndex = 5;
            // 
            // btn_submit
            // 
            this.btn_submit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_submit.FlatAppearance.BorderSize = 0;
            this.btn_submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_submit.ForeColor = System.Drawing.Color.Black;
            this.btn_submit.Location = new System.Drawing.Point(358, 553);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(75, 23);
            this.btn_submit.TabIndex = 7;
            this.btn_submit.Text = "提交";
            this.btn_submit.UseVisualStyleBackColor = false;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(439, 553);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_username
            // 
            this.btn_username.AutoSize = true;
            this.btn_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.btn_username.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.btn_username.Location = new System.Drawing.Point(81, 38);
            this.btn_username.Name = "btn_username";
            this.btn_username.Size = new System.Drawing.Size(23, 12);
            this.btn_username.TabIndex = 4;
            this.btn_username.TabStop = true;
            this.btn_username.Text = "xxx";
            this.btn_username.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_username_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(22, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "用户名：";
            // 
            // picBox_close
            // 
            this.picBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_close.Image = global::Doctor.Properties.Resources.close2;
            this.picBox_close.Location = new System.Drawing.Point(489, 0);
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
            this.picBox_minimize.Location = new System.Drawing.Point(459, 0);
            this.picBox_minimize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.picBox_minimize.Name = "picBox_minimize";
            this.picBox_minimize.Size = new System.Drawing.Size(24, 24);
            this.picBox_minimize.TabIndex = 7;
            this.picBox_minimize.TabStop = false;
            this.picBox_minimize.Click += new System.EventHandler(this.picBox_minimize_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.picBox_close);
            this.flowLayoutPanel1.Controls.Add(this.picBox_minimize);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(516, 26);
            this.flowLayoutPanel1.TabIndex = 9;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.flowLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // SelfCheckDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(526, 584);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_username);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.tb_comment);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "SelfCheckDetailForm";
            this.Text = "自检详情";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelfCheckDetailForm_FormClosed);
            this.Load += new System.EventHandler(this.SelfCheckDetailForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_description;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_comment;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_ifAuth;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox tb_prevComment;
        private System.Windows.Forms.LinkLabel btn_username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBox_close;
        private System.Windows.Forms.PictureBox picBox_minimize;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}