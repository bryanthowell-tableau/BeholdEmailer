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
    public partial class ConfigureTableauServer : Form
    {
        public ConfigureTableauServer()
        {
            InitializeComponent();

            // Load Settings
            server_admin_username.Text = Configurator.GetConfig("server_admin_username");
            server_password.Text = Configurator.GetConfig("server_admin_password");
            tableau_server_url.Text = Configurator.GetConfig("tableau_server");
            repositoryPW.Text = Configurator.GetConfig("repository_pw");
            tabcmdProgramLocation.Text = Configurator.GetConfig("tabcmd_program_location");
            tabcmdConfigLocation.Text = Configurator.GetConfig("tabcmd_config_location");
        }

        private void ConfigureTableauServer_Load(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Configurator.SetConfig("server_admin_username", server_admin_username.Text);
            Configurator.SetConfig("server_admin_password", server_password.Text);
            Configurator.SetConfig("tableau_server", tableau_server_url.Text);
            Configurator.SetConfig("repository_pw", repositoryPW.Text);
            Configurator.SetConfig("tabcmd_program_location", tabcmdProgramLocation.Text);
            Configurator.SetConfig("tabcmd_config_location", tabcmdConfigLocation.Text);

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

        private void pickTabcmdConfigFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = tabcmdFolderPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = tabcmdFolderPicker.SelectedPath + "\\";
                this.tabcmdConfigLocation.Text = filename;
            }
        }

        private void pickTabcmdProgramFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = tabcmdProgramPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = tabcmdProgramPicker.SelectedPath + "\\";
                this.tabcmdProgramLocation.Text = filename;
            }
        }
    }
}
