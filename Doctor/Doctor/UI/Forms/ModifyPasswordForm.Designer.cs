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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "旧密码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "新密码";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "再次输入";
            // 
            // tb_oldPassword
            // 
            this.tb_oldPassword.Location = new System.Drawing.Point(100, 10);
            this.tb_oldPassword.Name = "tb_oldPassword";
            this.tb_oldPassword.PasswordChar = '*';
            this.tb_oldPassword.Size = new System.Drawing.Size(112, 21);
            this.tb_oldPassword.TabIndex = 1;
            this.tb_oldPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // tb_newPassword
            // 
            this.tb_newPassword.Location = new System.Drawing.Point(100, 37);
            this.tb_newPassword.Name = "tb_newPassword";
            this.tb_newPassword.PasswordChar = '*';
            this.tb_newPassword.Size = new System.Drawing.Size(112, 21);
            this.tb_newPassword.TabIndex = 2;
            this.tb_newPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // tb_newPasswordAgain
            // 
            this.tb_newPasswordAgain.Location = new System.Drawing.Point(100, 64);
            this.tb_newPasswordAgain.Name = "tb_newPasswordAgain";
            this.tb_newPasswordAgain.PasswordChar = '*';
            this.tb_newPasswordAgain.Size = new System.Drawing.Size(112, 21);
            this.tb_newPasswordAgain.TabIndex = 3;
            this.tb_newPasswordAgain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_KeyDown);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(42, 100);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 4;
            this.btn_confirm.Text = "确认";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(123, 100);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // ModifyPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 135);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.tb_newPasswordAgain);
            this.Controls.Add(this.tb_newPassword);
            this.Controls.Add(this.tb_oldPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ModifyPasswordForm";
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.ModifyPasswordForm_Load);
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
    }
}