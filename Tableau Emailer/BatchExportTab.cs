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
        private void pickBulkCSVFile_Click(object sender, EventArgs e)
        {
            DialogResult result = batchCsvPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filename = batchCsvPicker.FileName;
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
                Dictionary<string, bool> requiredFields = new Dictionary<string, bool> { {"Attachment Type", false }, {"To:", false}, {"CC:", false}, {"BCC:", false},
                                                                                          {"Site", false}, {"View Location", false}};
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
                    MessageBox.Show("CSV headers are not correct. The correct format is:\nAttchment Type,To:,CC:,BCC:,Site,View Location,Filter Field Name 1,Filter Values 1,...");
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
                bulkEmailPreview.DataSource = dt2;

                bulkEmailPreview.Visible = true;
                // Don't enable sending until something is loaded
                sendBatchEmails.Enabled = true;
                // Clean out the schema file
                File.Delete(schemaFileName);
            }
        }

        private void sendBulkEmails_Click(object sender, EventArgs e)
        {
            var activityId = this.nextActivityId;
            this.nextActivityId++;
            WriteToActivityGrid("Queing a batch send from the currently loaded CSV", activityId);
            pickBulkCSVFile.Enabled = false;
            sendBatchEmails.Enabled = false;
            // Add to the actionQueue. Throw an anonymous function so you can call whatever params at this time
            asyncActionQueue.Add(() =>
            {
                WriteToActivityGrid("Starting a batch send from the currently loaded CSV", activityId);
                var results = sendBulkEmails(activityId);
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

        private string sendBulkEmails(uint activityId)
        {
            // Reset the sending messages
            foreach (DataGridViewRow row in bulkEmailPreview.Rows)
            {
                row.Cells["Status"].Value = "";
            }
            // Run through and send the e-mails
            int rowCount = 0;
            foreach (DataGridViewRow row in bulkEmailPreview.Rows)
            {
                row.Cells["Status"].Value = "Sending...";
                row.Selected = true;
                string rawTo = (string)row.Cells["To:"].Value.ToString();
                if (rawTo == null)
                {
                    WriteToActivityGrid(String.Format("Skipped row {0} due to missing To: field", rowCount), activityId);
                    continue;
                }
                var emailTo = rawTo.Split(';');
                string rawCc = (string)row.Cells["CC:"].Value.ToString();
                var emailCc = rawCc.Split(';');
                string rawBcc = (string)row.Cells["BCC:"].Value.ToString();
                var email_bcc = rawBcc.Split(';');
                string viewLocation = (string)row.Cells["View Location"].Value.ToString();
                string site = (string)row.Cells["Site"].Value.ToString();
                string attachmentType = row.Cells["Attachment Type"].Value.ToString().ToLower();

                // Skip if there is no view location or site
                if (viewLocation == "" || site == "")
                {
                    row.Cells["Status"].Value = "Invalid";
                    row.Selected = false;
                    WriteToActivityGrid(String.Format("Skipped row {0} due to missing View Location or Site", rowCount), activityId);
                    continue;
                }

                // Validate the Attachment Type
                if (Array.IndexOf(ExportTypeIndexMap, attachmentType) == -1)
                {
                    row.Cells["Status"].Value = "Invalid";
                    row.Selected = false;
                    WriteToActivityGrid(String.Format("Skipped row {0} due to incorrect or missing Attachment Type", rowCount), activityId);
                    continue;
                }

                // Implement multiple to

                Dictionary<string, string> filters_dict = new Dictionary<string, string>();

                // Up to 25 filters (no one would realistically go this high)
                int j = 1;
                while (j <= 25)
                {
                    string filterFieldKey = String.Format("Filter Field Name {0}", j.ToString());
                    string filterValuesKey = String.Format("Filter Values {0}", j.ToString());
                    if (!bulkEmailPreview.Columns.Contains(filterFieldKey) || !bulkEmailPreview.Columns.Contains(filterValuesKey))
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

                this.SendEmail(site, emailTo, emailCc, email_bcc, bulkEmailSubject.Text, bulkUsernameToImpersonateAs.Text, viewLocation, filters_dict, attachmentType);
                row.Cells["Status"].Value = "Sent";
                WriteToActivityGrid(String.Format("Sent email of {0} to {1}", viewLocation, emailTo[0]), activityId);
                row.Selected = false;
                rowCount++;
            }
            EnableBatchButtons();
            return "Finished running the batch send";
        }
    }
}