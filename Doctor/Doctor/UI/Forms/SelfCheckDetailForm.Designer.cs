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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 21);
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
            this.label3.Location = new System.Drawing.Point(13, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "自检图片";
            // 
            // tb_description
            // 
            this.tb_description.Location = new System.Drawing.Point(13, 37);
            this.tb_description.Multiline = true;
            this.tb_description.Name = "tb_description";
            this.tb_description.ReadOnly = true;
            this.tb_description.Size = new System.Drawing.Size(474, 50);
            this.tb_description.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_description);
            this.groupBox1.Controls.Add(this.flowLayoutPanel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 248);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "自检信息";
            // 
            // tb_comment
            // 
            this.tb_comment.Location = new System.Drawing.Point(28, 426);
            this.tb_comment.Multiline = true;
            this.tb_comment.Name = "tb_comment";
            this.tb_comment.Size = new System.Drawing.Size(474, 91);
            this.tb_comment.TabIndex = 5;
            this.tb_comment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_comment_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_ifAuth);
            this.groupBox2.Controls.Add(this.tb_prevComment);
            this.groupBox2.Location = new System.Drawing.Point(15, 290);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 233);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "医生意见";
            // 
            // lbl_ifAuth
            // 
            this.lbl_ifAuth.AutoSize = true;
            this.lbl_ifAuth.ForeColor = System.Drawing.Color.Red;
            this.lbl_ifAuth.Location = new System.Drawing.Point(13, 122);
            this.lbl_ifAuth.Name = "lbl_ifAuth";
            this.lbl_ifAuth.Size = new System.Drawing.Size(173, 12);
            this.lbl_ifAuth.TabIndex = 0;
            this.lbl_ifAuth.Text = "您没有通过认证，不能发布意见";
            // 
            // tb_prevComment
            // 
            this.tb_prevComment.Location = new System.Drawing.Point(13, 20);
            this.tb_prevComment.Multiline = true;
            this.tb_prevComment.Name = "tb_prevComment";
            this.tb_prevComment.ReadOnly = true;
            this.tb_prevComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_prevComment.Size = new System.Drawing.Size(474, 99);
            this.tb_prevComment.TabIndex = 5;
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(358, 529);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(75, 23);
            this.btn_submit.TabIndex = 7;
            this.btn_submit.Text = "提交";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(439, 529);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_username
            // 
            this.btn_username.AutoSize = true;
            this.btn_username.Location = new System.Drawing.Point(81, 14);
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
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "用户名：";
            // 
            // SelfCheckDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 557);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_username);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.tb_comment);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SelfCheckDetailForm";
            this.Text = "自检详情";
            this.Load += new System.EventHandler(this.SelfCheckDetailForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}