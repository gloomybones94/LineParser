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

            if (windowsAuth == true)
            {
                windowsAuthButton.Checked = true;
            }
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            reportPath = GetReportPath();

            if (reportPath != null)
            {
                goButton.Enabled = true;
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            DateTime reportDate = reportDateInput.Value;

            string dateString = reportDate.ToShortDateString();
            string fileDate = reportDate.Year.ToString() + reportDate.Month.ToString().PadLeft(2, '0');
            string destinationDir = $@"{Path.GetDirectoryName(reportPath)}\{fileDate}\";

            
            if (reportPath != null)
            {
                bool splitSucceeded = FileSplitter.SplitFile(reportPath, destinationDir);

                if (splitSucceeded)
                {
                    Form2 form2 = new(destinationDir, dateString, fileDate);
                    form2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Unable to split file.","Error");
                }

            }
            else
            {
                MessageBox.Show("No report selected.","Error");
            }

        }

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

        private void testConnectionButton_Click(object sender, EventArgs e)
        {
            bool connection = Data.TestConnection();
            string message;

            if (connection)
            {
                message = "Connected to database successfully!";
            }
            else
            {
                message = "Could not connect to the database. Please double check the server name and authentication type.";
            }

            MessageBox.Show(message);
        }

        private void setupDBButton_Click(object sender, EventArgs e)
        {
            Data.BuildConnectionString(Properties.Settings.Default.serverName, Properties.Settings.Default.windowsAuth);
            bool dbBuildSuccess = Data.BuildSQLDB(serverNameInput.Text);
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
                MessageBox.Show("Unable to build database, seek help :(","Database Build Unsuccessful");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cleanDBButton_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to clean up the database?\n\nThis will remove all report data currently in the database. You should only need to use this button if the application crashed last time you used it.";
            DialogResult res = MessageBox.Show(msg, "Remove all report data from local database?",MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes) 
            {
                Data.Cleanup();
                MessageBox.Show("Database truncated successfully.","Cleanup Successful");
            }
        }
    }
}