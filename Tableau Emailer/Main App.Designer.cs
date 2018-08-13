namespace Behold_Emailer
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.singleViewLocation = new System.Windows.Forms.TextBox();
            this.testFilename = new System.Windows.Forms.TextBox();
            this.singleExportSite = new System.Windows.Forms.TextBox();
            this.refreshSchedulesButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.powerPointExportFileName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableauServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.emailServerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pageNumberingWatermarkingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localSettingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runAllSelectedSchedulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainTabBar = new System.Windows.Forms.TabControl();
            this.singleExportTab = new System.Windows.Forms.TabPage();
            this.exportTypeDropDown = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.testEmailSubject = new System.Windows.Forms.TextBox();
            this.testEmailRecipient = new System.Windows.Forms.TextBox();
            this.singleUsernameForImpersonation = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.createExport = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sendEmail = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.batchExportTab = new System.Windows.Forms.TabPage();
            this.bulkUsernameToImpersonateAs = new System.Windows.Forms.TextBox();
            this.bulkEmailSubject = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pickBulkCSVFile = new System.Windows.Forms.Button();
            this.sendBatchEmails = new System.Windows.Forms.Button();
            this.bulkEmailPreview = new System.Windows.Forms.DataGridView();
            this.schedulesTab = new System.Windows.Forms.TabPage();
            this.runSchedulesGroupBox = new System.Windows.Forms.GroupBox();
            this.enableSchedulesRadioButton = new System.Windows.Forms.RadioButton();
            this.disableSchedulesRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.availableSchedulesList = new System.Windows.Forms.CheckedListBox();
            this.powerPointTab = new System.Windows.Forms.TabPage();
            this.loadPowerPointListFile = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.powerPointUserToGenerateAs = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.powerPointStatusView = new System.Windows.Forms.DataGridView();
            this.updatePowerPointButton = new System.Windows.Forms.Button();
            this.powerPointFilename = new System.Windows.Forms.TextBox();
            this.pickPowerPointTemplateButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.activityGrid = new System.Windows.Forms.DataGridView();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActivityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchCsvPicker = new System.Windows.Forms.OpenFileDialog();
            this.actionQueueBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.powerPointTemplatePicker = new System.Windows.Forms.OpenFileDialog();
            this.powerPointListPicker = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mainTabBar.SuspendLayout();
            this.singleExportTab.SuspendLayout();
            this.batchExportTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bulkEmailPreview)).BeginInit();
            this.schedulesTab.SuspendLayout();
            this.runSchedulesGroupBox.SuspendLayout();
            this.powerPointTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPointStatusView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.activityGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // singleViewLocation
            // 
            this.singleViewLocation.Location = new System.Drawing.Point(10, 124);
            this.singleViewLocation.Name = "singleViewLocation";
            this.singleViewLocation.Size = new System.Drawing.Size(243, 22);
            this.singleViewLocation.TabIndex = 6;
            this.toolTip1.SetToolTip(this.singleViewLocation, "This is the URL portion that describes the view (typically a single sheet\r\nin a w" +
        "orkbook, but could be a custom view)\r\n\r\nUse the portion of Tableau Server URL af" +
        "ter /views/ and before any \"?\"");
            this.singleViewLocation.TextChanged += new System.EventHandler(this.singleViewLocation_TextChanged);
            // 
            // testFilename
            // 
            this.testFilename.Location = new System.Drawing.Point(353, 63);
            this.testFilename.Name = "testFilename";
            this.testFilename.Size = new System.Drawing.Size(178, 22);
            this.testFilename.TabIndex = 18;
            this.toolTip1.SetToolTip(this.testFilename, "Do not include the file extension (.pdf), it will be added automatically");
            // 
            // singleExportSite
            // 
            this.singleExportSite.Location = new System.Drawing.Point(8, 64);
            this.singleExportSite.Name = "singleExportSite";
            this.singleExportSite.Size = new System.Drawing.Size(243, 22);
            this.singleExportSite.TabIndex = 5;
            this.singleExportSite.Text = "default";
            this.toolTip1.SetToolTip(this.singleExportSite, "Use \"default\" for default site or the site name in the Tableau Server URL");
            this.singleExportSite.TextChanged += new System.EventHandler(this.testSite_TextChanged);
            // 
            // refreshSchedulesButton
            // 
            this.refreshSchedulesButton.Location = new System.Drawing.Point(256, 213);
            this.refreshSchedulesButton.Name = "refreshSchedulesButton";
            this.refreshSchedulesButton.Size = new System.Drawing.Size(143, 41);
            this.refreshSchedulesButton.TabIndex = 39;
            this.refreshSchedulesButton.Text = "Refresh Schedules List";
            this.toolTip1.SetToolTip(this.refreshSchedulesButton, "Gets Disabled Schedules from Tableau Repository");
            this.refreshSchedulesButton.UseVisualStyleBackColor = true;
            this.refreshSchedulesButton.Click += new System.EventHandler(this.ReloadSubscriptions);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Window;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Site Content URL";
            this.toolTip1.SetToolTip(this.label5, "Site Content URL is the portion of the Tableau Server URL\r\nthat names the site. F" +
        "or example:\r\nhttp://mytableauserver.domain.com/site/my_site/projects\r\n\r\n\"my_site" +
        "\" would be the Site Content URL");
            // 
            // powerPointExportFileName
            // 
            this.powerPointExportFileName.Location = new System.Drawing.Point(298, 31);
            this.powerPointExportFileName.Name = "powerPointExportFileName";
            this.powerPointExportFileName.Size = new System.Drawing.Size(178, 22);
            this.powerPointExportFileName.TabIndex = 84;
            this.toolTip1.SetToolTip(this.powerPointExportFileName, "Do not include the file extension (.pdf), it will be added automatically");
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.testToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableauServerToolStripMenuItem1,
            this.emailServerToolStripMenuItem1,
            this.pageNumberingWatermarkingToolStripMenuItem,
            this.localSettingsToolStripMenuItem1});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(72, 20);
            this.toolStripMenuItem1.Text = "Configure";
            this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tableauServerToolStripMenuItem1
            // 
            this.tableauServerToolStripMenuItem1.Name = "tableauServerToolStripMenuItem1";
            this.tableauServerToolStripMenuItem1.Size = new System.Drawing.Size(250, 22);
            this.tableauServerToolStripMenuItem1.Text = "Tableau Server";
            this.tableauServerToolStripMenuItem1.Click += new System.EventHandler(this.tableauServerToolStripMenuItem1_Click);
            // 
            // emailServerToolStripMenuItem1
            // 
            this.emailServerToolStripMenuItem1.Name = "emailServerToolStripMenuItem1";
            this.emailServerToolStripMenuItem1.Size = new System.Drawing.Size(250, 22);
            this.emailServerToolStripMenuItem1.Text = "Email Server";
            this.emailServerToolStripMenuItem1.Click += new System.EventHandler(this.emailServerToolStripMenuItem1_Click);
            // 
            // pageNumberingWatermarkingToolStripMenuItem
            // 
            this.pageNumberingWatermarkingToolStripMenuItem.Name = "pageNumberingWatermarkingToolStripMenuItem";
            this.pageNumberingWatermarkingToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.pageNumberingWatermarkingToolStripMenuItem.Text = "Page Numbering / Watermarking";
            this.pageNumberingWatermarkingToolStripMenuItem.Click += new System.EventHandler(this.pageNumberingWatermarkingToolStripMenuItem_Click);
            // 
            // localSettingsToolStripMenuItem1
            // 
            this.localSettingsToolStripMenuItem1.Name = "localSettingsToolStripMenuItem1";
            this.localSettingsToolStripMenuItem1.Size = new System.Drawing.Size(250, 22);
            this.localSettingsToolStripMenuItem1.Text = "Local Settings";
            this.localSettingsToolStripMenuItem1.Click += new System.EventHandler(this.localSettingsToolStripMenuItem1_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runAllSelectedSchedulesToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // runAllSelectedSchedulesToolStripMenuItem
            // 
            this.runAllSelectedSchedulesToolStripMenuItem.Name = "runAllSelectedSchedulesToolStripMenuItem";
            this.runAllSelectedSchedulesToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.runAllSelectedSchedulesToolStripMenuItem.Text = "Run All Selected Schedules Now";
            this.runAllSelectedSchedulesToolStripMenuItem.Click += new System.EventHandler(this.RunSelectedSchedulesOnce);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mainTabBar);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(794, 638);
            this.splitContainer1.SplitterDistance = 333;
            this.splitContainer1.TabIndex = 27;
            // 
            // mainTabBar
            // 
            this.mainTabBar.CausesValidation = false;
            this.mainTabBar.Controls.Add(this.singleExportTab);
            this.mainTabBar.Controls.Add(this.batchExportTab);
            this.mainTabBar.Controls.Add(this.schedulesTab);
            this.mainTabBar.Controls.Add(this.powerPointTab);
            this.mainTabBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabBar.Location = new System.Drawing.Point(6, 0);
            this.mainTabBar.Multiline = true;
            this.mainTabBar.Name = "mainTabBar";
            this.mainTabBar.SelectedIndex = 0;
            this.mainTabBar.Size = new System.Drawing.Size(776, 361);
            this.mainTabBar.TabIndex = 2;
            // 
            // singleExportTab
            // 
            this.singleExportTab.Controls.Add(this.exportTypeDropDown);
            this.singleExportTab.Controls.Add(this.label3);
            this.singleExportTab.Controls.Add(this.label2);
            this.singleExportTab.Controls.Add(this.testEmailSubject);
            this.singleExportTab.Controls.Add(this.singleExportSite);
            this.singleExportTab.Controls.Add(this.testFilename);
            this.singleExportTab.Controls.Add(this.singleViewLocation);
            this.singleExportTab.Controls.Add(this.testEmailRecipient);
            this.singleExportTab.Controls.Add(this.singleUsernameForImpersonation);
            this.singleExportTab.Controls.Add(this.label14);
            this.singleExportTab.Controls.Add(this.label13);
            this.singleExportTab.Controls.Add(this.label5);
            this.singleExportTab.Controls.Add(this.label31);
            this.singleExportTab.Controls.Add(this.createExport);
            this.singleExportTab.Controls.Add(this.label6);
            this.singleExportTab.Controls.Add(this.label4);
            this.singleExportTab.Controls.Add(this.sendEmail);
            this.singleExportTab.Controls.Add(this.label12);
            this.singleExportTab.Controls.Add(this.Label7);
            this.singleExportTab.Location = new System.Drawing.Point(4, 25);
            this.singleExportTab.Name = "singleExportTab";
            this.singleExportTab.Padding = new System.Windows.Forms.Padding(3);
            this.singleExportTab.Size = new System.Drawing.Size(768, 332);
            this.singleExportTab.TabIndex = 7;
            this.singleExportTab.Text = "Single Export";
            this.singleExportTab.UseVisualStyleBackColor = true;
            // 
            // exportTypeDropDown
            // 
            this.exportTypeDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exportTypeDropDown.FormattingEnabled = true;
            this.exportTypeDropDown.Items.AddRange(new object[] {
            "Full PDF - All Sheets",
            "PDF - Single Sheet",
            "PNG - Single Sheet",
            "CSV - Single Sheet"});
            this.exportTypeDropDown.Location = new System.Drawing.Point(9, 243);
            this.exportTypeDropDown.Name = "exportTypeDropDown";
            this.exportTypeDropDown.Size = new System.Drawing.Size(239, 24);
            this.exportTypeDropDown.TabIndex = 58;
            this.exportTypeDropDown.SelectedIndexChanged += new System.EventHandler(this.exportTypeDropDown_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 223);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 16);
            this.label3.TabIndex = 57;
            this.label3.Text = "Export Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 56;
            this.label2.Text = "Export Configuration";
            // 
            // testEmailSubject
            // 
            this.testEmailSubject.Location = new System.Drawing.Point(579, 63);
            this.testEmailSubject.Name = "testEmailSubject";
            this.testEmailSubject.Size = new System.Drawing.Size(152, 22);
            this.testEmailSubject.TabIndex = 15;
            this.testEmailSubject.Validating += new System.ComponentModel.CancelEventHandler(this.ValidateTextBox);
            // 
            // testEmailRecipient
            // 
            this.testEmailRecipient.Location = new System.Drawing.Point(579, 121);
            this.testEmailRecipient.Name = "testEmailRecipient";
            this.testEmailRecipient.Size = new System.Drawing.Size(152, 22);
            this.testEmailRecipient.TabIndex = 16;
            // 
            // singleUsernameForImpersonation
            // 
            this.singleUsernameForImpersonation.Location = new System.Drawing.Point(8, 185);
            this.singleUsernameForImpersonation.Name = "singleUsernameForImpersonation";
            this.singleUsernameForImpersonation.Size = new System.Drawing.Size(240, 22);
            this.singleUsernameForImpersonation.TabIndex = 7;
            this.singleUsernameForImpersonation.TextChanged += new System.EventHandler(this.singleUsernameForImpersonation_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(341, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(150, 16);
            this.label14.TabIndex = 55;
            this.label14.Text = "Generate Single File";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(561, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(135, 16);
            this.label13.TabIndex = 54;
            this.label13.Text = "Send Single Email";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(576, 47);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(90, 16);
            this.label31.TabIndex = 53;
            this.label31.Text = "Email Subject";
            // 
            // createExport
            // 
            this.createExport.Location = new System.Drawing.Point(421, 150);
            this.createExport.Name = "createExport";
            this.createExport.Size = new System.Drawing.Size(110, 49);
            this.createExport.TabIndex = 19;
            this.createExport.Text = "Generate File";
            this.createExport.UseVisualStyleBackColor = true;
            this.createExport.Click += new System.EventHandler(this.QueueSingleExport);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(350, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "File Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "View Location";
            // 
            // sendEmail
            // 
            this.sendEmail.Location = new System.Drawing.Point(618, 150);
            this.sendEmail.Name = "sendEmail";
            this.sendEmail.Size = new System.Drawing.Size(114, 49);
            this.sendEmail.TabIndex = 17;
            this.sendEmail.Text = "Send Email";
            this.sendEmail.UseVisualStyleBackColor = true;
            this.sendEmail.Click += new System.EventHandler(this.QueueSingleEmail);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(576, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(156, 16);
            this.label12.TabIndex = 31;
            this.label12.Text = "Email Recipient Address";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.Location = new System.Drawing.Point(7, 166);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(263, 16);
            this.Label7.TabIndex = 14;
            this.Label7.Text = "Username to Generate Export As (optional)";
            // 
            // batchExportTab
            // 
            this.batchExportTab.Controls.Add(this.bulkUsernameToImpersonateAs);
            this.batchExportTab.Controls.Add(this.bulkEmailSubject);
            this.batchExportTab.Controls.Add(this.label24);
            this.batchExportTab.Controls.Add(this.label23);
            this.batchExportTab.Controls.Add(this.pickBulkCSVFile);
            this.batchExportTab.Controls.Add(this.sendBatchEmails);
            this.batchExportTab.Controls.Add(this.bulkEmailPreview);
            this.batchExportTab.Location = new System.Drawing.Point(4, 25);
            this.batchExportTab.Name = "batchExportTab";
            this.batchExportTab.Padding = new System.Windows.Forms.Padding(3);
            this.batchExportTab.Size = new System.Drawing.Size(768, 332);
            this.batchExportTab.TabIndex = 9;
            this.batchExportTab.Text = "Batch Export";
            this.batchExportTab.UseVisualStyleBackColor = true;
            // 
            // bulkUsernameToImpersonateAs
            // 
            this.bulkUsernameToImpersonateAs.Location = new System.Drawing.Point(348, 42);
            this.bulkUsernameToImpersonateAs.Name = "bulkUsernameToImpersonateAs";
            this.bulkUsernameToImpersonateAs.Size = new System.Drawing.Size(235, 22);
            this.bulkUsernameToImpersonateAs.TabIndex = 64;
            // 
            // bulkEmailSubject
            // 
            this.bulkEmailSubject.Location = new System.Drawing.Point(8, 42);
            this.bulkEmailSubject.Name = "bulkEmailSubject";
            this.bulkEmailSubject.Size = new System.Drawing.Size(271, 22);
            this.bulkEmailSubject.TabIndex = 62;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(345, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(270, 16);
            this.label24.TabIndex = 65;
            this.label24.Text = "Username to Generate Exports As (optional)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(5, 23);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(90, 16);
            this.label23.TabIndex = 63;
            this.label23.Text = "Email Subject";
            // 
            // pickBulkCSVFile
            // 
            this.pickBulkCSVFile.Location = new System.Drawing.Point(499, 240);
            this.pickBulkCSVFile.Name = "pickBulkCSVFile";
            this.pickBulkCSVFile.Size = new System.Drawing.Size(116, 52);
            this.pickBulkCSVFile.TabIndex = 61;
            this.pickBulkCSVFile.Text = "Load from CSV";
            this.pickBulkCSVFile.UseVisualStyleBackColor = true;
            // 
            // sendBatchEmails
            // 
            this.sendBatchEmails.Enabled = false;
            this.sendBatchEmails.Location = new System.Drawing.Point(632, 240);
            this.sendBatchEmails.Name = "sendBatchEmails";
            this.sendBatchEmails.Size = new System.Drawing.Size(127, 52);
            this.sendBatchEmails.TabIndex = 3;
            this.sendBatchEmails.Text = "Send Emails";
            this.sendBatchEmails.UseVisualStyleBackColor = true;
            this.sendBatchEmails.Click += new System.EventHandler(this.sendBulkEmails_Click);
            // 
            // bulkEmailPreview
            // 
            this.bulkEmailPreview.AllowUserToAddRows = false;
            this.bulkEmailPreview.AllowUserToDeleteRows = false;
            this.bulkEmailPreview.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.bulkEmailPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bulkEmailPreview.Location = new System.Drawing.Point(3, 70);
            this.bulkEmailPreview.Name = "bulkEmailPreview";
            this.bulkEmailPreview.ReadOnly = true;
            this.bulkEmailPreview.ShowEditingIcon = false;
            this.bulkEmailPreview.Size = new System.Drawing.Size(765, 152);
            this.bulkEmailPreview.TabIndex = 1;
            this.bulkEmailPreview.Visible = false;
            // 
            // schedulesTab
            // 
            this.schedulesTab.Controls.Add(this.runSchedulesGroupBox);
            this.schedulesTab.Controls.Add(this.refreshSchedulesButton);
            this.schedulesTab.Controls.Add(this.label1);
            this.schedulesTab.Controls.Add(this.availableSchedulesList);
            this.schedulesTab.Cursor = System.Windows.Forms.Cursors.Default;
            this.schedulesTab.Location = new System.Drawing.Point(4, 25);
            this.schedulesTab.Name = "schedulesTab";
            this.schedulesTab.Padding = new System.Windows.Forms.Padding(3);
            this.schedulesTab.Size = new System.Drawing.Size(768, 332);
            this.schedulesTab.TabIndex = 5;
            this.schedulesTab.Text = "Tableau Server Schedules";
            this.schedulesTab.UseVisualStyleBackColor = true;
            this.schedulesTab.Enter += new System.EventHandler(this.ReloadSubscriptions);
            // 
            // runSchedulesGroupBox
            // 
            this.runSchedulesGroupBox.Controls.Add(this.enableSchedulesRadioButton);
            this.runSchedulesGroupBox.Controls.Add(this.disableSchedulesRadioButton);
            this.runSchedulesGroupBox.Location = new System.Drawing.Point(10, 213);
            this.runSchedulesGroupBox.Name = "runSchedulesGroupBox";
            this.runSchedulesGroupBox.Size = new System.Drawing.Size(194, 68);
            this.runSchedulesGroupBox.TabIndex = 42;
            this.runSchedulesGroupBox.TabStop = false;
            this.runSchedulesGroupBox.Text = "Monitor and Run Schedules";
            // 
            // enableSchedulesRadioButton
            // 
            this.enableSchedulesRadioButton.AutoSize = true;
            this.enableSchedulesRadioButton.Location = new System.Drawing.Point(6, 21);
            this.enableSchedulesRadioButton.Name = "enableSchedulesRadioButton";
            this.enableSchedulesRadioButton.Size = new System.Drawing.Size(77, 20);
            this.enableSchedulesRadioButton.TabIndex = 40;
            this.enableSchedulesRadioButton.Text = "Enabled";
            this.enableSchedulesRadioButton.UseVisualStyleBackColor = true;
            this.enableSchedulesRadioButton.CheckedChanged += new System.EventHandler(this.enableSchedulesRadioButton_CheckedChanged);
            // 
            // disableSchedulesRadioButton
            // 
            this.disableSchedulesRadioButton.AutoSize = true;
            this.disableSchedulesRadioButton.Checked = true;
            this.disableSchedulesRadioButton.Location = new System.Drawing.Point(6, 42);
            this.disableSchedulesRadioButton.Name = "disableSchedulesRadioButton";
            this.disableSchedulesRadioButton.Size = new System.Drawing.Size(81, 20);
            this.disableSchedulesRadioButton.TabIndex = 41;
            this.disableSchedulesRadioButton.TabStop = true;
            this.disableSchedulesRadioButton.Text = "Disabled";
            this.disableSchedulesRadioButton.UseVisualStyleBackColor = true;
            this.disableSchedulesRadioButton.CheckedChanged += new System.EventHandler(this.disableSchedulesRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(354, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Available Tableau Server Subscription Schedules";
            // 
            // availableSchedulesList
            // 
            this.availableSchedulesList.FormattingEnabled = true;
            this.availableSchedulesList.Location = new System.Drawing.Point(6, 41);
            this.availableSchedulesList.Name = "availableSchedulesList";
            this.availableSchedulesList.Size = new System.Drawing.Size(393, 157);
            this.availableSchedulesList.TabIndex = 0;
            this.availableSchedulesList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.availableSchedulesList_ItemChecked);
            // 
            // powerPointTab
            // 
            this.powerPointTab.Controls.Add(this.loadPowerPointListFile);
            this.powerPointTab.Controls.Add(this.powerPointExportFileName);
            this.powerPointTab.Controls.Add(this.label10);
            this.powerPointTab.Controls.Add(this.powerPointUserToGenerateAs);
            this.powerPointTab.Controls.Add(this.label8);
            this.powerPointTab.Controls.Add(this.label9);
            this.powerPointTab.Controls.Add(this.powerPointStatusView);
            this.powerPointTab.Controls.Add(this.updatePowerPointButton);
            this.powerPointTab.Controls.Add(this.powerPointFilename);
            this.powerPointTab.Controls.Add(this.pickPowerPointTemplateButton);
            this.powerPointTab.Location = new System.Drawing.Point(4, 25);
            this.powerPointTab.Name = "powerPointTab";
            this.powerPointTab.Padding = new System.Windows.Forms.Padding(3);
            this.powerPointTab.Size = new System.Drawing.Size(768, 332);
            this.powerPointTab.TabIndex = 10;
            this.powerPointTab.Text = "PowerPoint";
            this.powerPointTab.UseVisualStyleBackColor = true;
            // 
            // loadPowerPointListFile
            // 
            this.loadPowerPointListFile.Location = new System.Drawing.Point(476, 251);
            this.loadPowerPointListFile.Name = "loadPowerPointListFile";
            this.loadPowerPointListFile.Size = new System.Drawing.Size(116, 54);
            this.loadPowerPointListFile.TabIndex = 86;
            this.loadPowerPointListFile.Text = "Load from CSV";
            this.loadPowerPointListFile.UseVisualStyleBackColor = true;
            this.loadPowerPointListFile.Click += new System.EventHandler(this.pickPowerpointInputFile_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(295, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 16);
            this.label10.TabIndex = 85;
            this.label10.Text = "Export File Name";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // powerPointUserToGenerateAs
            // 
            this.powerPointUserToGenerateAs.Location = new System.Drawing.Point(495, 31);
            this.powerPointUserToGenerateAs.Name = "powerPointUserToGenerateAs";
            this.powerPointUserToGenerateAs.Size = new System.Drawing.Size(235, 22);
            this.powerPointUserToGenerateAs.TabIndex = 82;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(492, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(270, 16);
            this.label8.TabIndex = 83;
            this.label8.Text = "Username to Generate Exports As (optional)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(216, 16);
            this.label9.TabIndex = 81;
            this.label9.Text = "PowerPoint Template File Location";
            // 
            // powerPointStatusView
            // 
            this.powerPointStatusView.AllowUserToAddRows = false;
            this.powerPointStatusView.AllowUserToDeleteRows = false;
            this.powerPointStatusView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.powerPointStatusView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.powerPointStatusView.Location = new System.Drawing.Point(2, 93);
            this.powerPointStatusView.Name = "powerPointStatusView";
            this.powerPointStatusView.ReadOnly = true;
            this.powerPointStatusView.ShowEditingIcon = false;
            this.powerPointStatusView.Size = new System.Drawing.Size(762, 151);
            this.powerPointStatusView.TabIndex = 79;
            this.powerPointStatusView.Visible = false;
            // 
            // updatePowerPointButton
            // 
            this.updatePowerPointButton.Location = new System.Drawing.Point(598, 250);
            this.updatePowerPointButton.Name = "updatePowerPointButton";
            this.updatePowerPointButton.Size = new System.Drawing.Size(164, 55);
            this.updatePowerPointButton.TabIndex = 78;
            this.updatePowerPointButton.Text = "Fill in PowerPoint Template";
            this.updatePowerPointButton.UseVisualStyleBackColor = true;
            this.updatePowerPointButton.Click += new System.EventHandler(this.fillInPowerpoint_Click);
            // 
            // powerPointFilename
            // 
            this.powerPointFilename.Location = new System.Drawing.Point(9, 31);
            this.powerPointFilename.Name = "powerPointFilename";
            this.powerPointFilename.Size = new System.Drawing.Size(271, 22);
            this.powerPointFilename.TabIndex = 77;
            // 
            // pickPowerPointTemplateButton
            // 
            this.pickPowerPointTemplateButton.Location = new System.Drawing.Point(116, 59);
            this.pickPowerPointTemplateButton.Name = "pickPowerPointTemplateButton";
            this.pickPowerPointTemplateButton.Size = new System.Drawing.Size(164, 28);
            this.pickPowerPointTemplateButton.TabIndex = 62;
            this.pickPowerPointTemplateButton.Text = "Browse Template File";
            this.pickPowerPointTemplateButton.UseVisualStyleBackColor = true;
            this.pickPowerPointTemplateButton.Click += new System.EventHandler(this.pickPowerPointFileButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.activityGrid);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(6, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(781, 266);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Activity";
            // 
            // activityGrid
            // 
            this.activityGrid.AllowUserToAddRows = false;
            this.activityGrid.AllowUserToDeleteRows = false;
            this.activityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activityGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Timestamp,
            this.ActivityID,
            this.Message});
            this.activityGrid.Location = new System.Drawing.Point(3, 21);
            this.activityGrid.Name = "activityGrid";
            this.activityGrid.ReadOnly = true;
            this.activityGrid.Size = new System.Drawing.Size(773, 239);
            this.activityGrid.TabIndex = 0;
            // 
            // Timestamp
            // 
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.MinimumWidth = 150;
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;
            this.Timestamp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Timestamp.Width = 150;
            // 
            // ActivityID
            // 
            this.ActivityID.HeaderText = "Activity ID";
            this.ActivityID.Name = "ActivityID";
            this.ActivityID.ReadOnly = true;
            // 
            // Message
            // 
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            this.Message.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Message.Width = 575;
            // 
            // actionQueueBackgroundWorker
            // 
            this.actionQueueBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.actionQueueBackgroundWorker_DoWork);
            this.actionQueueBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.actionQueueBackgroundWorker_RunWorkerCompleted);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(799, 645);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Behold! Emailer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mainTabBar.ResumeLayout(false);
            this.singleExportTab.ResumeLayout(false);
            this.singleExportTab.PerformLayout();
            this.batchExportTab.ResumeLayout(false);
            this.batchExportTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bulkEmailPreview)).EndInit();
            this.schedulesTab.ResumeLayout(false);
            this.schedulesTab.PerformLayout();
            this.runSchedulesGroupBox.ResumeLayout(false);
            this.runSchedulesGroupBox.PerformLayout();
            this.powerPointTab.ResumeLayout(false);
            this.powerPointTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerPointStatusView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.activityGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runAllSelectedSchedulesToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableauServerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem emailServerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pageNumberingWatermarkingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localSettingsToolStripMenuItem1;
        private System.Windows.Forms.TabControl mainTabBar;
        private System.Windows.Forms.TabPage schedulesTab;
        private System.Windows.Forms.Button refreshSchedulesButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox availableSchedulesList;
        private System.Windows.Forms.TabPage singleExportTab;
        private System.Windows.Forms.TextBox testEmailSubject;
        private System.Windows.Forms.TextBox singleExportSite;
        private System.Windows.Forms.TextBox testFilename;
        private System.Windows.Forms.TextBox singleViewLocation;
        private System.Windows.Forms.TextBox testEmailRecipient;
        private System.Windows.Forms.TextBox singleUsernameForImpersonation;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button createExport;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sendEmail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label Label7;
        private System.Windows.Forms.TabPage batchExportTab;
        private System.Windows.Forms.TextBox bulkUsernameToImpersonateAs;
        private System.Windows.Forms.TextBox bulkEmailSubject;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button pickBulkCSVFile;
        private System.Windows.Forms.Button sendBatchEmails;
        private System.Windows.Forms.DataGridView bulkEmailPreview;
        private System.Windows.Forms.OpenFileDialog batchCsvPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView activityGrid;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ComboBox exportTypeDropDown;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker actionQueueBackgroundWorker;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActivityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.GroupBox runSchedulesGroupBox;
        private System.Windows.Forms.RadioButton disableSchedulesRadioButton;
        private System.Windows.Forms.RadioButton enableSchedulesRadioButton;
        private System.Windows.Forms.TabPage powerPointTab;
        private System.Windows.Forms.Button pickPowerPointTemplateButton;
        private System.Windows.Forms.OpenFileDialog powerPointTemplatePicker;
        private System.Windows.Forms.TextBox powerPointFilename;
        private System.Windows.Forms.Button updatePowerPointButton;
        private System.Windows.Forms.TextBox powerPointExportFileName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox powerPointUserToGenerateAs;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView powerPointStatusView;
        private System.Windows.Forms.Button loadPowerPointListFile;
        private System.Windows.Forms.OpenFileDialog powerPointListPicker;
    }
}

