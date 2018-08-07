namespace Behold_Emailer
{
    partial class ConfigureEmailServer
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
            this.label22 = new System.Windows.Forms.Label();
            this.smtpServerTLS = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.smtpServerPort = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.smtpServerPassword = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.smtpServerUsername = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.htmlEmailFilename = new System.Windows.Forms.TextBox();
            this.textEmailFilename = new System.Windows.Forms.TextBox();
            this.emailServer = new System.Windows.Forms.TextBox();
            this.emailSender = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.htmlEmailPicker = new System.Windows.Forms.OpenFileDialog();
            this.textEmailPicker = new System.Windows.Forms.OpenFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveLocalCopyCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 186);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 13);
            this.label22.TabIndex = 93;
            this.label22.Text = "Email Body Templates";
            // 
            // smtpServerTLS
            // 
            this.smtpServerTLS.FormattingEnabled = true;
            this.smtpServerTLS.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.smtpServerTLS.Location = new System.Drawing.Point(188, 152);
            this.smtpServerTLS.Name = "smtpServerTLS";
            this.smtpServerTLS.Size = new System.Drawing.Size(74, 21);
            this.smtpServerTLS.TabIndex = 92;
            this.smtpServerTLS.Text = "No";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(185, 136);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 13);
            this.label21.TabIndex = 91;
            this.label21.Text = "Use TLS/SSL?";
            // 
            // smtpServerPort
            // 
            this.smtpServerPort.Location = new System.Drawing.Point(15, 152);
            this.smtpServerPort.Name = "smtpServerPort";
            this.smtpServerPort.Size = new System.Drawing.Size(152, 20);
            this.smtpServerPort.TabIndex = 89;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 136);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(160, 13);
            this.label20.TabIndex = 90;
            this.label20.Text = "SMTP Server Port (if not default)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 13);
            this.label19.TabIndex = 88;
            this.label19.Text = "SMTP Server Options";
            // 
            // smtpServerPassword
            // 
            this.smtpServerPassword.Location = new System.Drawing.Point(188, 104);
            this.smtpServerPassword.Name = "smtpServerPassword";
            this.smtpServerPassword.Size = new System.Drawing.Size(152, 20);
            this.smtpServerPassword.TabIndex = 86;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(185, 88);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 13);
            this.label18.TabIndex = 87;
            this.label18.Text = "SMTP Server Password";
            // 
            // smtpServerUsername
            // 
            this.smtpServerUsername.Location = new System.Drawing.Point(15, 104);
            this.smtpServerUsername.Name = "smtpServerUsername";
            this.smtpServerUsername.Size = new System.Drawing.Size(152, 20);
            this.smtpServerUsername.TabIndex = 84;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 88);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(122, 13);
            this.label17.TabIndex = 85;
            this.label17.Text = "SMTP Server Username";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(265, 251);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 83;
            this.button6.Text = "Browse";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.HtmlEmailTemplateButton_Click);
            // 
            // htmlEmailFilename
            // 
            this.htmlEmailFilename.Location = new System.Drawing.Point(188, 225);
            this.htmlEmailFilename.Name = "htmlEmailFilename";
            this.htmlEmailFilename.Size = new System.Drawing.Size(152, 20);
            this.htmlEmailFilename.TabIndex = 77;
            // 
            // textEmailFilename
            // 
            this.textEmailFilename.Location = new System.Drawing.Point(15, 225);
            this.textEmailFilename.Name = "textEmailFilename";
            this.textEmailFilename.Size = new System.Drawing.Size(152, 20);
            this.textEmailFilename.TabIndex = 76;
            // 
            // emailServer
            // 
            this.emailServer.Location = new System.Drawing.Point(15, 25);
            this.emailServer.Name = "emailServer";
            this.emailServer.Size = new System.Drawing.Size(152, 20);
            this.emailServer.TabIndex = 74;
            // 
            // emailSender
            // 
            this.emailSender.Location = new System.Drawing.Point(179, 25);
            this.emailSender.Name = "emailSender";
            this.emailSender.Size = new System.Drawing.Size(152, 20);
            this.emailSender.TabIndex = 75;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(185, 209);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(131, 13);
            this.label30.TabIndex = 82;
            this.label30.Text = "HTML Email Template File";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(97, 251);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 81;
            this.button5.Text = "Browse";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.TextEmailTemplateButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 79;
            this.label9.Text = "SMTP Server";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 78;
            this.label8.Text = "Text Email Template File";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(176, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 13);
            this.label11.TabIndex = 80;
            this.label11.Text = "Email Sender Address";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(290, 325);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 94;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(209, 325);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 95;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // saveLocalCopyCheckBox
            // 
            this.saveLocalCopyCheckBox.AutoSize = true;
            this.saveLocalCopyCheckBox.Checked = true;
            this.saveLocalCopyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.saveLocalCopyCheckBox.Location = new System.Drawing.Point(6, 291);
            this.saveLocalCopyCheckBox.Name = "saveLocalCopyCheckBox";
            this.saveLocalCopyCheckBox.Size = new System.Drawing.Size(189, 17);
            this.saveLocalCopyCheckBox.TabIndex = 97;
            this.saveLocalCopyCheckBox.Text = "Save Local Copy of Emailed Files?";
            this.saveLocalCopyCheckBox.UseVisualStyleBackColor = true;
            this.saveLocalCopyCheckBox.CheckedChanged += new System.EventHandler(this.saveLocalCopyCheckBox_CheckedChanged);
            // 
            // ConfigureEmailServer
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 360);
            this.Controls.Add(this.saveLocalCopyCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.smtpServerTLS);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.smtpServerPort);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.smtpServerPassword);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.smtpServerUsername);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.htmlEmailFilename);
            this.Controls.Add(this.textEmailFilename);
            this.Controls.Add(this.emailServer);
            this.Controls.Add(this.emailSender);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label11);
            this.Name = "ConfigureEmailServer";
            this.Text = "ConfigureEmailServer";
            this.Load += new System.EventHandler(this.ConfigureEmailServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox smtpServerTLS;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox smtpServerPort;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox smtpServerPassword;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox smtpServerUsername;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox htmlEmailFilename;
        private System.Windows.Forms.TextBox textEmailFilename;
        private System.Windows.Forms.TextBox emailServer;
        private System.Windows.Forms.TextBox emailSender;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.OpenFileDialog htmlEmailPicker;
        private System.Windows.Forms.OpenFileDialog textEmailPicker;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox saveLocalCopyCheckBox;
    }
}