using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Behold_Emailer
{
    public partial class ConfigureWatermarking : Form
    {
        public string[] WatermarkPageLocationNames;

        public ConfigureWatermarking()
        {
            InitializeComponent();
            this.WatermarkPageLocationNames = new string[] { "topLeft", "topCenter", "topRight", "bottomLeft", "bottomCenter", "bottomRight" };
            LoadPageLayoutConfigs();
        }

        private void ConfigureWatermarking_Load(object sender, EventArgs e)
        {
        }

        private void LoadPageLayoutConfigs()
        {
            foreach (string page_layout_location in this.WatermarkPageLocationNames)
            {
                SerializableStringDictionary watermark_settings = Configurator.GetConfigSerializableStringDict(page_layout_location);
                if (watermark_settings != null)
                {
                    Label label = this.Controls.Find("label_" + page_layout_location, true).FirstOrDefault() as Label;
                    string new_label_text = "";
                    if (watermark_settings["watermark_type"] == "text")
                    {
                        new_label_text = "Text";
                    }
                    else if (watermark_settings["watermark_type"] == "image")
                    {
                        new_label_text = "Image";
                    }
                    else if (watermark_settings["watermark_type"] == "page_number")
                    {
                        new_label_text = "Page Number";
                    }
                    // Don't change labels if no response, exit early
                    else
                    {
                        return;
                    }
                    label.Text = new_label_text;
                    Button button = this.Controls.Find("edit_" + page_layout_location, true).FirstOrDefault() as Button;
                    button.Text = "Edit";
                }
            }
        }

        /*
        * Watermarking Tab Events
        */

        private void EditWatermarkerConfiguration(object sender, EventArgs e)
        {
            Control box = (Control)sender;
            Button button = (Button)sender;

            string page_location = box.Name.Split('_')[1];
            Label label = this.Controls.Find("label_" + page_location, true).FirstOrDefault() as Label;
            if (label.Text == "")
            {
                watermarkContextMenu.Show(button, new Point(0, button.Height));
            }
            else
            {
                SerializableStringDictionary related_config = Configurator.GetConfigSerializableStringDict(page_location);
                if (label.Text == "Text")
                {
                    Form wm = new Text_Watermark(page_location, related_config);
                    wm.ShowDialog(this);
                }
                else if (label.Text == "Page Number")
                {
                    Form wm = new Page_Number_Watermark(page_location, related_config);
                    wm.ShowDialog(this);
                }
                else if (label.Text == "Image")
                {
                    Form wm = new Image_Watermark(page_location, related_config);
                    wm.ShowDialog(this);
                }
            }
        }

        private void ClearWatermarkConfiguration(object sender, EventArgs e)
        {
            Button s = (Button)sender;
            string page_location = s.Name.Split('_')[1];
            DialogResult results = MessageBox.Show("Delete existing watermark configuration?", "Remove Watermark?", MessageBoxButtons.YesNo);
            if (results == DialogResult.Yes)
            {
                SerializableStringDictionary empty_config = new SerializableStringDictionary();
                Configurator.SetConfig(page_location, empty_config);
                Configurator.SaveConfig();
                Label label = this.Controls.Find("label_" + page_location, true).FirstOrDefault() as Label;
                label.Text = "";
                Button button = this.Controls.Find("edit_" + page_location, true).FirstOrDefault() as Button;
                button.Text = "Add";
            }
        }

        private void watermarkContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem m = (ToolStripItem)e.ClickedItem;
            ContextMenuStrip cm = (ContextMenuStrip)m.Owner;
            Control c = cm.SourceControl;
            string page_location = c.Name.Split('_')[1];

            if (e.ClickedItem.Text == "Text")
            {
                Form wm = new Text_Watermark(page_location);
                wm.ShowDialog(this);
            }
            else if (e.ClickedItem.Text == "Page Number")
            {
                Form wm = new Page_Number_Watermark(page_location);
                wm.ShowDialog(this);
            }
            else if (e.ClickedItem.Text == "Image")
            {
                Form wm = new Image_Watermark(page_location);
                wm.ShowDialog(this);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}