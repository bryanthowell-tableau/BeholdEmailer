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
    public partial class ConfigureLocalSettings : Form
    {
        public ConfigureLocalSettings()
        {
            InitializeComponent();
            exportArchiveFolder.Text = Configurator.GetConfig("export_archive_folder");
        }

        private void ConfigureLocalSettings_Load(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Configurator.SetConfig("export_archive_folder", exportArchiveFolder.Text);
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

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = archiveFolderPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = archiveFolderPicker.SelectedPath + "\\";
                this.exportArchiveFolder.Text = filename;
            }
        }
    }
}
