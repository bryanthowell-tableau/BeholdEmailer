using Npgsql;
using System;

namespace Behold_Emailer
{
    /*
     * TableauRepository represents a connection to the PostgreSQL repository in Tableau Server
     * This class is just a convenience which builds in a connection and useful queries and processing
     * You could make the thing yourself if you needed to
     */

    internal class TableauRepository
    {
        private string[] repository_users = new string[3] { "tableau", "readonly", "tblwgadmin" };
        private int repositoryPort;
        private string repositoryDatabase;
        private string repositoryUsername;
        private string repositoryPassword;
        private string repositoryServer;
        private NpgsqlConnection repository;
        public SimpleLogger logger;

        public TableauRepository(string tableauServerUrl, string repositoryPassword, string repositoryUsername)
        {
            if (String.Equals(repositoryUsername, "")) { this.repositoryUsername = "readonly"; }
            else
            {
                this.repositoryUsername = repositoryUsername;
            }
            this.repositoryPort = 8060;
            this.repositoryDatabase = "workgroup";
            this.repositoryPassword = repositoryPassword;
            this.logger = null;
            // Don't use user "tableau", you need at least "readonly" right
            // Only need tblwgadmin if you need to write into the repository, only for advanced hack cases

            // Remove the http:// or https:// to log in to the repository. (Do we need things if this is SSL?)
            int colon_slash_slash = tableauServerUrl.IndexOf("://");
            if (colon_slash_slash != -1)
            {
                this.repositoryServer = tableauServerUrl.Substring(colon_slash_slash + 3);
            }
            else
            {
                this.repositoryServer = tableauServerUrl;
            }
            // Take off any extra stuff after the server main (including port number extensions)
            int final_colon = this.repositoryServer.IndexOf(":");
            if (final_colon != -1)
            {
                this.repositoryServer = this.repositoryServer.Substring(0, final_colon);
            }
            int extra_slash = this.repositoryServer.IndexOf("/");
            if (extra_slash != -1)
            {
                this.repositoryServer = this.repositoryServer.Substring(0, extra_slash);
            }

            this.repository = new NpgsqlConnection(String.Format("Host={0};Username={1};Password={2};Database={3};Port={4};Pooling=false;Timeout=25", this.repositoryServer,
                this.repositoryUsername, this.repositoryPassword, this.repositoryDatabase, this.repositoryPort));
            try
            {
                this.Log(String.Format("Opening connection to repository on {0}", this.repositoryServer));
                this.repository.Open();
            }
            catch (NpgsqlException)
            {
                throw new ConfigurationException("Cannot connect to Repository, please check credentials");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Timeout"))
                {
                    this.Log("Issue with PG timeout. Going to keep on trucking");
                }
                else
                {
                    throw e;
                }
            }
        }

        // Destructor to free up resource
        /* ~TableauRepository()
         {
             this.repository.Close();
         }*/

        public void Log(string l)
        {
            if (this.logger != null)
            {
                this.logger.Log(l);
            }
        }

        public NpgsqlDataReader ExecuteQuery(string sqlQuery)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, this.repository);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public NpgsqlDataReader QuerySessions(string username)
        {
            // Trusted tickets sessions do not have anything in the 'data' column
            //The auth token is contained within the shared_wg_write column, stored as JSON
            string sessionsSql = @"
            SELECT
            sessions.session_id,
            sessions.data,
            sessions.updated_at,
            sessions.user_id,
            sessions.shared_wg_write,
            sessions.shared_vizql_write,
            system_users.name AS user_name,
            users.system_user_id
            FROM sessions,
            system_users,
            users
            WHERE sessions.user_id = users.id AND users.system_user_id = system_users.id
        ";
            if (username != "")
            {
                sessionsSql += "AND system_users.name = @uname\n";
            }
            sessionsSql += "ORDER BY sessions.updated_at DESC;";

            NpgsqlCommand cmd = new NpgsqlCommand(sessionsSql, this.repository);
            cmd.Parameters.AddWithValue("@uname", username);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public NpgsqlDataReader QuerySubscriptionsForUsers(string scheduleName, Boolean viewsOnlyFlag)
        {
            string subscriptionsSql = @"
                SELECT
                s.id,
                s.subject,
                s.user_name,
                s.site_name,
                COALESCE(cv.repository_url, s.view_url) as view_url,
                sch.name,
                su.email
                FROM _subscriptions s
                LEFT JOIN _customized_views cv  ON s.customized_view_id = cv.id
                JOIN _schedules sch ON sch.name = s.schedule_name
                JOIN system_users su ON su.name = s.user_name
            ";

            if (scheduleName != "")
            {
                subscriptionsSql += "WHERE sch.name = @sched_name\n";

                if (viewsOnlyFlag == true)
                {
                    subscriptionsSql += "AND s.view_url IS NOT NULL -- Export command in tabcmd requires a View not a Workbook";
                }
            }
            else
            {
                if (viewsOnlyFlag == true)
                {
                    subscriptionsSql += "WHERE s.view_url IS NOT NULL -- Export command in tabcmd requires a View not a Workbook";
                }
            }

            NpgsqlCommand cmd = new NpgsqlCommand(subscriptionsSql, this.repository);
            if (scheduleName != "")
            {
                cmd.Parameters.AddWithValue("@sched_name", scheduleName);
            }

            NpgsqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public NpgsqlDataReader QueryInactiveSubscriptionSchedules()
        {
            string sub_sched_sql = @"
                SELECT *
                FROM _schedules sch
                WHERE scheduled_action_type = 'Subscriptions'
                AND active=false
            ";

            NpgsqlCommand cmd = new NpgsqlCommand(sub_sched_sql, this.repository);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public NpgsqlDataReader QueryInactiveSubscriptionSchedulesForNextRunTime()
        {
            string sub_sched_sql = @"
                SELECT
			        name,
			        run_next_at
                FROM _schedules sch
                WHERE scheduled_action_type = 'Subscriptions' AND active=false
            ";

            NpgsqlCommand cmd = new NpgsqlCommand(sub_sched_sql, this.repository);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
    }
}