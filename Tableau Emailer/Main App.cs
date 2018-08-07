using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Behold_Emailer
{
    public partial class MainWindow : Form
    {
        public SimpleLogger Logger;
        private System.Windows.Forms.Timer scheduleMonitorTimer;
        public List<string[]> ActiveSchedules;
        public string ActiveSchedulesQueueFilename;
        public StreamWriter CompletedSchedulesFile;
        private string[] ExportTypeIndexMap;
        private List<queueAction> asyncActionQueue;
        private uint nextActivityId;

        public MainWindow()
        {
            InitializeComponent();
            nextActivityId = 1;

            // Load actions from the queue filename
            asyncActionQueue = new List<queueAction>();

            // Initialize shared logger
            this.Logger = new SimpleLogger("log.txt");

            ExportTypeIndexMap = new string[] { "fullpdf", "pdf", "png", "csv" };

            // Fill in some fields
            singleViewLocation.Text = Configurator.GetConfig("single_export_view_location");
            singleExportSite.Text = Configurator.GetConfig("single_export_site");
            singleUsernameForImpersonation.Text = Configurator.GetConfig("single_export_impersonate_as_user");
            exportTypeDropDown.SelectedIndex = Int32.Parse(Configurator.GetConfig("single_export_type_index"));

            // Initializing queue system
            int minutesBetweenTimer = 1;
            this.scheduleMonitorTimer = new System.Windows.Forms.Timer();
            scheduleMonitorTimer.Interval = minutesBetweenTimer * 60 * 1000;
            this.scheduleMonitorTimer.Tick += queueSchedulesOnTimer;
            this.ActiveSchedulesQueueFilename = "active_schedules.csv";
            string completedSchedulesFilename = "completed_schedules.csv";
            this.ActiveSchedules = new List<string[]>();
            try
            {
                // Read anything in the active_schedules file
                string[] lastActiveSchedules = File.ReadAllLines(ActiveSchedulesQueueFilename);
                foreach (string scheduleLine in lastActiveSchedules)
                {
                    string[] cols = scheduleLine.Split('|');
                    ActiveSchedules.Add(cols);
                    this.Logger.Log(String.Format("Loaded {0} at {1} to active schedules from disk", cols[0], cols[1]));
                }

                // Open up the streams to keep track of what happens even if the program fails

                this.CompletedSchedulesFile = new StreamWriter(completedSchedulesFilename, true, new UTF8Encoding());
                this.CompletedSchedulesFile.AutoFlush = true;
            }
            catch (IOException ex)
            {
                this.Logger.Log(ex.Message);
            }

            // Simple test for first time load with no configs set
            /*if (Configurator.GetConfig("tableau_server") != "")
            {
                // Load the Inactive Subscriptions from the Repository
                LoadSubscriptionsFromRepository();
            }*/
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var schedulesEnabledFlag = Configurator.GetConfigBool("schedules_enabled");
            if (schedulesEnabledFlag)
            {
                enableSchedulesRadioButton.Checked = true;
            }
            else
            {
                disableSchedulesRadioButton.Checked = true;
            }
        }

        // This delegate does any of the live methods, but allows queuing
        public delegate string queueAction();

        /*
         * Loading and Configuration
         */

        private void LoadSubscriptionsFromRepository()
        {
            try
            {
                TableauRepository rep = new TableauRepository(Configurator.GetConfig("tableau_server"),
                Configurator.GetConfig("repository_pw"), "readonly");
                rep.logger = this.Logger;
                NpgsqlDataReader dr = rep.QueryInactiveSubscriptionSchedules();
                Dictionary<string, int> inputBoxSchedules = new Dictionary<string, int>();
                int rowCount = 0;
                availableSchedulesList.Items.Clear();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        availableSchedulesList.Items.Add(dr.GetString(1));
                        inputBoxSchedules[dr.GetString(1)] = rowCount;
                        rowCount++;
                    }
                }
                dr.Close();

                // Check the selected schedules from the configs
                StringCollection checkedSchedules = Configurator.GetConfigCollection("selected_schedule_names");
                if (checkedSchedules != null)
                {
                    foreach (String sched_name in checkedSchedules)
                    {
                        availableSchedulesList.SetItemChecked(inputBoxSchedules[sched_name], true);
                    }
                }
            }
            catch (NpgsqlException ne)
            {
                this.Logger.Log("Error with repository while loading the available schedules. Press reload button to try again. Going with existing checked schedules for now");
                this.Logger.Log(ne.Message);

                // Fill in the checkbox list based on the existing selected schedules
                StringCollection checkedSchedules = Configurator.GetConfigCollection("selected_schedule_names");
                if (checkedSchedules != null)
                {
                    var i = 0;
                    Dictionary<string, int> inputBoxSchedules = new Dictionary<string, int>();
                    availableSchedulesList.Items.Clear();
                    foreach (String sched_name in checkedSchedules)
                    {
                        availableSchedulesList.Items.Add(sched_name);
                        inputBoxSchedules[sched_name] = i;
                        availableSchedulesList.SetItemChecked(inputBoxSchedules[sched_name], true);
                        i++;
                    }
                }
            }
            catch (ConfigurationException ce)
            {
                this.Logger.Log("Incorrect credentials for repository readonly user. Cannot connect to repository.");
                MessageBox.Show("Credentials were not correct for the Repository \"readonly\" user. Please fix");
            }
        }

        /*
         * Configuration Saving Events
         */

        private void singleViewLocation_TextChanged(object sender, EventArgs e)
        {
            Configurator.SetConfig("single_export_view_location", singleViewLocation.Text);
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
        }

        private void testSite_TextChanged(object sender, EventArgs e)
        {
            Configurator.SetConfig("single_export_site", singleExportSite.Text);
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
        }

        private void singleUsernameForImpersonation_TextChanged(object sender, EventArgs e)
        {
            Configurator.SetConfig("single_export_impersonate_as_user", singleUsernameForImpersonation.Text);
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
        }

        private void exportTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configurator.SetConfig("single_export_type_index", exportTypeDropDown.SelectedIndex.ToString());
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
        }

        private void availableSchedulesList_ItemChecked(object sender, ItemCheckEventArgs e)
        {
            // Delayed for ItemClick from https://stackoverflow.com/questions/4454058/no-itemchecked-event-in-a-checkedlistbox#4454594

            this.BeginInvoke((MethodInvoker)delegate
            {
                StringCollection checked_schedules = new StringCollection();
                foreach (object itemChecked in availableSchedulesList.CheckedItems)
                {
                    checked_schedules.Add(itemChecked.ToString());
                }
                Configurator.SetConfig("selected_schedule_names", checked_schedules);
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
            });
        }

        /*
         * Main E-mailing methods (used by all)
         */

        public SmtpClient CreateSmtpClientFromConfig()
        {
            SmtpClient smtp_client = new SmtpClient(Configurator.GetConfig("smtp_server"));
            // Set all of the SMTP Server options
            var port = Configurator.GetConfig("smtp_server_port");
            if (port != "")
            {
                smtp_client.Port = Int32.Parse(port);
            }
            var tls = Configurator.GetConfig("smtp_server_tls");
            if (tls == "Yes")
            {
                smtp_client.EnableSsl = true;
            }
            var smtpServerUsername = Configurator.GetConfig("smtp_server_username");
            var smtpServerPassword = Configurator.GetConfig("smtp_server_password");
            if (smtpServerUsername != "" && smtpServerPassword != "")
            {
                smtp_client.Credentials = new System.Net.NetworkCredential(smtpServerUsername, smtpServerPassword);
            }
            return smtp_client;
        }

        private BeholdEmailer CreateBeholdEmailerFromConfig()
        {
            return this.CreateBeholdEmailerFromConfig(Configurator.GetConfig("single_export_site"));
        }

        private BeholdEmailer CreateBeholdEmailerFromConfig(string site)
        {
            var tabrep = new TableauRepository(
                Configurator.GetConfig("tableau_server"),
                Configurator.GetConfig("repository_pw"),
                "readonly"
                )
            {
                logger = this.Logger
            };
            var tabcmd = new Tabcmd(
                Configurator.GetConfig("tabcmd_program_location"),
                Configurator.GetConfig("tableau_server"),
                Configurator.GetConfig("server_admin_username"),
                Configurator.GetConfig("server_admin_password"),
                site,
                Configurator.GetConfig("tabcmd_config_location"),
                tabrep,
                this.Logger
            );

            BeholdEmailer tabemailer = new BeholdEmailer(tabcmd, this.CreateSmtpClientFromConfig());
            tabemailer.Logger = this.Logger;
            tabemailer.HtmlEmailTemplateFilename = Configurator.GetConfig("html_email_template_filename");
            tabemailer.TextEmailTemplateFilename = Configurator.GetConfig("text_email_template_filename");
            return tabemailer;
        }

        private Watermarker CreateWatermarkerFromConfig()
        {
            Watermarker wm = new Watermarker();
            string[] pageLocations = { "top_left", "top_center", "top_right", "bottom_left", "bottom_center", "bottom_right" };
            foreach (string pageLocation in pageLocations)
            {
                string settingsPageLocation = pageLocation.Split('_')[0] + pageLocation.Split('_')[1].First() + pageLocation.Split('_')[1].Substring(1);
                wm.SetPageLocationWatermarkFromConfig(pageLocation, Configurator.GetConfigSerializableStringDict(settingsPageLocation));
            }
            return wm;
        }

        public bool SendEmail(string scheduleName)
        {
            try
            {
                BeholdEmailer tabemailer = this.CreateBeholdEmailerFromConfig();
                Watermarker wm = this.CreateWatermarkerFromConfig();

                try
                {
                    bool result;

                    if (scheduleName != "")
                    {
                        tabemailer.ExportArchiveFolderPath = Configurator.GetConfig("export_archive_folder");
                        result = tabemailer.GenerateEmailsFromNamedScheduleInRepository(scheduleName, Configurator.GetConfig("email_sender"), wm);
                    }
                    else
                    {
                        result = tabemailer.GenerateEmailFromView(Configurator.GetConfig("email_sender"), new string[1] { testEmailRecipient.Text }, new string[0] { }, new string[0] { }, testEmailSubject.Text, "Please see attached file", singleUsernameForImpersonation.Text,
                        singleViewLocation.Text, ExportTypeIndexMap[Int32.Parse(Configurator.GetConfig("single_export_type_index"))], new Dictionary<String, String>(), wm);
                    }

                    return result;
                }
                catch (ConfigurationException ce)
                {
                    this.Logger.Log(ce.Message);
                    return false;
                }
            }
            // From Repository Failing
            catch (ConfigurationException ce)
            {
                this.Logger.Log(ce.Message);
                return false;
            }
        }

        public bool SendEmail()
        {
            try
            {
                BeholdEmailer tabemailer = this.CreateBeholdEmailerFromConfig();
                Watermarker wm = this.CreateWatermarkerFromConfig();

                try
                {
                    bool result;

                    result = tabemailer.GenerateEmailFromView(Configurator.GetConfig("email_sender"), new string[1] { testEmailRecipient.Text }, new string[0] { }, new string[0] { }, testEmailSubject.Text, "Please see attached Tableau file", singleUsernameForImpersonation.Text,
                    singleViewLocation.Text, "fullpdf", new Dictionary<String, String>(), wm);

                    return result;
                }
                catch (ConfigurationException ce)
                {
                    this.Logger.Log(ce.Message);
                    return false;
                }
            }
            // From Repository Failing
            catch (ConfigurationException ce)
            {
                this.Logger.Log(ce.Message);
                return false;
            }
        }

        public bool SendEmail(string site, string[] emailTo, string[] emailCc, string[] emailBcc, string emailSubject, string usernameForImpersonation, string viewLocation, Dictionary<String, String> viewFiltersMap, string attachmentContentType)
        {
            try
            {
                BeholdEmailer tabemailer = this.CreateBeholdEmailerFromConfig(site);
                Watermarker wm = this.CreateWatermarkerFromConfig();

                try
                {
                    bool result;
                    this.Logger.Log("Fixin' to send the e-mail");
                    result = tabemailer.GenerateEmailFromView(Configurator.GetConfig("email_sender"), emailTo, emailCc, emailBcc, emailSubject, "Please see attached Tableau file", usernameForImpersonation,
                    viewLocation, attachmentContentType.ToLower(), viewFiltersMap, wm);

                    return result;
                }
                catch (ConfigurationException ce)
                {
                    this.Logger.Log(ce.Message);
                    return false;
                }
            }
            // From Repository Failing
            catch (ConfigurationException ce)
            {
                this.Logger.Log(ce.Message);
                return false;
            }
        }

        public bool SendEmail(string site, string[] emailTo, string[] emailCc, string[] emailBcc, string emailSubject, string usernameForImpersonation, string viewLocation, Dictionary<String, String> viewFiltersMap)
        {
            return this.SendEmail(site, emailTo, emailCc, emailBcc, emailSubject, usernameForImpersonation, viewLocation, viewFiltersMap, "fullpdf");
        }

        /*
         * Batch Mode
         */

        /*
         * Helper tools
         */

        public delegate void ActivityGridDelegate(string message, string activityId);

        public void ActivityGridWriteMethod(string message, string activityId)
        {
            activityGrid.Rows.Add(new string[] { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), activityId, message });
            activityGrid.CurrentCell = activityGrid.Rows[activityGrid.Rows.Count - 1].Cells[0];
        }

        private void WriteToActivityGrid(string message)
        {
            // need to add activity ID
            string[] args = { message, "" };
            activityGrid.BeginInvoke(new ActivityGridDelegate(ActivityGridWriteMethod), args);
        }

        private void WriteToActivityGrid(string message, uint activityId)
        {
            // need to add activity ID
            string[] args = { message, activityId.ToString("D8") };
            activityGrid.BeginInvoke(new ActivityGridDelegate(ActivityGridWriteMethod), args);
        }

        public void EnableBatchButtons()
        {
            pickBulkCSVFile.BeginInvoke(new BatchButtonsDelegate(BatchButtonsEnableMethod));
        }

        public delegate void BatchButtonsDelegate();

        public void BatchButtonsEnableMethod()
        {
            pickBulkCSVFile.Enabled = true;
            sendBatchEmails.Enabled = true;
        }

        private void ValidateTextBox(object sender, CancelEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text == "")
            {
                t.BackColor = System.Drawing.Color.LightSalmon;
            }
            else
            {
                t.BackColor = System.Drawing.Color.White;
            }
        }

        private bool ValidateSetOfElements(Array elements)
        {
            bool all_clear = true;
            foreach (TextBox element in elements)
            {
                element.Text = element.Text.Trim();
                if (element.Text == "")
                {
                    all_clear = false;
                    element.BackColor = Color.LightSalmon;
                }
                else
                {
                    element.BackColor = Color.White;
                }
            };
            if (all_clear == false)
            {
                MessageBox.Show("Please fill out all necessary information");
            }
            return all_clear;
        }

        private void ValidateTableauServerUrl(object sender, EventArgs e)
        {
        }

        private void ReloadSubscriptions(object sender, EventArgs e)
        {
            LoadSubscriptionsFromRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configurator.EncryptConfig();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableauServerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form tsConfig = new ConfigureTableauServer();
            tsConfig.ShowDialog(this);
        }

        private void emailServerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form emailConfig = new ConfigureEmailServer();
            emailConfig.ShowDialog(this);
        }

        private void pageNumberingWatermarkingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form wmConfig = new ConfigureWatermarking();
            wmConfig.ShowDialog(this);
        }

        private void localSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form localConfig = new ConfigureLocalSettings();
            localConfig.ShowDialog(this);
        }

        private void actionQueueBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queueAction = (queueAction)e.Argument;

            e.Result = queueAction();
        }

        private void actionQueueBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //WriteToActivityGrid((string)e.Result);
            asyncActionQueue.RemoveAt(0);
            if (asyncActionQueue.Count != 0)
            {
                try
                {
                    actionQueueBackgroundWorker.RunWorkerAsync(asyncActionQueue[0]);
                }
                catch (InvalidOperationException exc)
                {
                    this.Logger.Log("Next action attempted but other action currently running");
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void disableSchedulesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (button.Checked == true)
            {
                StopTimer(sender, e);
            }

            Configurator.SetConfig("schedules_enabled", false);
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
        }

        private void enableSchedulesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (button.Checked == true)
            {
                StartTimer(sender, e);
            }
            Configurator.SetConfig("schedules_enabled", true);
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
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var readmeUrl = "https://github.com/bryantbhowell/Behold--Emailer/blob/master/Readme.txt";
            try
            {
                Process.Start(readmeUrl);
            }
            catch
            {
                Process.Start("iexplore.exe", readmeUrl);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = MessageBox.Show("Written by Bryant Howell\n\n(C) Tableau Software 2018\n\nhttps://github.com/bryantbhowell/Behold--Emailer", "About Behold! Emailer");
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (asyncActionQueue.Count > 0)
            {
                if (MessageBox.Show("Actions are still queued. If you close, they will be lost.", "Do you want to close?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    // Cancel the Closing event
                    e.Cancel = true;
                }
            }
        }
    }
}