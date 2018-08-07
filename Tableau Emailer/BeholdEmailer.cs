using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace Behold_Emailer
{
    internal class BeholdEmailer
    {
        private string TableauServerUrl;
        private string RepositoryPassword;
        private SmtpClient SmtpServer;
        private Tabcmd Tabcmd;
        public string HtmlEmailTemplateFilename { get; set; }
        public string TextEmailTemplateFilename { get; set; }
        public SimpleLogger Logger;
        public string ExportArchiveFolderPath;

        // Original constructor created the tabcmd object
        public BeholdEmailer(string tabcmdDirectory, string tabcmdConfigLocation, string repositoryPassword, string tableauServerUrl,
            string tableauServerAdminUsername, string tableauServerAdminPassword, string smtpServerName, string smtpServerUsername, string smtpServerPassword)
        {
            this.TableauServerUrl = tableauServerUrl;
            this.RepositoryPassword = repositoryPassword;
            this.SmtpServer = new SmtpClient(smtpServerName);
            this.HtmlEmailTemplateFilename = "";
            this.TextEmailTemplateFilename = "";
            // Add credentials stuff here

            this.Tabcmd = new Tabcmd(tabcmdDirectory, tableauServerUrl, tableauServerAdminUsername, tableauServerAdminPassword, "default", repositoryPassword, tabcmdConfigLocation);
            this.Logger = null;
            this.ExportArchiveFolderPath = null;
        }

        // If you create the tabcmd object separately you could put it
        public BeholdEmailer(Tabcmd tabcmd, string smtpServer)
        {
            this.Tabcmd = tabcmd;
            this.TableauServerUrl = this.Tabcmd.TableauServerUrl;
            this.SmtpServer = new SmtpClient(smtpServer);
            this.HtmlEmailTemplateFilename = "";
            this.TextEmailTemplateFilename = "";
            this.Logger = null;
            this.ExportArchiveFolderPath = null;
        }

        // In this situation you create the tabcmd and smtpClient objects separately
        public BeholdEmailer(Tabcmd tabcmd, SmtpClient smtpClient)
        {
            this.Tabcmd = tabcmd;
            this.TableauServerUrl = this.Tabcmd.TableauServerUrl;
            this.SmtpServer = smtpClient;
            this.HtmlEmailTemplateFilename = "";
            this.TextEmailTemplateFilename = "";
            this.Logger = null;
            this.ExportArchiveFolderPath = null;
        }

        // Placeholder here for Constructor for Emailer 2.0 which uses the REST API connection instead of tabcmd
        public BeholdEmailer(string restApiAdminUsername, string restApiAdminPassword, SmtpClient smtpClient)
        {
        }

        // Public facing logging method in case it is needed
        public void Log(string l)
        {
            if (this.Logger != null)
            {
                this.Logger.Log(l);
            }
        }

        // Method for doing the e-mailing action. Assumes a single template set in the main class
        public void EmailFileFromTemplate(string fromAddress, string[] toAddresses, string[] ccAddresses, string[] bccAddresses,
            string subject, string simpleMessageText, string filenameToAttach)
        {
            this.Log("Actually sending from " + fromAddress);
            string MessageText = "";
            // Load Text template file. If it doesn't exist, just use the simple message text
            if (this.TextEmailTemplateFilename != "")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(this.TextEmailTemplateFilename))
                    {
                        MessageText = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                MessageText = simpleMessageText;
            }
            // Initiate the mail message
            MailMessage Message = new MailMessage()
            {
                From = new MailAddress(fromAddress),
                Body = MessageText,
                Subject = subject
            };
            this.Log("Message created effectively");
            // Run through the string arrays of e-mail addresses and add them to the necessary SMTP collections
            foreach (var toUser in toAddresses)
            {
                if (!String.IsNullOrEmpty(toUser))
                {
                    Message.To.Add(new MailAddress(toUser));
                }
            }

            foreach (var ccUser in ccAddresses)
            {
                if (!String.IsNullOrEmpty(ccUser))
                {
                    Message.CC.Add(new MailAddress(ccUser));
                };
            }

            foreach (var bccUser in bccAddresses)
            {
                if (!String.IsNullOrEmpty(bccUser))
                {
                    Message.Bcc.Add(new MailAddress(bccUser));
                }
            }
            this.Log("Added all the addresses");

            // Load HTML template as alternative message type
            if (this.HtmlEmailTemplateFilename != "")
            {
                try
                {
                    using (StreamReader sr = new StreamReader(this.HtmlEmailTemplateFilename))
                    {
                        ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                        AlternateView htmlAltView = AlternateView.CreateAlternateViewFromString(sr.ReadToEnd(), mimeType);
                        Message.AlternateViews.Add(htmlAltView);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            // Add the attachment
            Attachment att = new Attachment(filenameToAttach);
            Message.Attachments.Add(att);

            // Send the message via the SmtpServer object
            this.SmtpServer.Send(Message);

            // Must close the attachment or file will stay locked to the program and you can't clean up
            att.Dispose();
        }

        // Wrapper combination function to create the export
        public string GenerateExportAndWatermark(string viewUser, string viewLocation, string contentType,
            Dictionary<string, string> viewFilterDictionary, Watermarker watermarker)
        {
            string filenameToAttach = this.Tabcmd.CreateExportFile(contentType, viewLocation, viewFilterDictionary, viewUser, "exported_workbook");
            this.Log(String.Format("{1} created and saved successfully as {0}", filenameToAttach, contentType));

            // Watermark the PDF (working copy) if PDF
            if (contentType == "fullpdf" || contentType == "pdf")
            {
                if (watermarker != null)
                {
                    this.Log("Adding watermark / header / footer to exported PDF");
                    watermarker.AddWatermarkToPdf(filenameToAttach);
                    this.Log("Watermarking performed successfully");
                }
            }
            return filenameToAttach;
        }

        // This is the main wrapper method for pulling the PDF, watermarking it, then e-mailing it out
        public bool GenerateEmailFromView(string fromAddress, string[] toAddresses, string[] ccAddresses, string[] bccAddresses, string emailSubject, string emailTemplateFilename,
            string viewUsername, string viewLocation, string attachmentContentFileType, Dictionary<string, string> viewFilterMap, Watermarker watermarker)
        {
            this.Log("Sending from " + fromAddress);
            this.Log(String.Format("Creating {2} export from tabcmd for {0} for user {1}", viewLocation, viewUsername, attachmentContentFileType));
            try
            {
                string filenameToAttach = this.GenerateExportAndWatermark(viewUsername, viewLocation, attachmentContentFileType, viewFilterMap, watermarker);
                this.Log(String.Format("{1} created and saved successfully as {0}", filenameToAttach, attachmentContentFileType));

                // Copy the file with a new name so that it can be archived
                string[] fileEnding = filenameToAttach.Split('.');

                string timestamp = DateTime.UtcNow.ToString("s");
                timestamp = timestamp.Replace(":", "_");
                timestamp = timestamp.Replace("-", "_");
                string finalFilename = String.Format("{0} - {1} - {2}.{3}", emailSubject, viewUsername, timestamp, fileEnding[fileEnding.Length - 1]);
                if (this.ExportArchiveFolderPath != null)
                {
                    finalFilename = this.ExportArchiveFolderPath + finalFilename;
                    this.Log(String.Format("Achiving export to {0}", finalFilename));
                }

                File.Copy(filenameToAttach, finalFilename, true);

                this.Log(String.Format("Sending e-mail of exported (and watermarked) file to {0}", toAddresses[0]));
                this.EmailFileFromTemplate(fromAddress, toAddresses, ccAddresses, bccAddresses, emailSubject, emailTemplateFilename, finalFilename);
                this.Log(String.Format("Removing original file {0}", filenameToAttach));
                File.Delete(filenameToAttach);

                // Cleanup if no archive
                if (this.ExportArchiveFolderPath == "")
                {
                    this.Log(String.Format("Removing e-mailed file {0}", finalFilename));
                    File.Delete(finalFilename);
                }

                this.Log("Email sent successfully");
                return true;
            }
            catch (ConfigurationException ce)
            {
                this.Log(ce.Message);
                return false;
            }
        }

        // This is the wrapper method for pulling all of the e-mails on the next schedule and generating them
        public bool GenerateEmailsFromNamedScheduleInRepository(string scheduleName, string fromAddress, Watermarker watermarker)
        {
            // Pull all of the scheduled e-mails
            NpgsqlDataReader dr = this.Tabcmd.Repository.QuerySubscriptionsForUsers(scheduleName, true);
            bool suceededFlag = true;
            var dataTable = new DataTable();
            dataTable.Load(dr);
            dr.Close();
            // For each of the scheduled e-mails, build and e-mail
            foreach (DataRow row in dataTable.Rows)
            {
                string emailSubject = row[1].ToString();
                string user = row[2].ToString();
                string site = row[3].ToString();
                string viewLocation = row[4].ToString();
                string userEmail = row[6].ToString();

                this.Tabcmd.Site = site;
                this.Log(String.Format("Generating e-mail of view {0} on site {1} for {2} at {3}", viewLocation, site, user, userEmail));

                // Call to the method that generates the e-mails
                bool result = this.GenerateEmailFromView(fromAddress, new string[1] { userEmail }, new string[0] { }, new string[0] { }, emailSubject, "", user, viewLocation, "fullpdf",
                    new Dictionary<string, string>(), watermarker);
                if (suceededFlag == true && result == false)
                {
                    suceededFlag = false;
                }
                if (result == true)
                {
                    this.Log("Email generated succesfully");
                }
                else
                {
                    this.Log("Error creating email");
                }
            }
            return suceededFlag;
        }
    }
}