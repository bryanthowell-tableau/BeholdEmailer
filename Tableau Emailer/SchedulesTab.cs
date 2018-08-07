using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Behold_Emailer
{
    public partial class MainWindow : Form
    {
        // New run schedule event which pushes the actions to the top of the queue
        private void queueSchedulesOnTimer(object source, EventArgs e)
        {
            //WriteToActivityGrid("Schedule Check Timer ticking");
            // Only run on one minute after every 15
            if (DateTime.Now.Minute == 1 || DateTime.Now.Minute == 16 || DateTime.Now.Minute == 31 || DateTime.Now.Minute == 46)
            {
                updateSchedulesQueue();
            }
        }

        private void updateSchedulesQueue()
        {
            var activityId = this.nextActivityId;
            this.nextActivityId++;
            WriteToActivityGrid("Checking for schedules to run in the repository", activityId);
            this.Logger.Log("Schedule checks begin");
            try
            {
                // Read the schedules that are currently in the repository, to update the active schedule queue
                TableauRepository rep = new TableauRepository(Configurator.GetConfig("tableau_server"),
                    Configurator.GetConfig("repository_pw"), "readonly");
                rep.logger = this.Logger;
                //this.Logger.Log("Starting to read from the repository");
                NpgsqlDataReader dr = rep.QueryInactiveSubscriptionSchedulesForNextRunTime();
                if (dr.HasRows == true)
                {
                    //this.Logger.Log("Opening the active schedules queue file");
                    using (StreamWriter activeSchedulesQueueFileWriter = new StreamWriter(this.ActiveSchedulesQueueFilename, false, new UTF8Encoding()))
                    {
                        //this.Logger.Log("Reading from the result set ");
                        while (dr.Read())
                        {
                            // this.Logger.Log("Working inside the result set ");
                            string scheduleName = dr.GetString(0);
                            string scheduleNextRunTime = dr.GetDateTime(1).ToString();
                            //this.Logger.Log(String.Format("Schedule here {0} {1}", scheduleName, scheduleNextRunTime));

                            // Because the selections are stored instantly, load from the saved collectionr rather than the control
                            // It is possible the control has not been redrawn at the time
                            List<string> selectedScheduleNames = new List<string>() { };
                            StringCollection savedSchedules = Configurator.GetConfigCollection("selected_schedule_names");
                            if (savedSchedules != null)
                            {
                                foreach (String savedScheduleName in savedSchedules)
                                {
                                    selectedScheduleNames.Add(savedScheduleName);
                                }
                            }

                            string[] scheduleInfo = new string[2] { scheduleName, scheduleNextRunTime };
                            bool doesScheduleExistInQueue = false;
                            // See if what is in the activeSchedulesQueue file is a checked schedule still
                            foreach (string[] activeSchedule in this.ActiveSchedules)
                            {
                                if (activeSchedule[0] == scheduleName && activeSchedule[1] == scheduleNextRunTime)
                                {
                                    this.Logger.Log(String.Format("Schedule {0} at {1} already in the schedules queue", scheduleName, scheduleNextRunTime));
                                    doesScheduleExistInQueue = true;
                                }
                            }
                            if (doesScheduleExistInQueue == false)
                            {
                                // Only add checked schedule names
                                if (selectedScheduleNames.Any(scheduleInfo.Contains) == true)
                                {
                                    this.ActiveSchedules.Add(scheduleInfo);
                                    WriteToActivityGrid(String.Format("Schedule {0} at {1} added to the schedules queue", scheduleName, scheduleNextRunTime), activityId);
                                    this.Logger.Log(String.Format("Schedule {0} at {1} added to the schedules queue", scheduleName, scheduleNextRunTime));
                                    activeSchedulesQueueFileWriter.WriteLine(String.Format("{0}|{1}", scheduleName, scheduleNextRunTime));
                                }
                                else
                                {
                                    this.Logger.Log(String.Format("Schedule {0} at {1} not added to schedules queue because it is not selected", scheduleName, scheduleNextRunTime));
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.Logger.Log("No rows found in the query for new schedules");
                }
                dr.Close();
            }
            catch (NpgsqlException ne)
            {
                this.Logger.Log("Connecting to repository to update the active queue failed. Ignoring for now, will update at next interval");
                this.Logger.Log(ne.Message);
            }
            WriteToActivityGrid("Completed checking for new schedules", activityId);

            // Look at the Active Queue and determine if anything is past it's due date
            foreach (string[] queuedSchedule in this.ActiveSchedules.Reverse<string[]>())
            {
                if (DateTime.Now.ToUniversalTime() > DateTime.Parse(queuedSchedule[1]))
                {
                    activityId = this.nextActivityId;
                    this.nextActivityId++;
                    this.Logger.Log(String.Format("Schedule {0} at {1} is now eligible to be run.", queuedSchedule[0], queuedSchedule[1]));
                    WriteToActivityGrid(String.Format("Schedule {0} at {1} is now eligible to be run. Queuing to run.", queuedSchedule[0], queuedSchedule[1]), activityId);
                    // Add to the actionQueue at the top
                    asyncActionQueue.Insert(0, () =>
                    {
                        var queuedScheduleName = queuedSchedule[0];
                        var queuedScheduleRunTime = queuedSchedule[1];
                        WriteToActivityGrid(String.Format("Running Schedule {0} at {1}.", queuedSchedule[0], queuedSchedule[1]), activityId);
                        bool result = SendEmail(queuedScheduleName);

                        // Even if failure, remove from active schedules queue
                        this.Logger.Log(String.Format("Removing {0} at {1} from active schedules queue", queuedSchedule[0], queuedSchedule[1]));
                        this.ActiveSchedules.Remove(queuedSchedule);
                        // Write the remaining queues.
                        using (StreamWriter activeSchedulesQueueFileWriter = new StreamWriter(this.ActiveSchedulesQueueFilename, false, new UTF8Encoding()))
                        {
                            foreach (string[] currentlyQueuedSchedules in this.ActiveSchedules)
                            {
                                activeSchedulesQueueFileWriter.WriteLine(String.Format("{0}|{1}", currentlyQueuedSchedules[0], currentlyQueuedSchedules[1]));
                            }
                        }

                        if (result)
                        {
                            WriteToActivityGrid(String.Format("Completed Schedule {0} at {1}.", queuedScheduleName, queuedScheduleRunTime), activityId);
                            return String.Format("Completed Schedule {0} at {1}.", queuedScheduleName, queuedScheduleRunTime);
                        }
                        else
                        {
                            this.ActiveSchedules.Remove(queuedSchedule);
                            WriteToActivityGrid(String.Format("Schedule {0} at {1} failed, see log.", queuedScheduleName, queuedScheduleRunTime), activityId);
                            return String.Format("Schedule {0} at {1} failed, see log.", queuedScheduleName, queuedScheduleRunTime);
                        }
                    }
                    );

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
            }
        }

        private void StartTimer(object sender, EventArgs e)
        {
            var activityId = this.nextActivityId;
            this.nextActivityId++;
            WriteToActivityGrid("Enabling monitoring and running of email schedules", activityId);
            // Run once to pick up anything that will run at the next 15 minute time period
            updateSchedulesQueue();
            this.scheduleMonitorTimer.Start();
        }

        private void StopTimer(object sender, EventArgs e)
        {
            var activityId = this.nextActivityId;
            this.nextActivityId++;
            WriteToActivityGrid("Disabling monitoring and running of email schedules", activityId);
            this.scheduleMonitorTimer.Stop();
        }

        private void RunSelectedSchedulesOnce(object sender, EventArgs e)
        {
            WriteToActivityGrid("Starting a single run of each selected schedule");

            foreach (object itemChecked in availableSchedulesList.CheckedItems)
            {
                WriteToActivityGrid("Queuing schedule \"" + itemChecked.ToString() + "\"");
                //this.SendEmail(itemChecked.ToString());
                // Add to the actionQueue
                asyncActionQueue.Add(() =>
                {
                    var resultFlag = SendEmail(itemChecked.ToString());
                    if (resultFlag)
                    {
                        return "Scheduled e-mail completed";
                    }
                    else
                    {
                        return "Scheduled e-mail failed, see log for details";
                    }
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
        }
    }
}