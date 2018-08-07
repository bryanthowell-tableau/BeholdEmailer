using System;
using System.Net;

namespace Behold_Emailer
{
    internal class TableauHTTP
    {
        public string TableauServerUrl;
        public SimpleLogger Logger;

        public TableauHTTP(string tableauServerUrl)
        {
            this.TableauServerUrl = tableauServerUrl;
            this.Logger = null;
        }

        public void Log(string l)
        {
            if (this.Logger != null)
            {
                this.Logger.Log(l);
            }
        }

        public bool CreateTrustedTicketSession(string viewContentUrlToRedeem, string username, string siteContentUrl, string ip)
        {
            string ticket = this.GetTrustedTicketForUser(username, siteContentUrl, ip);
            //this.Log(String.Format("Trusted ticket returned {0}", ticket));
            bool result = this.RedeemTrustedTicket(viewContentUrlToRedeem, ticket, siteContentUrl);
            return result;
        }

        // This is a simple implementation of a Trusted Ticket request in C#
        public string GetTrustedTicketForUser(string username, string siteContentUrl, string ip)
        {
            if (siteContentUrl == "") { siteContentUrl = "default"; }
            this.Log(String.Format("Requesting trusted ticket for {0} on site {1}", username, siteContentUrl));

            string trusted_url = this.TableauServerUrl + "/trusted";
            this.Log(trusted_url);
            WebClient client = new WebClient();

            byte[] response;
            try
            {
                if (siteContentUrl == "default")
                {
                    response = client.UploadValues(trusted_url, new System.Collections.Specialized.NameValueCollection()
                        {
                            { "username", username }
                        }
                   );
                }
                else
                {
                    response = client.UploadValues(trusted_url, new System.Collections.Specialized.NameValueCollection()
                        {
                            { "sername", username },
                            { "target_site", siteContentUrl }
                        }
                    );
                }

                string result = System.Text.Encoding.UTF8.GetString(response);
                if (result == "-1")
                {
                    // If you don't get -1, you should have a trusted ticket, raise an exception
                    string error = String.Format("Trusted ticket for {0} on site {1} from server {2} returned -1, some error occurred", username, siteContentUrl, this.TableauServerUrl);
                    this.Log(error);
                    throw new ConfigurationException(error);
                }
                // If misconfigured, the Tableau Server returns a redirect page
                else if (result.Contains("html") == true)
                {
                    // If you don't get -1, you should have a trusted ticket, raise an exception
                    string error = String.Format("Trusted ticket for {0} on site {1} from server {2} returned the redirect page, some error occurred", username, siteContentUrl, this.TableauServerUrl);
                    this.Log(error);
                    throw new ConfigurationException(error);
                }

                //this.Log(String.Format("Trusted ticket for {0} on site {1} from server {2} returned {3}", username, siteContentUrl, this.TableauServerUrl, result));
                return result;
            }
            catch (WebException)
            {
                throw new ConfigurationException("Trusted tickets not working, check configuration of Tableau Server and the configuration program");
            }
        }

        // Simple implementation of redeeming a trusted ticket, which should create a session
        // Prior to Tableau Server 10.4, it was possible to retrieve the full session token from the Tableau Repository
        // It might be possible to pull the full token in later versions from the cookie response, but it's unclear if that would let you access the session
        // It is possible that you need tabadmin set features.ProtectVizPortalSessionIds false for this to work in 10.4 and later
        public bool RedeemTrustedTicket(string viewContentUrlToRedeem, string trustedTicket, string siteContentUrl)
        {
            if (siteContentUrl == "" || siteContentUrl.ToLower() == "default")
            {
                siteContentUrl = "default";
            }
            string trustedViewUrl = String.Format("{0}/trusted/{1}", this.TableauServerUrl, trustedTicket);
            if (siteContentUrl.ToLower() != "default")
            {
                trustedViewUrl += String.Format("/t/{0}/views/{1}", siteContentUrl, viewContentUrlToRedeem);
            }
            else
            {
                trustedViewUrl += String.Format("/views/{0}", viewContentUrlToRedeem);
            }

            WebClient client = new WebClient();
            try
            {
                this.Log(String.Format("Redeeming trusted ticket via {0}", trustedViewUrl));
                byte[] response = client.DownloadData(trustedViewUrl);
                this.Log(String.Format("Trusted ticket redeemed succesfully"));
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    var statusDescription = ((HttpWebResponse)ex.Response).StatusDescription;
                    this.Log(String.Format("Trusted ticket redemption failed with Status Code {0} and Description {1}", statusCode, statusDescription));
                }
                return false;
            }
        }
    }
}