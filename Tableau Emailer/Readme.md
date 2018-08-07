# Behold! Emailer 2.0 Readme

Behold! Emailer is a Windows application designed to run on a machine with access to both tabcmd and the Tableau Repository, and that the Tableau Server has been set to trust
for Trusted Tickets (https://onlinehelp.tableau.com/current/server/en-us/trusted_auth_trustIP.htm )

It has been tested up to Tableau Server 2018.1.

It expands Tableau Server's native capabilities with regards to exporting and e-mails, particularly scheduling e-mails with PDF or CSV attachments, to allow schedules selected in Tableau Server to send out PDF attachments, as well as giving a simple interface for batch runs of e-mails with different filter options in their view URLs.

When exporting to PDF, Tableau Server will export a Worksheet that has scrolling information in full. If that Worksheet is included within a Dashboard, the export of that Dashboard will only show the visible rows. To create Dashboards that can export all of their data, you'll need to make the underlying scrolling Worksheets visible tabs, typically placed right after the Dashboard, and export using the FULLPDF option (or simply choose the Worksheet, rather than the dashboard).

Because there is only one tabcmd configuration file for the system, all actions are Queued into an Activity Queue. Different actions within the application (pressing the Export button, or a schedule time threshold passing) will add that Action into the Action Queue. Then the queue begins working until all actions have been cleared. 

The interface include a main screen with the following main elements:

1. Configuration Menu
2. Single Export Tab
3. Batch Export Tab
4. Tableau Server Schedules Tab
5. Activity Log Pane

## Configuration Menu
All main shared configurations are accessed in the **Configure** menu. Each menu option handles the configuration of a different component of the Behold! Emailer toolchain.

### Tableau Server

The first things you need to input are your Tableau Server URL (including http:// or https://), a Tableau Server Administrator Username and that user's Password. This is necessary to establish the first tabcmd session so that all of the file exporting will work smoothly.

Next, enter the password for the "readonly" user of the Tableau Server repository. If you've never set the password for this user before, the instructions are at https://onlinehelp.tableau.com/current/server/en-us/perf_collect_server_repo.htm. We need this repository user for access to the scheduling information among other things. As the "readonly" user, there is no danger to your repository and no ability to write or change anything.

Next, browse for the location of your tabcmd executable file. If you are installing Tableau Emailer on your Tableau Server, this will be Program Files\Tableau\Tableau Server\{version}\bin\, but you can also install tabcmd separately on a different machine using the installer at Program Files\Tableau\Tableau Server\{version}\extras\TabcmdInstaller-x64.exe, and in that case the location will be where ever you installed it.

Lastly, browse for the location of your tabcmd configuration files. You may need to run tabcmd at least once from the command line for the configuration folder to exist; a simple tabcmd login command will suffice to generate it. The file should be located in C:\Users\{user}\AppData\Local\Tableau\Tabcmd\ . The file is called tabcmd-session.xml, if you see that, you've found the right folder.

Now that you have all of those things, press the Save button to leave the dialog.


### Email Server

Behold! Emailer requires a standard SMTP server on the network to send out e-mails. 

Enter in the server name or IP address first. 

Email Sender Address is the name that will show up in the From: field of the e-mail. This can be a "noreply@yourcompany.com" type address or something that indicates it comes from Tableau.

The message body of the e-mail will be based on the templates files you select. The HTML version will show in most e-mail applications, but you should have a plain text version as well for a backup.

An HTML template can be as plain as:

	<html>
	<head></head>

	<body>

	<p>Here is your e-mail
	</body></html>`


Currently there is no type of substitution of phrases within these templates.


### Page Numbering / Watermarking

The **Page Numbering / Watermarking** configuration dialog allows you to add additional text or images if you are exporting a PDF from tabcmd. There are currently three types:

    Text: Allows a message, including a timestamp in UTC
    Page Number: Numbers each page
    Image: An image that is shorter than 40px. (The viz itself starts at 50px from the top of the page and ends 50px before the bottom of the page, so anything larger would overlap the exported viz)

### Local Settings

The **Local Settings** dialog is for settings related to the machine Behold! Emailer is run on. Currently, it lets you specify the directory where any exported or generated file will be stored. 

One of the settings in the **Email Configurations** menu is **"Save Local Copy of Emailed Files?"** When this is selected, every file that is generated and e-mailed will be stored to this local folder.

## Single Export Tab
The **Single Export** tab in the main window is an easier interface than dealing with tabcmd directly, and a great way to test that your configurations are correct.y

On the left side are entry boxes to specify what view you want to export, and in what format.

* **Site Content URL** is the portion of the Tableau Server URL that specifies what site you are logged into. Use "default" if you are on the Default site. 
* **View Locatio**n is the portion of a Tableau Server view URL that specifies a single sheet view. This is the URL portion that describes the view (typically a single sheet
in a workbook, but could be a custom view). Use the portion of Tableau Server URL after /views/ and before any "?"
* **Username to Export As** allows you to export "as" a given user. This is useful for testing custom views or when you know a view has Row Level Security built in.
* **Export Type** is a dropdown for choosing between FULLPDF, PDF, PNG or CSV. FULLPDF gets all of the sheets of a workbook, even if you are only specifying a particular Sheet. 

In the middle is the configuration for exporting just a file. You do not need to specify the filename extension; this will be appended automatically depending on what you have selected as an export type.

On the right is the configuration for sending a single e-mail export. 

When you press the **Generate File** or **Send Email** buttons, the actions will be queued with the current values of the configuration boxes, which you can see in the **Activity Pane**. If there is nothing else in the queue, they will start running immediately. You can queue several different variations, or exports and e-mails all in a row.

## Batch Export Tab
You can filter Tableau Server views by setting Filter Values or Parameter values in the URL for the view. However, you need to URL encode these values among other requirements, which makes it tough to do consistently on the command line.

The **Batch Export** tab is designed to import a CSV file of a certain structure and then run through all of the views and filters that have been specified.

The headers for the CSV file (do include the headers!) are:

`Attachment Type,To:,CC:,BCC:,Site,View Location,Filter Field Name 1,Filter Values 1,Filter Field Name 2,Filter Values 2,Filter Name 3,Filter Values 3,...`

where the ... represents the fact that you can add up to 25 different Filter Name / Filter Value columns. These columns can set both Filters and Parameter values. Make sure to only send *single* values for Paramters. Filters on the other hand, can take multiple values if separated within by a semi-color **;**

Attachment type should be in lowercase one of the following:
* fullpdf
* pdf
* png
* csv

For example:

`Attachment Type,To:,CC:,BCC:,Site,View Location,Filter Field Name 1,Filter Values 1,Filter Field Name 2,Filter Values 2,Filter Name 3,Filter Values 3
fullpdf,aperson@tableau.com,,,default,BasicSuperstoreSQLServer/Sheet1,Category,Technology;Office Supplies,YEAR(Order Date),2013;2014,,`

A batch load will be queued as one action, and once it has run, you can see the status of each of the items in the **Batch Export** tab.

## Tableau Server Schedules Tab

The Behold! Emailer takes advantage of the Disabled Schedules technique pioneered by Matt Coles and Jonathan Drummey for VizAlerts to allow users of Tableau Server to select schedules from the Subscribe drop-down in their Server view, while the export is actually generated by Behold! Emailer instead of the Tableau Server Backgrounder process.

The **Tableau Server Schedules** tab allows you to configure which Disabled Schedules to listen to, and to turn this functionality on and off. 

If you have added any new schedules in Tableau Server, you may want to hit the "Refresh Schedules List" button; this will requery the Repository to get the latest list of Disabled Schedules. Click the ones you want to monitor until they are checked (these settings are saved automatically).

When the **Monitor and Run Schedules** radio button is set to "Enabled", a monitor process will fires off on the :01, the :16, the :31 and the :46 minute of the hour. Since the Tableau Server subscription options are 15 minute increments at the smallest, this will capture any next subscription to send. 

All of the scheduled e-mail schedules are stored in a text file named "active_schedules.csv" so that information will persist even if you have to restart the program. When a schedule is ready to be run, it will be queued into the Actions queue at the top of the queue. 

## Activity Log Pane
The Activity Log pane at the bottom of the main screen lists out actions that happen. It is not as verbose as the log file itself, and may reference that you should check the log file if something bad happens. 

Because actions are queued rather than run immediately, an Activity ID is assigned at queueing time, and all messages around that action should have the same Activity ID. This helps track what happened for a given actions, as there might be other activity log messages between the queuing and the actual performance of the activity.

