using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Behold_Emailer
{
    internal class Tabcmd
    {
        private string tabcmdFolderLocation;
        private string username;
        private string password;
        private string site;
        private string repositoryPassword;
        private string userSessionId;
        private string userAuthenticationToken;
        private string tabcmdConfigLocation;
        private string tabcmdConfigFilename;

        public string Site
        {
            get { return site; }
            set { if (value.ToLower() == "default") { site = "default"; } else { site = value; } }
        }

        public string TableauServerUrl;
        public SimpleLogger Logger;
        public TableauRepository Repository;

        public Tabcmd(string tabcmdFolderLocation, string tableauServerUrl,
            string username, string password, string site, string repositoryPassword,
            string tabcmdConfigurationFileLocation)
        {
            // tabcmd program configurations are stored within an XML file
            this.tabcmdFolderLocation = tabcmdFolderLocation;
            this.tabcmdConfigFilename = "tabcmd-session.xml";
            this.tabcmdConfigLocation = tabcmdConfigurationFileLocation;
            // Open the configuration file and test whether it resembles the real file.
            // Should really replace with XML reader to make this better, but in the end this part needs to be deprecated for the APIs
            try
            {
                StreamReader tabcmdConfigurationFile = new StreamReader(tabcmdConfigurationFileLocation + tabcmdConfigFilename);
                // Read first line
                tabcmdConfigurationFile.ReadLine();
                // Read second line, should be <session>. May come up with better test
                string second_line = tabcmdConfigurationFile.ReadLine();
                if (second_line.Contains("<session>") == false)
                {
                    throw new ConfigurationException("tabcmd-session.xml file information is incorrect. File is not tabcmd-session.xml");
                }
                tabcmdConfigurationFile.Close();
            }
            catch (IOException)
            {
                throw new ConfigurationException("tabcmd-config file information is incorrect. Config file could not be opened");
            }

            this.username = username;
            this.password = password;
            this.Site = site;
            this.TableauServerUrl = tableauServerUrl;

            this.repositoryPassword = repositoryPassword;
            this.Repository = new TableauRepository(this.TableauServerUrl, this.repositoryPassword, "readonly");

            // This preps tabcmd for subsequent calls
            this.Log("Preping the tabcmd admin session");
            this.CreateTabcmdAdminSession();
            this.Logger = null;
        }

        public Tabcmd(string tabcmd_folder, string tableau_server_url, string username, string password,
            string site, string tabcmd_config_location, TableauRepository TableauRepository)
        {
            this.tabcmdFolderLocation = tabcmd_folder;
            this.username = username;
            this.password = password;
            this.Site = site;
            this.TableauServerUrl = tableau_server_url;
            this.tabcmdConfigFilename = "tabcmd-session.xml";
            this.tabcmdConfigLocation = tabcmd_config_location;

            this.Repository = TableauRepository;

            // This preps tabcmd for subsequent calls
            this.Log("Preping the tabcmd admin session");
            this.CreateTabcmdAdminSession();
            this.Logger = null;
        }

        public Tabcmd(string tabcmd_folder, string tableau_server_url, string username, string password,
            string site, string tabcmd_config_location, TableauRepository TableauRepository, SimpleLogger logger)
        {
            this.tabcmdFolderLocation = tabcmd_folder;
            this.username = username;
            this.password = password;
            this.Site = site;
            this.TableauServerUrl = tableau_server_url;

            this.tabcmdConfigFilename = "tabcmd-session.xml";
            this.tabcmdConfigLocation = tabcmd_config_location;

            this.Logger = logger;

            this.Repository = TableauRepository;

            // This preps tabcmd for subsequent calls
            this.Log(String.Format("Preping the tabcmd admin session for site {0}", this.Site));
            this.CreateTabcmdAdminSession();
        }

        // Need to add no-certcheck when https
        private string CreateNoCertCheckParameter()
        {
            if (this.TableauServerUrl.ToLower().Contains("https"))
            {
                return "--no-certcheck";
            }
            else
            {
                return "";
            }
        }

        public void Log(string LogLine)
        {
            if (this.Logger != null)
            {
                this.Logger.Log(LogLine);
            }
        }

        // All the build* methods are just for convenience of wrapping tabcmd

        // Always change to the directory that tabcmd is located in, including the drive letter change
        // This is actually just a standard batch command, nothing tabcmd specific
        public string BuildDirectoryChangeCommand()
        {
            var driveLetter = this.tabcmdFolderLocation.Substring(0, 1);
            return String.Format("{0}: \ncd {1}", driveLetter, this.tabcmdFolderLocation);
        }

        /*
         * For the tabcmd technique, you are always logging in using an admin username and password, then swapping the session
         * in the tabcmd config XML file to use the session that you established using trusted tickets
         */

        public string BuildLoginCommand(string TemporaryPasswordStorageFilename)
        {
            try
            {   // Writes the password to a temporary file so that the session can be logged in
                File.WriteAllText(TemporaryPasswordStorageFilename, this.password);
            }
            catch (Exception e)
            {
                Console.WriteLine("The password file could not be read:");
                Console.WriteLine(e.Message);
            }
            string batchCommandText;
            if (this.Site.ToLower() == "default")
            {
                batchCommandText = String.Format("tabcmd login -s {0} -u {1} --password-file \"{2}\" {3}", this.TableauServerUrl,
                    this.username, TemporaryPasswordStorageFilename, this.CreateNoCertCheckParameter());
            }
            else
            {
                batchCommandText = String.Format("tabcmd login -s {0} -t {1} -u {2} --password-file \"{3}\" {4}", this.TableauServerUrl,
                    this.Site, this.username, TemporaryPasswordStorageFilename, this.CreateNoCertCheckParameter());
            }
            return batchCommandText;
        }

        public string BuildExportCommand(string exportFileExtension, string filename, string viewUrl, Dictionary<string, string> viewFiltersMapping,
            bool refresh)
        {
            string batchCommandText;

            // If no viewUrl, throw a configuration exception
            if (viewUrl == "")
            {
                throw new ConfigurationException("No viewUrl was provided");
            }

            // Check to make sure the system passsed in one of the acceptable types
            string[] allowableExportTypes = new string[4] { "pdf", "csv", "png", "fullpdf" };
            if (allowableExportTypes.Contains(exportFileExtension.ToLower()) == false)
            {
                // Exception
            }

            string additionalUrlParameters = "";

            // viewFiltersMapping converts Key=>Value dictionary to URL parameters with batch file encoding
            if (viewFiltersMapping != null)
            {
                // WebUtility.HtmlEncode
                var firstParameter = 0;
                foreach (KeyValuePair<string, string> pair in viewFiltersMapping)
                {
                    if (firstParameter == 0)
                    {
                        additionalUrlParameters += "?";
                        firstParameter++;
                    }
                    else
                    {
                        additionalUrlParameters += "&";
                    }
                    // Gotta double the % sign because batch files use %2 as a replacement token.
                    additionalUrlParameters += Uri.EscapeUriString(pair.Key).Replace("%", "%%") + "=" + (pair.Value);
                }

                if (refresh == true)
                {
                    additionalUrlParameters += "&:refresh";
                }
            }
            else if (viewFiltersMapping == null)
            {
                if (refresh == true)
                {
                    additionalUrlParameters += "?:refresh";
                }
            }
            viewUrl += additionalUrlParameters;
            batchCommandText = String.Format("tabcmd export \"{0}\" --filename \"{1}\" --{2} {3}",
                viewUrl, filename, exportFileExtension, this.CreateNoCertCheckParameter());

            // Additional parameters for export options
            string extraCommandParameters = "--pagelayout {4} --pagesize {5} --width {6} --height {7}";

            return batchCommandText;
        }

        // You need to log in to tabcmd successfully with admin privileges the first time
        private void CreateTabcmdAdminSession()
        {
            this.Log("Creating a tabcmd admin session");

            string[] cmds = new string[2];

            // Change directory command necessary to get to tabcmd directory to run the batch file
            cmds[0] = this.BuildDirectoryChangeCommand();

            //var changeResults = Cmd.Run(cmds[0], true);
            //this.Log(changeResults[0]);
            //this.Log(changeResults[1]);

            // The password gets decrypted from the storage, then stored temporarily in the clear to disk
            // This is necessary for tabcmd to use a password file
            string temporaryPasswordStorageFilename = this.tabcmdFolderLocation + "sh3zoy2lya.txt";
            cmds[1] = this.BuildLoginCommand(temporaryPasswordStorageFilename);

            // Change to the tabcmd directory first, before writing

            try
            {
                File.WriteAllLines(this.tabcmdFolderLocation + "login.bat", cmds);
            }
            catch (IOException)
            {
                throw new ConfigurationException("Could not write login.bat file, please restart and check all files");
            }

            string[] loginCmds = new string[2];
            loginCmds[0] = cmds[0];
            loginCmds[1] = "login.bat";
            // There are going to be multiple lines returned, accessible as an array
            var results = Cmd.Run(loginCmds, true);
            // Check tabcmd results?

            this.Log(results[0]);
            this.Log(results[1]);

            // Clear admin password file after each run
            File.Delete(temporaryPasswordStorageFilename);
            File.Delete(this.tabcmdFolderLocation + "login.bat");
        }

        public string CreateExportFile(string export_type, string viewLocation,
            Dictionary<string, string> viewFilterMapping, string usernameToImpersonate,
            string FilenameToSaveAs)
        {
            if (viewLocation == "")
            {
                throw new ConfigurationException("viewLocation is not specified");
            }

            if (String.Equals(usernameToImpersonate, "") == false)
            {
                this.CreateSessionAndConfigureTabcmdForUser(usernameToImpersonate, viewLocation);
            }

            string[] cmds = new string[2];
            cmds[0] = this.BuildDirectoryChangeCommand();

            string savedFilename;
            if (String.Equals(export_type.ToLower(), "fullpdf"))
            {
                savedFilename = String.Format("{0}.{1}", FilenameToSaveAs, "pdf");
            }
            else
            {
                savedFilename = String.Format("{0}.{1}", FilenameToSaveAs, export_type);
            }

            //cmds[1] = String.Format("del \"{0}\"", saved_filename);
            cmds[1] = this.BuildExportCommand(export_type, savedFilename, viewLocation, viewFilterMapping, false);

            try
            {
                File.WriteAllLines(this.tabcmdFolderLocation + "export.bat", cmds);
            }
            catch (IOException)
            {
                MessageBox.Show("Could not write export.bat file");
            }

            string full_file_location = this.tabcmdFolderLocation + savedFilename;
            this.Log(String.Format("Writing to file {0}", full_file_location));

            // Run the commands
            // DIrectory change first, just in case you are in a weird place
            string[] allCmds = new string[2];
            allCmds[0] = cmds[0];
            allCmds[1] = "export.bat";
            var results = Cmd.Run(allCmds, true);

            this.Log(results[0]);
            this.Log(results[1]);
            if (results[1].Contains("===== Saved"))
            {
                this.Log("Export file generated correctly");
            }
            else
            {
                throw new ConfigurationException("Export command failed, most likely configuration issue.");
            }

            File.Delete(this.tabcmdFolderLocation + "export.bat");
            return full_file_location;
        }

        /*
         * The essence of how this works is that you can use trusted tickets to create a session
         * then rewrite tabcmd's config file with that session info instead. Because tabcmd has
         * session continuation, you can keep running tabcmd without a signin action, but continually
         * switching to the correct user
         */

        /*
         * tabcmd keeps a session history, stored within its XML configuration file.
         * Rather than logging into tabcmd again, once there is a session history, we simply substitute in the
         * impersonated user's info directly into the XML.
         */

        private void ConfigureTabcmdConfigForUserSession(string user)
        {
            XmlDocument doc = new XmlDocument();
            var tabcmdConfigFullFilePath = this.tabcmdConfigLocation + this.tabcmdConfigFilename;
            this.Log("Trying to open config file for tabcmd at " + tabcmdConfigFullFilePath);
            doc.Load(tabcmdConfigFullFilePath);
            this.Log("Loaded the tabcmd config file");
            XmlWriterSettings xwsSettings = new XmlWriterSettings();
            xwsSettings.Indent = true;
            xwsSettings.IndentChars = " ";

            XmlNode root = doc.DocumentElement;
            this.Log("Reading the first elements of the XML");
            XmlNode uname = root.SelectSingleNode("username");
            uname.InnerText = user;

            XmlNode baseUrl = root.SelectSingleNode("base-url");
            baseUrl.InnerText = this.TableauServerUrl;

            XmlNode sessionId = root.SelectSingleNode("session-id");
            sessionId.InnerText = this.userSessionId;

            XmlNode authToken = root.SelectSingleNode("authenticity-token");
            authToken.InnerText = this.userAuthenticationToken;

            XmlNode sitePrefix = root.SelectSingleNode("site-prefix");
            if (this.site.ToLower() != "default")
            {
                sitePrefix.InnerText = String.Format("t/{0}", this.site);
            }
            else
            {
                sitePrefix.InnerText = null;
            }
            this.Log("Writing the new XML file to replace the older one");
            using (XmlWriter xwWriter = XmlWriter.Create(tabcmdConfigFullFilePath, xwsSettings))
            {
                doc.PreserveWhitespace = true;
                doc.Save(xwWriter);
            }
        }

        // By querying the sessions table, there is a JSON string which includes the auth token
        private void SetTabcmdAuthenticationInfoFromRepositoryForImpersonation(string usernameToImpersonate)
        {
            NpgsqlDataReader dr = this.Repository.QuerySessions(usernameToImpersonate);
            if (dr.HasRows == true)
            {
                dr.Read();
                this.userSessionId = dr.GetString(0);
                string wg_json = dr.GetString(4);
                this.Log(String.Format("{0} is userSessionId, {1}  is wg_json", this.userSessionId, wg_json));
                var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(wg_json), new System.Xml.XmlDictionaryReaderQuotas());
                var XmlDoc = new XmlDocument();
                XmlDoc.Load(jsonReader);
                XmlNode root = XmlDoc.DocumentElement;
                XmlNode auth_token = root.SelectSingleNode("auth_token");
                this.userAuthenticationToken = auth_token.InnerText;
            }
            else
            {
                // Throw some kind of exception because you didn't find any sessions for that user
                // Something must have broken in the trusted tickets stuff
            }
            dr.Close();
        }

        private void CreateSessionAndConfigureTabcmdForUser(string user, string viewLocation)
        {
            TableauHTTP tabHttp = new TableauHTTP(this.TableauServerUrl);
            tabHttp.Logger = this.Logger;
            if (tabHttp.CreateTrustedTicketSession(viewLocation, user, this.site, ""))
            {
                this.SetTabcmdAuthenticationInfoFromRepositoryForImpersonation(user);
                this.ConfigureTabcmdConfigForUserSession(user);
            }
            else
            {
                this.Log("Trusted ticket session could not be established");
            }
        }
    }
}

// Taken from http://techvalleyprojects.blogspot.com/2012/04/c-using-command-prompt.html
public static class Cmd
{
    public static string[] Run(string command, bool output)
    {
        /*
         *  New array of two strings.
         *  string[0] is the error message.
         *  string[1] is the output message.
         */
        string[] message = new string[2];

        // ProcessStartInfo allows better control over
        // the soon to executed process
        ProcessStartInfo info = new ProcessStartInfo();

        // Input to the process is going to come from the Streamwriter
        info.RedirectStandardInput = true;

        // Output from the process is going to be put into message[1]
        info.RedirectStandardOutput = true;

        // Error, if any, from the process is going to be put into message[0]
        info.RedirectStandardError = true;

        // This must be set to false
        info.UseShellExecute = false;

        // We want to open the command line
        info.FileName = "cmd.exe";

        // We don't want to see a command line window
        info.CreateNoWindow = true;

        // Instantiate a Process object
        Process proc = new Process();

        // Set the Process object's start info to the above StartProcessInfo
        proc.StartInfo = info;

        // Start the process
        proc.Start();

        // The stream writer is replacing the keyboard as the input
        using (StreamWriter writer = proc.StandardInput)
        {
            // If the streamwriter is able to write
            if (writer.BaseStream.CanWrite)
            {
                // Write the command that was passed into the method
                writer.WriteLine(command);
                // Exit the command window
                writer.WriteLine("exit");
            }
            // close the StreamWriter
            writer.Close();
        }

        // Get any Error's that may exist
        message[0] = proc.StandardError.ReadToEnd();

        // If the output flag was set to true
        if (output)
        {
            // Get the output from the command line
            message[1] = proc.StandardOutput.ReadToEnd();
        }

        // close the process
        proc.Close();

        // return the any error/output
        return message;
    }

    public static string[] Run(string[] command, bool output)
    {
        string[] message = new string[2];

        ProcessStartInfo info = new ProcessStartInfo();

        info.RedirectStandardInput = true;
        info.RedirectStandardOutput = true;
        info.RedirectStandardError = true;

        info.UseShellExecute = false;
        info.FileName = "cmd.exe";
        info.CreateNoWindow = true;

        Process proc = new Process();
        proc.StartInfo = info;
        proc.Start();

        using (StreamWriter writer = proc.StandardInput)
        {
            if (writer.BaseStream.CanWrite)
            {
                foreach (string q in command)
                {
                    writer.WriteLine(q);
                }
                writer.WriteLine("exit");
            }
        }

        message[0] = proc.StandardError.ReadToEnd();

        if (output)
        {
            message[1] = proc.StandardOutput.ReadToEnd();
        }

        // close the process
        proc.Close();

        return message;
    }
}