using OfficeOpenXml;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace LineParser
{
    
    public partial class Form1 : Form
    {
        public string? reportPath;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            bool firstRun = Properties.Settings.Default.firstRun;
            bool windowsAuth = Properties.Settings.Default.windowsAuth;
            string? serverName = Properties.Settings.Default.serverName;

            // if the user has set up their database connection, proceed with setup
            // otherwise, enable the first-time setup button
            if (firstRun == false) 
            {
                serverNameInput.Text = serverName;
                Data.BuildConnectionString(serverName, windowsAuth);
                cleanDBButton.Enabled = true;
            }
            else
            {
                setupDBButton.Enabled= true;
            }

            // for now, this will always be true as I haven't written anything to handle a pw authenticated server
            if (windowsAuth == true)
            {
                windowsAuthButton.Checked = true;
            }
        }


        // event handler for the Build DB button
        private void setupDBButton_Click(object sender, EventArgs e)
        {
            Data.BuildConnectionString(Properties.Settings.Default.serverName, Properties.Settings.Default.windowsAuth);
            bool dbBuildSuccess = Data.BuildSQLDB(serverNameInput.Text);

            // If the database was set up successfully, update the app settings
            if (dbBuildSuccess)
            {
                MessageBox.Show("Parser Database built successfully!");
                Properties.Settings.Default.firstRun = false;
                Properties.Settings.Default.serverName = serverNameInput.Text;
                Properties.Settings.Default.Save();
                cleanDBButton.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Unable to build database, seek help :(\n\n{Data.errorMessage}", "Database Build Unsuccessful");
            }
        }


        // event handler for the Clean DB button
        private void cleanDBButton_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to clean up the database?\n\nThis will remove all report data currently in the database. You should only need to use this button if the application crashed last time you used it.";
            DialogResult res = MessageBox.Show(msg, "Remove all report data from local database?", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {
                // truncate all the database tables
                Data.Cleanup();
                MessageBox.Show("Database truncated successfully.", "Cleanup Successful");
            }
        }


        // event handler for the Select Report File Button
        private void selectReportButton_Click(object sender, EventArgs e)
        {
            reportPath = GetReportPath();

            if (reportPath != null)
            {
                goButton.Enabled = true;
            }
        }


        // allow the user to select one excel file
        private static string? GetReportPath()
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Excel Worksheets|*.xlsx",
                FilterIndex = 1,
                Multiselect = false

            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string reportPath = openFileDialog.FileName;
                return reportPath;
            }
            else
            {
                return null;
            }

        }


        // event handler for the Go button
        private void goButton_Click(object sender, EventArgs e)
        {
            // The report date is necessary because of formatting changes over time.
            // It is used to determine what transformations need to be made to the
            // worksheet prior to db insertion
            DateTime reportDate = reportDateInput.Value;

            string dateString = reportDate.ToShortDateString(); // a date string for database insertion
            string fileDate = reportDate.Year.ToString() + reportDate.Month.ToString().PadLeft(2, '0'); // the yyyymm date format for use in file names
            string destinationDir = $@"{Path.GetDirectoryName(reportPath)}\{fileDate}\"; // the output directory for all generated files

            
            if (reportPath != null)
            {
                // Split the excel file into its constituent sheets
                bool splitSucceeded = FileSplitter.SplitFile(reportPath, destinationDir, reportDate);

                // if successful, open the second form
                if (splitSucceeded)
                {
                    Form2 form2 = new(destinationDir, dateString, fileDate);
                    form2.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Unable to split file.\n\n{FileSplitter.errorMessage}","File Splitter Error");
                }

            }
            else
            {
                MessageBox.Show("No report selected.","Error");
            }

        }


        // event handler for the test connection button
        private void testConnectionButton_Click(object sender, EventArgs e)
        {
            bool connection = Data.TestConnection();

            if (connection)
            {
                MessageBox.Show("Connected to database successfully!");
            }
            else
            {
                MessageBox.Show("Could not connect to the database. Please double check the server name and authentication type.");
            }
        }


        // close the application
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}