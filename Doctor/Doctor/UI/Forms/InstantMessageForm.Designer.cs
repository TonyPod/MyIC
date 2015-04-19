namespace Doctor.Forms
{
    partial class InstantMessageForm
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
            this.components = new System.ComponentModel.Container();
            this.tb_history = new System.Windows.Forms.RichTextBox();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_enter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_ctrl = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_sendOptions = new System.Windows.Forms.Button();
            this.btn_selfCheck = new System.Windows.Forms.Button();
            this.picBox_minimize = new System.Windows.Forms.PictureBox();
            this.picBox_close = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_history
            // 
            this.tb_history.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(124)))), ((int)(((byte)(127)))));
            this.tb_history.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_history.Cursor = System.Windows.Forms.Cursors.Default;
            this.tb_history.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_history.Location = new System.Drawing.Point(12, 37);
            this.tb_history.Name = "tb_history";
            this.tb_history.ReadOnly = true;
            this.tb_history.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tb_history.Size = new System.Drawing.Size(449, 307);
            this.tb_history.TabIndex = 0;
            this.tb_history.TabStop = false;
            this.tb_history.Text = "";
            // 
            // tb_input
            // 
            this.tb_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_input.ForeColor = System.Drawing.Color.White;
            this.tb_input.Location = new System.Drawing.Point(13, 350);
            this.tb_input.Multiline = true;
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(450, 77);
            this.tb_input.TabIndex = 1;
            this.tb_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_input_KeyDown);
            // 
            // btn_send
            // 
            this.btn_send.AllowDrop = true;
            this.btn_send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_send.FlatAppearance.BorderSize = 0;
            this.btn_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_send.Location = new System.Drawing.Point(374, 434);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_enter,
            this.menuItem_ctrl});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(188, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            this.contextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // menuItem_enter
            // 
            this.menuItem_enter.Name = "menuItem_enter";
            this.menuItem_enter.Size = new System.Drawing.Size(187, 22);
            this.menuItem_enter.Text = "Enter 发送消息";
            // 
            // menuItem_ctrl
            // 
            this.menuItem_ctrl.Name = "menuItem_ctrl";
            this.menuItem_ctrl.Size = new System.Drawing.Size(187, 22);
            this.menuItem_ctrl.Text = "Ctrl+Enter 发送消息";
            // 
            // btn_sendOptions
            // 
            this.btn_sendOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_sendOptions.FlatAppearance.BorderSize = 0;
            this.btn_sendOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sendOptions.Location = new System.Drawing.Point(447, 434);
            this.btn_sendOptions.Name = "btn_sendOptions";
            this.btn_sendOptions.Size = new System.Drawing.Size(16, 23);
            this.btn_sendOptions.TabIndex = 5;
            this.btn_sendOptions.Text = ".";
            this.btn_sendOptions.UseVisualStyleBackColor = false;
            this.btn_sendOptions.Click += new System.EventHandler(this.btn_sendOptions_Click);
            // 
            // btn_selfCheck
            // 
            this.btn_selfCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_selfCheck.FlatAppearance.BorderSize = 0;
            this.btn_selfCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_selfCheck.Location = new System.Drawing.Point(14, 433);
            this.btn_selfCheck.Name = "btn_selfCheck";
            this.btn_selfCheck.Size = new System.Drawing.Size(100, 23);
            this.btn_selfCheck.TabIndex = 6;
            this.btn_selfCheck.Text = "查看该用户自检";
            this.btn_selfCheck.UseVisualStyleBackColor = false;
            this.btn_selfCheck.Click += new System.EventHandler(this.btn_selfCheck_Click);
            // 
            // picBox_minimize
            // 
            this.picBox_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_minimize.Image = global::Doctor.Properties.Resources.minimize;
            this.picBox_minimize.Location = new System.Drawing.Point(412, 0);
            this.picBox_minimize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.picBox_minimize.Name = "picBox_minimize";
            this.picBox_minimize.Size = new System.Drawing.Size(24, 24);
            this.picBox_minimize.TabIndex = 7;
            this.picBox_minimize.TabStop = false;
            this.picBox_minimize.Click += new System.EventHandler(this.picBox_minimize_Click);
            // 
            // picBox_close
            // 
            this.picBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_close.Image = global::Doctor.Properties.Resources.close2;
            this.picBox_close.Location = new System.Drawing.Point(442, 0);
            this.picBox_close.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.picBox_close.Name = "picBox_close";
            this.picBox_close.Size = new System.Drawing.Size(24, 24);
            this.picBox_close.TabIndex = 7;
            this.picBox_close.TabStop = false;
            this.picBox_close.Click += new System.EventHandler(this.picBox_close_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.picBox_close);
            this.flowLayoutPanel1.Controls.Add(this.picBox_minimize);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-1, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(469, 26);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.flowLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // InstantMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(474, 467);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btn_selfCheck);
            this.Controls.Add(this.btn_sendOptions);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.tb_history);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "InstantMessageForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InstantMessageForm_FormClosed);
            this.Load += new System.EventHandler(this.InstantMessageForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb_history;
        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItem_enter;
        private System.Windows.Forms.ToolStripMenuItem menuItem_ctrl;
        private System.Windows.Forms.Button btn_sendOptions;
        private System.Windows.Forms.Button btn_selfCheck;
        private System.Windows.Forms.PictureBox picBox_close;
        private System.Windows.Forms.PictureBox picBox_minimize;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}