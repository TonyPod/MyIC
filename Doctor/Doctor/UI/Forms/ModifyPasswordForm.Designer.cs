namespace Doctor.Forms
{
    partial class ModifyPasswordForm
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
            this.tb_oldPassword = new System.Windows.Forms.TextBox();
            this.tb_newPassword = new System.Windows.Forms.TextBox();
            this.tb_newPasswordAgain = new System.Windows.Forms.TextBox();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.picBox_close = new System.Windows.Forms.PictureBox();
            this.picBox_minimize = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(19, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "旧密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(19, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "新密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(19, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "再次输入";
            // 
            // tb_oldPassword
            // 
            this.tb_oldPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_oldPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_oldPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_oldPassword.Location = new System.Drawing.Point(105, 33);
            this.tb_oldPassword.Name = "tb_oldPassword";
            this.tb_oldPassword.PasswordChar = '*';
            this.tb_oldPassword.Size = new System.Drawing.Size(112, 21);
            this.tb_oldPassword.TabIndex = 1;
            this.tb_oldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // tb_newPassword
            // 
            this.tb_newPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_newPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_newPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_newPassword.Location = new System.Drawing.Point(105, 60);
            this.tb_newPassword.Name = "tb_newPassword";
            this.tb_newPassword.PasswordChar = '*';
            this.tb_newPassword.Size = new System.Drawing.Size(112, 21);
            this.tb_newPassword.TabIndex = 2;
            this.tb_newPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // tb_newPasswordAgain
            // 
            this.tb_newPasswordAgain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(189)))), ((int)(((byte)(191)))));
            this.tb_newPasswordAgain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_newPasswordAgain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.tb_newPasswordAgain.Location = new System.Drawing.Point(105, 87);
            this.tb_newPasswordAgain.Name = "tb_newPasswordAgain";
            this.tb_newPasswordAgain.PasswordChar = '*';
            this.tb_newPasswordAgain.Size = new System.Drawing.Size(112, 21);
            this.tb_newPasswordAgain.TabIndex = 3;
            this.tb_newPasswordAgain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_confirm.FlatAppearance.BorderSize = 0;
            this.btn_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_confirm.Location = new System.Drawing.Point(40, 123);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 4;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(131, 123);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.picBox_close);
            this.flowLayoutPanel1.Controls.Add(this.picBox_minimize);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 1);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(241, 26);
            this.flowLayoutPanel1.TabIndex = 10;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.flowLayoutPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // picBox_close
            // 
            this.picBox_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_close.Image = global::Doctor.Properties.Resources.close2;
            this.picBox_close.Location = new System.Drawing.Point(214, 0);
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
            this.picBox_minimize.Location = new System.Drawing.Point(184, 0);
            this.picBox_minimize.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.picBox_minimize.Name = "picBox_minimize";
            this.picBox_minimize.Size = new System.Drawing.Size(24, 24);
            this.picBox_minimize.TabIndex = 7;
            this.picBox_minimize.TabStop = false;
            this.picBox_minimize.Click += new System.EventHandler(this.picBox_minimize_Click);
            // 
            // ModifyPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(126)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(239, 158);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.tb_newPasswordAgain);
            this.Controls.Add(this.tb_newPassword);
            this.Controls.Add(this.tb_oldPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "ModifyPasswordForm";
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.ModifyPasswordForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_minimize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_oldPassword;
        private System.Windows.Forms.TextBox tb_newPassword;
        private System.Windows.Forms.TextBox tb_newPasswordAgain;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox picBox_close;
        private System.Windows.Forms.PictureBox picBox_minimize;
    }
}