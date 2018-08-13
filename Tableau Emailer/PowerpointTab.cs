using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Behold_Emailer
{
    public partial class MainWindow : Form
    {
        private void pickPowerpointInputFile_Click(object sender, EventArgs e)
        {
            DialogResult result = powerPointListPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = powerPointListPicker.FileName;
                // Graciously taken from https://social.msdn.microsoft.com/Forums/vstudio/en-US/859ff0ed-40f9-41df-bf81-b8413465d053/csv-import-using-c?forum=csharpgeneral
                System.Data.Odbc.OdbcConnection conn;
                DataTable dt = new DataTable();
                System.Data.Odbc.OdbcDataAdapter da;
                string file = System.IO.Path.GetFileName(filename);
                string folder = System.IO.Path.GetDirectoryName(filename);

                // Gotta construct a schema.ini file that specifies everything come in as text
                // http://stackoverflow.com/questions/1688497/load-csv-into-oledb-and-force-all-inferred-datatypes-to-string

                // Open connection once to get the schema info
                conn = new System.Data.Odbc.OdbcConnection(@"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + folder + ";Extensions=asc,csv,tab,txt;Persist Security Info=False;");
                da = new System.Data.Odbc.OdbcDataAdapter("select * from [" + file + "]", conn);
                da.Fill(dt);

                StringBuilder schema = new StringBuilder();
                schema.AppendLine("[" + file + "]");
                schema.AppendLine("ColNameHeader=True");
                // Validate that the minimum headers exist
                Dictionary<string, bool> requiredFields = new Dictionary<string, bool> { {"Slide Number", false }, {"Site", false}, {"View Location", false}};
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (requiredFields.ContainsKey(dt.Columns[i].ColumnName))
                    {
                        requiredFields[dt.Columns[i].ColumnName] = true;
                    }
                }

                // Break if headers not correct

                if (requiredFields.ContainsValue(false))
                {
                    MessageBox.Show("CSV headers are not correct. The correct format is:\nSlide Number,Site,View Location,Filter Field Name 1,Filter Values 1,...");
                    return;
                }

                //schema.AppendLine("col1=\"Status\" Text");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    schema.AppendLine("col" + (i + 1).ToString() + "=\"" + dt.Columns[i].ColumnName + "\" Text");
                }
                string schemaFileName = folder + @"\Schema.ini";
                TextWriter tw = new StreamWriter(schemaFileName);
                tw.WriteLine(schema.ToString());
                this.Logger.Log(schema.ToString());
                tw.Close();

                // Open again, schema.ini should be in use
                DataTable dt2 = new DataTable();
                conn = new System.Data.Odbc.OdbcConnection(@"Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + folder + ";Extensions=asc,csv,tab,txt;Persist Security Info=False;");
                da = new System.Data.Odbc.OdbcDataAdapter("select '' AS \"Status\", * from [" + file + "]", conn);
                da.Fill(dt2);
                powerPointStatusView.DataSource = dt2;

                powerPointStatusView.Visible = true;
                // Don't enable sending until something is loaded
                updatePowerPointButton.Enabled = true;
                // Clean out the schema file
                File.Delete(schemaFileName);
            }
        }

        private void fillInPowerpoint_Click(object sender, EventArgs e)
        {
            var activityId = this.nextActivityId;
            this.nextActivityId++;
            WriteToActivityGrid("Queuing a PowerPoint template to be filled in", activityId);
            loadPowerPointListFile.Enabled = false;
            updatePowerPointButton.Enabled = false;
            // Add to the actionQueue. Throw an anonymous function so you can call whatever params at this time
            asyncActionQueue.Add(() =>
            {
                WriteToActivityGrid("Starting to fill in PowerPoint template", activityId);
                var results = fillInPowerpoint(activityId);
                WriteToActivityGrid(results, activityId);
                return results;
            });

            // Launch the next action in the queue, if possible
            try
            {
                actionQueueBackgroundWorker.RunWorkerAsync(asyncActionQueue[0]);
            }
            // The queue response will launch the next action once it is finished
            catch (InvalidOperationException)
            {
                this.Logger.Log("Action queued but other action currently running");
            }
        }

        private string fillInPowerpoint(uint activityId)
        {
            // Copy to the new file
            string ExportArchiveFolderPath = null;
            if (!String.IsNullOrEmpty(Configurator.GetConfig("export_archive_folder")))
            {
                ExportArchiveFolderPath = Configurator.GetConfig("export_archive_folder");
            }
            // Cancel action if no archive folder exists
            else
            {
                MessageBox.Show("Please configure the local save folder", "No Local Configuration",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new ConfigurationException("Please configure the local save folder");
            }


            // Add check for .PPT in the name before final

            string[] filenameNoExtension = powerPointExportFileName.Text.Split('.');

            var finalFilename = ExportArchiveFolderPath + filenameNoExtension[0] + ".pptx";

            // Create the new file
            try
            {
                this.Logger.Log(String.Format("Copying template from {1} to filename {0}", finalFilename, powerPointFilename.Text));
                File.Copy(powerPointFilename.Text, finalFilename, true);

            }
            catch(Exception e){
                this.Logger.Log(e.Message);
            }

            // Reset the sending messages
            foreach (DataGridViewRow row in powerPointStatusView.Rows)
            {
                row.Cells["Status"].Value = "";
            }
            // Run through and send the e-mails
            int rowCount = 0;
            // Catch exception?
            this.Logger.Log("Opening the PowerPointer object");
            var powerpointer = new PowerPointer(finalFilename, this.Logger);
            this.Logger.Log("Opened the PowerPointer object");
            foreach (DataGridViewRow row in powerPointStatusView.Rows)
            {
                row.Cells["Status"].Value = "Processing...";
                row.Selected = true;
                string rawSlideNumber = (string)row.Cells["Slide Number"].Value.ToString();
                if (rawSlideNumber == null)
                {
                    row.Cells["Status"].Value = "Invalid";
                    WriteToActivityGrid(String.Format("Skipped row {0} due to missing Slide Number field", rowCount), activityId);
                    continue;
                }
                int slideNumber = Int32.Parse(rawSlideNumber);

                string viewLocation = (string)row.Cells["View Location"].Value.ToString();
                string site = (string)row.Cells["Site"].Value.ToString();

                // Skip if there is no view location or site
                if (viewLocation == "" || site == "")
                {
                    row.Cells["Status"].Value = "Invalid";
                    row.Selected = false;
                    WriteToActivityGrid(String.Format("Skipped row {0} due to missing View Location or Site", rowCount), activityId);
                    continue;
                }

                Dictionary<string, string> filters_dict = new Dictionary<string, string>();

                // Up to 25 filters (no one would realistically go this high)
                int j = 1;
                while (j <= 25)
                {
                    string filterFieldKey = String.Format("Filter Field Name {0}", j.ToString());
                    string filterValuesKey = String.Format("Filter Values {0}", j.ToString());
                    if (!powerPointStatusView.Columns.Contains(filterFieldKey) || !powerPointStatusView.Columns.Contains(filterValuesKey))
                    {
                        break;
                    }
                    if (row.Cells[filterFieldKey].ValueType != typeof(DBNull))
                    {
                        string filterFieldName = (string)row.Cells[filterFieldKey].Value.ToString();

                        string filterValuesListRaw = (string)row.Cells[filterValuesKey].Value.ToString();

                        // Swap the semi-colons for commas as needed in the dict
                        string[] filterValuesList = filterValuesListRaw.Split(';');
                        // Skip if there's nothing in the first split value
                        if (filterValuesList[0] == "")
                        {
                            j++;
                            continue;
                        }
                        string[] encodedFilters = new string[filterValuesList.Length];
                        for (int i = 0; i < filterValuesList.Length; i++)
                        {
                            // Gotta double the % sign because batch files use %2 as a replacement token.
                            encodedFilters[i] = Uri.EscapeUriString(filterValuesList[i]).Replace("%", "%%");
                        }
                        // Figure out how not to add if empty
                        string finalValueParam = String.Join(",", encodedFilters);

                        filters_dict.Add(filterFieldName, finalValueParam);
                    }
                    j++;
                }

                // Open PowerPointer object on newly copied file

                // Save the Presentation
                try {
                    var slidePart = powerpointer.FindSlidePartBySlideNumber(slideNumber);
                    // Generate the image file
                    this.Logger.Log("Generating the image file to swap in");
                    var tempImageFileName = String.Format("Image {0}", rowCount);
                    this.GenerateSingleExport(site, powerPointUserToGenerateAs.Text, viewLocation, "png", tempImageFileName, filters_dict);
                    this.Logger.Log("PNG file generated");

                    var finalImageFileName = ExportArchiveFolderPath + tempImageFileName + ".png";
                    this.Logger.Log("Replacing the image in the slide");
                    var replacementSuccess = powerpointer.ReplaceImageInSlide(slidePart, finalImageFileName);
                    this.Logger.Log("Replaced the image in the slide");
                    File.Delete(finalImageFileName);
                    if (replacementSuccess)
                    {
                        row.Cells["Status"].Value = "Replaced Successfully";
                    }
                    else
                    {
                        row.Cells["Status"].Value = "No Replacement";
                    }
                }
                catch (NotFoundException)
                {
                    row.Cells["Status"].Value = "Invalid Slide Number";
                }

                row.Selected = false;
                rowCount++;
            }
            powerpointer.SavePresentation();
            powerpointer.ClosePresentation();
            EnablePowerpointButtons();

            return "Finished filling in PowerPoint template file";
        }
    }
}