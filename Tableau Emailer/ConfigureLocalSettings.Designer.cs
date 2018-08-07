namespace Behold_Emailer
{
    partial class ConfigureLocalSettings
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.exportArchiveFolder = new System.Windows.Forms.TextBox();
            this.archiveFolderPicker = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(235, 282);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 99;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(316, 282);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 98;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(89, 69);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 102;
            this.button4.Text = "Browse";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(12, 24);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(135, 13);
            this.label34.TabIndex = 101;
            this.label34.Text = "Export / File Archive Folder";
            // 
            // exportArchiveFolder
            // 
            this.exportArchiveFolder.Location = new System.Drawing.Point(12, 43);
            this.exportArchiveFolder.Name = "exportArchiveFolder";
            this.exportArchiveFolder.Size = new System.Drawing.Size(152, 20);
            this.exportArchiveFolder.TabIndex = 100;
            // 
            // archiveFolderPicker
            // 
            this.archiveFolderPicker.ShowNewFolderButton = false;
            // 
            // ConfigureLocalSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 317);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.exportArchiveFolder);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Name = "ConfigureLocalSettings";
            this.Text = "Local Settings Configuration";
            this.Load += new System.EventHandler(this.ConfigureLocalSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox exportArchiveFolder;
        private System.Windows.Forms.FolderBrowserDialog archiveFolderPicker;
    }
}