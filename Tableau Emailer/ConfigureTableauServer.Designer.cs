namespace Behold_Emailer
{
    partial class ConfigureTableauServer
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
            this.pickTabcmdProgramFolder = new System.Windows.Forms.Button();
            this.pickTabcmdConfigFolder = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.tabcmdProgramLocation = new System.Windows.Forms.TextBox();
            this.tabcmdConfigLocation = new System.Windows.Forms.TextBox();
            this.tableau_server_url = new System.Windows.Forms.TextBox();
            this.server_admin_username = new System.Windows.Forms.TextBox();
            this.server_password = new System.Windows.Forms.TextBox();
            this.repositoryPW = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.TableauServerUrlLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.tabcmdFolderPicker = new System.Windows.Forms.FolderBrowserDialog();
            this.tabcmdProgramPicker = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // pickTabcmdProgramFolder
            // 
            this.pickTabcmdProgramFolder.Location = new System.Drawing.Point(89, 171);
            this.pickTabcmdProgramFolder.Name = "pickTabcmdProgramFolder";
            this.pickTabcmdProgramFolder.Size = new System.Drawing.Size(75, 23);
            this.pickTabcmdProgramFolder.TabIndex = 61;
            this.pickTabcmdProgramFolder.Text = "Browse";
            this.pickTabcmdProgramFolder.UseVisualStyleBackColor = true;
            this.pickTabcmdProgramFolder.Click += new System.EventHandler(this.pickTabcmdProgramFolder_Click);
            // 
            // pickTabcmdConfigFolder
            // 
            this.pickTabcmdConfigFolder.Location = new System.Drawing.Point(270, 171);
            this.pickTabcmdConfigFolder.Name = "pickTabcmdConfigFolder";
            this.pickTabcmdConfigFolder.Size = new System.Drawing.Size(75, 23);
            this.pickTabcmdConfigFolder.TabIndex = 60;
            this.pickTabcmdConfigFolder.Text = "Browse";
            this.pickTabcmdConfigFolder.UseVisualStyleBackColor = true;
            this.pickTabcmdConfigFolder.Click += new System.EventHandler(this.pickTabcmdConfigFolder_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(12, 126);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(132, 13);
            this.label26.TabIndex = 59;
            this.label26.Text = "Tabcmd Program Location";
            // 
            // tabcmdProgramLocation
            // 
            this.tabcmdProgramLocation.Location = new System.Drawing.Point(15, 145);
            this.tabcmdProgramLocation.Name = "tabcmdProgramLocation";
            this.tabcmdProgramLocation.Size = new System.Drawing.Size(152, 20);
            this.tabcmdProgramLocation.TabIndex = 53;
            // 
            // tabcmdConfigLocation
            // 
            this.tabcmdConfigLocation.Location = new System.Drawing.Point(193, 145);
            this.tabcmdConfigLocation.Name = "tabcmdConfigLocation";
            this.tabcmdConfigLocation.Size = new System.Drawing.Size(152, 20);
            this.tabcmdConfigLocation.TabIndex = 54;
            // 
            // tableau_server_url
            // 
            this.tableau_server_url.BackColor = System.Drawing.Color.White;
            this.tableau_server_url.Location = new System.Drawing.Point(15, 25);
            this.tableau_server_url.Name = "tableau_server_url";
            this.tableau_server_url.Size = new System.Drawing.Size(336, 20);
            this.tableau_server_url.TabIndex = 48;
            // 
            // server_admin_username
            // 
            this.server_admin_username.Location = new System.Drawing.Point(15, 64);
            this.server_admin_username.Name = "server_admin_username";
            this.server_admin_username.Size = new System.Drawing.Size(152, 20);
            this.server_admin_username.TabIndex = 49;
            // 
            // server_password
            // 
            this.server_password.Location = new System.Drawing.Point(199, 64);
            this.server_password.Name = "server_password";
            this.server_password.PasswordChar = '*';
            this.server_password.Size = new System.Drawing.Size(152, 20);
            this.server_password.TabIndex = 50;
            // 
            // repositoryPW
            // 
            this.repositoryPW.Location = new System.Drawing.Point(15, 103);
            this.repositoryPW.Name = "repositoryPW";
            this.repositoryPW.PasswordChar = '*';
            this.repositoryPW.Size = new System.Drawing.Size(152, 20);
            this.repositoryPW.TabIndex = 51;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(193, 126);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(142, 13);
            this.label25.TabIndex = 58;
            this.label25.Text = "Tabcmd Config File Location";
            // 
            // TableauServerUrlLabel
            // 
            this.TableauServerUrlLabel.AutoSize = true;
            this.TableauServerUrlLabel.Location = new System.Drawing.Point(12, 9);
            this.TableauServerUrlLabel.Name = "TableauServerUrlLabel";
            this.TableauServerUrlLabel.Size = new System.Drawing.Size(105, 13);
            this.TableauServerUrlLabel.TabIndex = 52;
            this.TableauServerUrlLabel.Text = "Tableau Server URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Admin Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 56;
            this.label3.Text = "Admin Password";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(12, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(184, 13);
            this.label10.TabIndex = 57;
            this.label10.Text = "Repository \"readonly\" User Password";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(223, 214);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 97;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(304, 214);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 96;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // tabcmdFolderPicker
            // 
            this.tabcmdFolderPicker.ShowNewFolderButton = false;
            // 
            // tabcmdProgramPicker
            // 
            this.tabcmdProgramPicker.ShowNewFolderButton = false;
            // 
            // ConfigureTableauServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 248);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.pickTabcmdProgramFolder);
            this.Controls.Add(this.pickTabcmdConfigFolder);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.tabcmdProgramLocation);
            this.Controls.Add(this.tabcmdConfigLocation);
            this.Controls.Add(this.tableau_server_url);
            this.Controls.Add(this.server_admin_username);
            this.Controls.Add(this.server_password);
            this.Controls.Add(this.repositoryPW);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.TableauServerUrlLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Name = "ConfigureTableauServer";
            this.Text = "Tableau Server Configuration Settings";
            this.Load += new System.EventHandler(this.ConfigureTableauServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pickTabcmdProgramFolder;
        private System.Windows.Forms.Button pickTabcmdConfigFolder;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tabcmdProgramLocation;
        private System.Windows.Forms.TextBox tabcmdConfigLocation;
        private System.Windows.Forms.TextBox tableau_server_url;
        private System.Windows.Forms.TextBox server_admin_username;
        private System.Windows.Forms.TextBox server_password;
        private System.Windows.Forms.TextBox repositoryPW;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label TableauServerUrlLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.FolderBrowserDialog tabcmdFolderPicker;
        private System.Windows.Forms.FolderBrowserDialog tabcmdProgramPicker;
    }
}