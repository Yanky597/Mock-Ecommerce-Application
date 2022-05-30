namespace TermProjectMCON368
{
    partial class Form1
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
            this.WelcomeGuestlbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loginBx = new System.Windows.Forms.GroupBox();
            this.Usernamelbl = new System.Windows.Forms.Label();
            this.UsernametextBox = new System.Windows.Forms.TextBox();
            this.passwordlbl = new System.Windows.Forms.Label();
            this.passwordTextBx = new System.Windows.Forms.TextBox();
            this.loginBTN = new System.Windows.Forms.Button();
            this.displayUsernamelbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.loginBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // WelcomeGuestlbl
            // 
            this.WelcomeGuestlbl.AutoSize = true;
            this.WelcomeGuestlbl.Location = new System.Drawing.Point(1320, 35);
            this.WelcomeGuestlbl.Name = "WelcomeGuestlbl";
            this.WelcomeGuestlbl.Size = new System.Drawing.Size(164, 25);
            this.WelcomeGuestlbl.TabIndex = 2;
            this.WelcomeGuestlbl.Text = "Welcome Guest";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.displayUsernamelbl);
            this.panel1.Location = new System.Drawing.Point(411, 255);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 531);
            this.panel1.TabIndex = 3;
            // 
            // loginBx
            // 
            this.loginBx.BackColor = System.Drawing.Color.White;
            this.loginBx.Controls.Add(this.Usernamelbl);
            this.loginBx.Controls.Add(this.UsernametextBox);
            this.loginBx.Controls.Add(this.passwordlbl);
            this.loginBx.Controls.Add(this.passwordTextBx);
            this.loginBx.Controls.Add(this.loginBTN);
            this.loginBx.Location = new System.Drawing.Point(495, 329);
            this.loginBx.Name = "loginBx";
            this.loginBx.Size = new System.Drawing.Size(535, 367);
            this.loginBx.TabIndex = 4;
            this.loginBx.TabStop = false;
            this.loginBx.Text = "Login";
            // 
            // Usernamelbl
            // 
            this.Usernamelbl.AutoSize = true;
            this.Usernamelbl.Location = new System.Drawing.Point(204, 64);
            this.Usernamelbl.Name = "Usernamelbl";
            this.Usernamelbl.Size = new System.Drawing.Size(110, 25);
            this.Usernamelbl.TabIndex = 4;
            this.Usernamelbl.Text = "Username";
            // 
            // UsernametextBox
            // 
            this.UsernametextBox.Location = new System.Drawing.Point(174, 108);
            this.UsernametextBox.Name = "UsernametextBox";
            this.UsernametextBox.Size = new System.Drawing.Size(173, 31);
            this.UsernametextBox.TabIndex = 3;
            // 
            // passwordlbl
            // 
            this.passwordlbl.AutoSize = true;
            this.passwordlbl.Location = new System.Drawing.Point(208, 167);
            this.passwordlbl.Name = "passwordlbl";
            this.passwordlbl.Size = new System.Drawing.Size(106, 25);
            this.passwordlbl.TabIndex = 2;
            this.passwordlbl.Text = "Password";
            // 
            // passwordTextBx
            // 
            this.passwordTextBx.Location = new System.Drawing.Point(174, 209);
            this.passwordTextBx.Name = "passwordTextBx";
            this.passwordTextBx.Size = new System.Drawing.Size(173, 31);
            this.passwordTextBx.TabIndex = 1;
            // 
            // loginBTN
            // 
            this.loginBTN.Location = new System.Drawing.Point(175, 272);
            this.loginBTN.Name = "loginBTN";
            this.loginBTN.Size = new System.Drawing.Size(173, 52);
            this.loginBTN.TabIndex = 0;
            this.loginBTN.Text = "Submit";
            this.loginBTN.UseVisualStyleBackColor = true;
            this.loginBTN.Click += new System.EventHandler(this.loginBTN_Click);
            // 
            // displayUsernamelbl
            // 
            this.displayUsernamelbl.AutoSize = true;
            this.displayUsernamelbl.Location = new System.Drawing.Point(238, 467);
            this.displayUsernamelbl.Name = "displayUsernamelbl";
            this.displayUsernamelbl.Size = new System.Drawing.Size(0, 20);
            this.displayUsernamelbl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1525, 1025);
            this.Controls.Add(this.loginBx);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.WelcomeGuestlbl);
            this.Name = "Form1";
            this.Text = "Essentials.com";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.loginBx.ResumeLayout(false);
            this.loginBx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label WelcomeGuestlbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox loginBx;
        private System.Windows.Forms.Label Usernamelbl;
        private System.Windows.Forms.TextBox UsernametextBox;
        private System.Windows.Forms.Label passwordlbl;
        private System.Windows.Forms.TextBox passwordTextBx;
        private System.Windows.Forms.Button loginBTN;
        private System.Windows.Forms.Label displayUsernamelbl;
    }
}

