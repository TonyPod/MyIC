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
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_history
            // 
            this.tb_history.Location = new System.Drawing.Point(13, 13);
            this.tb_history.Name = "tb_history";
            this.tb_history.ReadOnly = true;
            this.tb_history.Size = new System.Drawing.Size(449, 217);
            this.tb_history.TabIndex = 0;
            this.tb_history.TabStop = false;
            this.tb_history.Text = "\n2014-12-12 13:00:00\n你好";
            // 
            // tb_input
            // 
            this.tb_input.Location = new System.Drawing.Point(12, 246);
            this.tb_input.Multiline = true;
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(450, 148);
            this.tb_input.TabIndex = 1;
            this.tb_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_input_KeyDown);
            // 
            // btn_send
            // 
            this.btn_send.AllowDrop = true;
            this.btn_send.Location = new System.Drawing.Point(372, 410);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
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
            this.btn_sendOptions.Location = new System.Drawing.Point(446, 410);
            this.btn_sendOptions.Name = "btn_sendOptions";
            this.btn_sendOptions.Size = new System.Drawing.Size(16, 23);
            this.btn_sendOptions.TabIndex = 5;
            this.btn_sendOptions.Text = ".";
            this.btn_sendOptions.UseVisualStyleBackColor = true;
            this.btn_sendOptions.Click += new System.EventHandler(this.btn_sendOptions_Click);
            // 
            // btn_selfCheck
            // 
            this.btn_selfCheck.Location = new System.Drawing.Point(13, 409);
            this.btn_selfCheck.Name = "btn_selfCheck";
            this.btn_selfCheck.Size = new System.Drawing.Size(100, 23);
            this.btn_selfCheck.TabIndex = 6;
            this.btn_selfCheck.Text = "查看该用户自检";
            this.btn_selfCheck.UseVisualStyleBackColor = true;
            this.btn_selfCheck.Click += new System.EventHandler(this.btn_selfCheck_Click);
            // 
            // InstantMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 444);
            this.Controls.Add(this.btn_selfCheck);
            this.Controls.Add(this.btn_sendOptions);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.tb_history);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InstantMessageForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InstantMessageForm_FormClosed);
            this.Load += new System.EventHandler(this.InstantMessageForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
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
    }
}