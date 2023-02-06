namespace LineParser
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.selectReportButton = new System.Windows.Forms.Button();
            this.reportDateInput = new System.Windows.Forms.DateTimePicker();
            this.reportDateLabel = new System.Windows.Forms.Label();
            this.userConfigGroup = new System.Windows.Forms.GroupBox();
            this.setupDBButton = new System.Windows.Forms.Button();
            this.testConnectionButton = new System.Windows.Forms.Button();
            this.authTypeLabel = new System.Windows.Forms.Label();
            this.passwordAuth = new System.Windows.Forms.RadioButton();
            this.windowsAuthButton = new System.Windows.Forms.RadioButton();
            this.serverNameInput = new System.Windows.Forms.TextBox();
            this.serverNameLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cleanDBButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.userConfigGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectReportButton
            // 
            this.selectReportButton.Location = new System.Drawing.Point(6, 127);
            this.selectReportButton.Name = "selectReportButton";
            this.selectReportButton.Size = new System.Drawing.Size(154, 31);
            this.selectReportButton.TabIndex = 0;
            this.selectReportButton.Text = "Select Report File";
            this.toolTip1.SetToolTip(this.selectReportButton, "Choose a report file to import");
            this.selectReportButton.UseVisualStyleBackColor = true;
            // 
            // reportDateInput
            // 
            this.reportDateInput.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.reportDateInput.Location = new System.Drawing.Point(6, 48);
            this.reportDateInput.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.reportDateInput.Name = "reportDateInput";
            this.reportDateInput.Size = new System.Drawing.Size(154, 23);
            this.reportDateInput.TabIndex = 1;
            // 
            // reportDateLabel
            // 
            this.reportDateLabel.AutoSize = true;
            this.reportDateLabel.Location = new System.Drawing.Point(6, 30);
            this.reportDateLabel.Name = "reportDateLabel";
            this.reportDateLabel.Size = new System.Drawing.Size(69, 15);
            this.reportDateLabel.TabIndex = 2;
            this.reportDateLabel.Text = "Report Date";
            // 
            // userConfigGroup
            // 
            this.userConfigGroup.Controls.Add(this.setupDBButton);
            this.userConfigGroup.Controls.Add(this.testConnectionButton);
            this.userConfigGroup.Controls.Add(this.authTypeLabel);
            this.userConfigGroup.Controls.Add(this.passwordAuth);
            this.userConfigGroup.Controls.Add(this.windowsAuthButton);
            this.userConfigGroup.Controls.Add(this.serverNameInput);
            this.userConfigGroup.Controls.Add(this.serverNameLabel);
            this.userConfigGroup.Location = new System.Drawing.Point(219, 12);
            this.userConfigGroup.Name = "userConfigGroup";
            this.userConfigGroup.Size = new System.Drawing.Size(186, 257);
            this.userConfigGroup.TabIndex = 3;
            this.userConfigGroup.TabStop = false;
            this.userConfigGroup.Text = "Configuration";
            // 
            // setupDBButton
            // 
            this.setupDBButton.Enabled = false;
            this.setupDBButton.Location = new System.Drawing.Point(7, 158);
            this.setupDBButton.Name = "setupDBButton";
            this.setupDBButton.Size = new System.Drawing.Size(156, 32);
            this.setupDBButton.TabIndex = 5;
            this.setupDBButton.Text = "Build DB";
            this.toolTip1.SetToolTip(this.setupDBButton, "Set up the database (first-time users)");
            this.setupDBButton.UseVisualStyleBackColor = true;
            this.setupDBButton.Click += new System.EventHandler(this.setupDBButton_Click);
            // 
            // testConnectionButton
            // 
            this.testConnectionButton.Location = new System.Drawing.Point(7, 196);
            this.testConnectionButton.Name = "testConnectionButton";
            this.testConnectionButton.Size = new System.Drawing.Size(156, 32);
            this.testConnectionButton.TabIndex = 5;
            this.testConnectionButton.Text = "Test Connection";
            this.toolTip1.SetToolTip(this.testConnectionButton, "Check your database connection");
            this.testConnectionButton.UseVisualStyleBackColor = true;
            this.testConnectionButton.Click += new System.EventHandler(this.testConnectionButton_Click);
            // 
            // authTypeLabel
            // 
            this.authTypeLabel.AutoSize = true;
            this.authTypeLabel.Location = new System.Drawing.Point(6, 90);
            this.authTypeLabel.Name = "authTypeLabel";
            this.authTypeLabel.Size = new System.Drawing.Size(113, 15);
            this.authTypeLabel.TabIndex = 4;
            this.authTypeLabel.Text = "Authentication Type";
            // 
            // passwordAuth
            // 
            this.passwordAuth.AutoSize = true;
            this.passwordAuth.Enabled = false;
            this.passwordAuth.Location = new System.Drawing.Point(6, 133);
            this.passwordAuth.Name = "passwordAuth";
            this.passwordAuth.Size = new System.Drawing.Size(157, 19);
            this.passwordAuth.TabIndex = 3;
            this.passwordAuth.TabStop = true;
            this.passwordAuth.Text = "Password Authentication";
            this.toolTip1.SetToolTip(this.passwordAuth, "Not yet implemented");
            this.passwordAuth.UseVisualStyleBackColor = true;
            // 
            // windowsAuthButton
            // 
            this.windowsAuthButton.AutoSize = true;
            this.windowsAuthButton.Location = new System.Drawing.Point(6, 108);
            this.windowsAuthButton.Name = "windowsAuthButton";
            this.windowsAuthButton.Size = new System.Drawing.Size(156, 19);
            this.windowsAuthButton.TabIndex = 2;
            this.windowsAuthButton.TabStop = true;
            this.windowsAuthButton.Text = "Windows Authentication";
            this.windowsAuthButton.UseVisualStyleBackColor = true;
            // 
            // serverNameInput
            // 
            this.serverNameInput.Location = new System.Drawing.Point(6, 48);
            this.serverNameInput.Name = "serverNameInput";
            this.serverNameInput.Size = new System.Drawing.Size(157, 23);
            this.serverNameInput.TabIndex = 1;
            // 
            // serverNameLabel
            // 
            this.serverNameLabel.AutoSize = true;
            this.serverNameLabel.Location = new System.Drawing.Point(6, 30);
            this.serverNameLabel.Name = "serverNameLabel";
            this.serverNameLabel.Size = new System.Drawing.Size(74, 15);
            this.serverNameLabel.TabIndex = 0;
            this.serverNameLabel.Text = "Server Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cleanDBButton);
            this.groupBox1.Controls.Add(this.exitButton);
            this.groupBox1.Controls.Add(this.goButton);
            this.groupBox1.Controls.Add(this.selectReportButton);
            this.groupBox1.Controls.Add(this.reportDateInput);
            this.groupBox1.Controls.Add(this.reportDateLabel);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 257);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Setup";
            // 
            // cleanDBButton
            // 
            this.cleanDBButton.Location = new System.Drawing.Point(6, 164);
            this.cleanDBButton.Name = "cleanDBButton";
            this.cleanDBButton.Size = new System.Drawing.Size(154, 31);
            this.cleanDBButton.TabIndex = 4;
            this.cleanDBButton.Text = "Clean DB";
            this.toolTip1.SetToolTip(this.cleanDBButton, "Remove all data currently in the local database");
            this.cleanDBButton.UseVisualStyleBackColor = true;
            this.cleanDBButton.Click += new System.EventHandler(this.cleanDBButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(6, 201);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(154, 32);
            this.exitButton.TabIndex = 3;
            this.exitButton.Text = "Exit";
            this.toolTip1.SetToolTip(this.exitButton, "Close the application");
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // goButton
            // 
            this.goButton.Enabled = false;
            this.goButton.Location = new System.Drawing.Point(6, 90);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(154, 31);
            this.goButton.TabIndex = 0;
            this.goButton.Text = "Go";
            this.toolTip1.SetToolTip(this.goButton, "Start report building process");
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 297);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.userConfigGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Line Parser - Report Setup";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.userConfigGroup.ResumeLayout(false);
            this.userConfigGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button selectReportButton;
        private DateTimePicker reportDateInput;
        private Label reportDateLabel;
        private GroupBox userConfigGroup;
        private TextBox serverNameInput;
        private Label serverNameLabel;
        private Label authTypeLabel;
        private RadioButton passwordAuth;
        private RadioButton windowsAuthButton;
        private Button testConnectionButton;
        private GroupBox groupBox1;
        private Button exitButton;
        private Button setupDBButton;
        private Button goButton;
        private Button cleanDBButton;
        private ToolTip toolTip1;
    }
}