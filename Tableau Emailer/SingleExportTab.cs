using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Behold_Emailer
{
    public partial class MainWindow : Form
    {
        private void QueueSingleExport(object sender, EventArgs e)
        {
            // Needs validation here
            TextBox[] elementsToValidate = { singleExportSite, singleViewLocation };
            bool validation = this.ValidateSetOfElements(elementsToValidate);
            if (validation == false)
            {
                return;
            }
            var activityId = this.nextActivityId;
            this.nextActivityId++;

            WriteToActivityGrid(String.Format("Queuing export of {0} on site {1}", singleViewLocation.Text, singleExportSite.Text), activityId);

            // Add to the actionQueue. Throw an anonymous function so you can call whatever params at this time
            asyncActionQueue.Add(() =>
            {
                var exportSite = singleExportSite.Text;
                var exportUsername = singleUsernameForImpersonation.Text;
                var exportViewLocation = singleViewLocation.Text;
                var exportAttachmentType = ExportTypeIndexMap[Int32.Parse(Configurator.GetConfig("single_export_type_index"))];
                var exportFilename = testFilename.Text;
                var results = GenerateSingleExport(exportSite, exportUsername, exportViewLocation, exportAttachmentType, exportFilename);
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

        private string GenerateSingleExport(string exportSite, string exportUsername, string exportViewLocation, string exportAttachmentType, string exportFilename)
        {
            // Generate Single File
            try
            {
                TableauRepository tabrep = new TableauRepository(
                    Configurator.GetConfig("tableau_server"),
                    Configurator.GetConfig("repository_pw"),
                    "readonly"
                );
                tabrep.logger = this.Logger;

                Tabcmd tabcmd = new Tabcmd(
                    Configurator.GetConfig("tabcmd_program_location"),
                    Configurator.GetConfig("tableau_server"),
                    Configurator.GetConfig("server_admin_username"),
                    Configurator.GetConfig("server_admin_password"),
                    exportSite,
                    Configurator.GetConfig("tabcmd_config_location"),
                    tabrep,
                    this.Logger
                    );
                // Emailer here is used because the Watermarking is built in there. Would it make more sense to move it to Tabcmd eventually, or its own class?
                BeholdEmailer tabemailer = new BeholdEmailer(tabcmd, Configurator.GetConfig("smtp_server"));
                Watermarker wm = new Watermarker();
                string[] pageLocations = { "top_left", "top_center", "top_right", "bottom_left", "bottom_center", "bottom_right" };
                foreach (string pageLocation in pageLocations)
                {
                    string settingsPageLocation = pageLocation.Split('_')[0] + pageLocation.Split('_')[1].First() + pageLocation.Split('_')[1].Substring(1);
                    wm.SetPageLocationWatermarkFromConfig(pageLocation, Configurator.GetConfigSerializableStringDict(settingsPageLocation));
                }

                string filename = tabemailer.GenerateExportAndWatermark(exportUsername, exportViewLocation,
                   exportAttachmentType, new Dictionary<string, string>(), wm);
                string[] fileEnding = filename.Split('.');

                string finalFilename = String.Format("{0}{1}.{2}", Configurator.GetConfig("export_archive_folder"), exportFilename, fileEnding[fileEnding.Length - 1]);

                this.Logger.Log(String.Format("Finalizing file and putting it here: {0}", finalFilename));
                File.Copy(filename, finalFilename, true);
                this.Logger.Log(String.Format("Removing original file {0}", filename));
                File.Delete(filename);

                return "Finished single export file creation. Saved to: " + finalFilename;
            }
            catch (ConfigurationException ce)
            {
                //progress.finish_progress_bar(33);
                // progress.update_status("Export failed for some reason, most likely bad settings.\nCheck logs for more info");
                this.Logger.Log(ce.Message);
                return "Single export failed. Please see logs for more information.";
            }
        }

        private void QueueSingleEmail(object sender, EventArgs e)
        {
            // Valdate the form elements
            TextBox[] elements_to_validate = { singleExportSite, singleViewLocation, testEmailRecipient, testEmailSubject };
            bool validation = this.ValidateSetOfElements(elements_to_validate);
            if (validation == false)
            {
                return;
            }

            var activityId = this.nextActivityId;
            this.nextActivityId++;
            WriteToActivityGrid(String.Format("Queued an email of {0} to {1}", singleViewLocation.Text, testEmailRecipient.Text), activityId);

            // Add to the actionQueue
            asyncActionQueue.Add(() =>
            {
                var exportSite = singleExportSite.Text;
                var exportUsername = singleUsernameForImpersonation.Text;
                var exportViewLocation = singleViewLocation.Text;
                var exportAttachmentType = ExportTypeIndexMap[Int32.Parse(Configurator.GetConfig("single_export_type_index"))];
                // var exportFilename = testFilename.Text;
                var emailTo = testEmailRecipient.Text;
                var emailSubject = testEmailSubject.Text;
                var results = SendSingleEmail(exportSite, emailTo, emailSubject, exportUsername, exportViewLocation, exportAttachmentType);
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

        private string SendSingleEmail(string singleExportSite, string singleEmailTo, string singleEmailSubject, string exportUsername, string exportViewLocation, string exportAttachmentType)
        {
            bool result = this.SendEmail(singleExportSite, new string[] { singleEmailTo }, new string[] { }, new string[] { }, singleEmailSubject, exportUsername, exportViewLocation, new Dictionary<string, string>(), exportAttachmentType);
            if (result == true)
            {
                return "Single E-mail sent succesfully";
            }
            else
            {
                return "Single E-mail failed. Please check configurations and try again. See log for details";
            }
        }
    }
}