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
            this.tb_history = new System.Windows.Forms.RichTextBox();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_history
            // 
            this.tb_history.Location = new System.Drawing.Point(13, 13);
            this.tb_history.Name = "tb_history";
            this.tb_history.ReadOnly = true;
            this.tb_history.Size = new System.Drawing.Size(449, 217);
            this.tb_history.TabIndex = 0;
            this.tb_history.Text = "\n2014-12-12 13:00:00\n你好";
            // 
            // tb_input
            // 
            this.tb_input.Location = new System.Drawing.Point(12, 246);
            this.tb_input.Multiline = true;
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(450, 148);
            this.tb_input.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.AllowDrop = true;
            this.btn_send.Location = new System.Drawing.Point(387, 409);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // InstantMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 444);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_input);
            this.Controls.Add(this.tb_history);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InstantMessageForm";
            this.Load += new System.EventHandler(this.InstantMessageForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb_history;
        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.Button btn_send;
    }
}