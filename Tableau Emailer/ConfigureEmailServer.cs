using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Behold_Emailer
{
    public partial class ConfigureEmailServer : Form
    {
        public ConfigureEmailServer()
        {
            InitializeComponent();
          
            // Load the configurations
            emailServer.Text = Configurator.GetConfig("smtp_server");
            smtpServerUsername.Text = Configurator.GetConfig("smtp_server_username");
            smtpServerPassword.Text = Configurator.GetConfig("smtp_server_password");
            smtpServerTLS.Text = Configurator.GetConfig("smtp_server_tls");
            smtpServerPort.Text = Configurator.GetConfig("smtp_server_port");
            textEmailFilename.Text = Configurator.GetConfig("text_email_template_filename");
            htmlEmailFilename.Text = Configurator.GetConfig("html_email_template_filename");
            emailSender.Text = Configurator.GetConfig("email_sender");
            saveLocalCopyCheckBox.Checked = Configurator.GetConfigBool("save_emailed_copies_flag");
        }

        private void TextEmailTemplateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = textEmailPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = textEmailPicker.FileName;
                this.textEmailFilename.Text = filename;
            }
        }

        private void HtmlEmailTemplateButton_Click(object sender, EventArgs e)
        {
            DialogResult result = htmlEmailPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = htmlEmailPicker.FileName;
                this.htmlEmailFilename.Text = filename;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Configurator.SetConfig("smtp_server", emailServer.Text);
            Configurator.SetConfig("smtp_server_username", smtpServerUsername.Text);
            Configurator.SetConfig("smtp_server_password", smtpServerPassword.Text);
            Configurator.SetConfig("smtp_server_tls", smtpServerTLS.Text);
            Configurator.SetConfig("smtp_server_port", smtpServerPort.Text);
            Configurator.SetConfig("text_email_template_filename", textEmailFilename.Text);
            Configurator.SetConfig("html_email_template_filename", htmlEmailFilename.Text);
            Configurator.SetConfig("email_sender", emailSender.Text);
            Configurator.SetConfig("save_emailed_copies_flag", saveLocalCopyCheckBox.Checked);

            try
            {
                Configurator.SaveConfig();
                //MessageBox.Show("Settings Saved Successfully!");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Settings were not saved correctly.\n\nPlease check all your entries, retry saving, and look at log files for additional info");
                //this.logger.Log("Saving settings failed");
                //this.logger.Log(exc.Message);
            }
            this.Close();
        }

        private void ConfigureEmailServer_Load(object sender, EventArgs e)
        {

        }

        private void saveLocalCopyCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
