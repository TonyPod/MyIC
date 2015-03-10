namespace IMClient_WinForm
{
    partial class IMForm
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
            this.tb_from = new System.Windows.Forms.TextBox();
            this.tb_content = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_to = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_received = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_from
            // 
            this.tb_from.Location = new System.Drawing.Point(50, 12);
            this.tb_from.Name = "tb_from";
            this.tb_from.ReadOnly = true;
            this.tb_from.Size = new System.Drawing.Size(100, 21);
            this.tb_from.TabIndex = 0;
            // 
            // tb_content
            // 
            this.tb_content.Location = new System.Drawing.Point(50, 67);
            this.tb_content.Multiline = true;
            this.tb_content.Name = "tb_content";
            this.tb_content.Size = new System.Drawing.Size(243, 186);
            this.tb_content.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Msg";
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(218, 256);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 3;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_to
            // 
            this.tb_to.Location = new System.Drawing.Point(50, 37);
            this.tb_to.Name = "tb_to";
            this.tb_to.Size = new System.Drawing.Size(100, 21);
            this.tb_to.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "To";
            // 
            // tb_received
            // 
            this.tb_received.Location = new System.Drawing.Point(313, 29);
            this.tb_received.Multiline = true;
            this.tb_received.Name = "tb_received";
            this.tb_received.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_received.Size = new System.Drawing.Size(296, 224);
            this.tb_received.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Received";
            // 
            // IMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 284);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_received);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_content);
            this.Controls.Add(this.tb_to);
            this.Controls.Add(this.tb_from);
            this.Name = "IMForm";
            this.Text = "IM";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IMForm_FormClosed);
            this.Load += new System.EventHandler(this.IMForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_from;
        private System.Windows.Forms.TextBox tb_content;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tb_to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_received;
        private System.Windows.Forms.Label label4;
    }
}

