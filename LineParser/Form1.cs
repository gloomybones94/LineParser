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
            bool authType = Properties.Settings.Default.windowsAuth;

            if (firstRun == false) 
            {
                serverNameInput.Text = Properties.Settings.Default.serverName;
                Data.BuildConnectionString(Properties.Settings.Default.serverName, Properties.Settings.Default.windowsAuth);
                cleanDBButton.Enabled = true;
            }
            else
            {
                setupDBButton.Enabled= true;
            }

            if (authType == true)
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

            SplitOriginalFile(reportPath, destinationDir);
            Form2 form2 = new(destinationDir, dateString, fileDate);
            form2.ShowDialog();
        }

        private static void SplitOriginalFile(string? reportPath, string destinationDir)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create directory
            Directory.CreateDirectory(destinationDir);

            // Get raw report file
            ExcelPackage excelFile = new(new FileInfo(reportPath));

            // Break into individual sheets and save
            foreach (var sheet in excelFile.Workbook.Worksheets)
            {
                string newFileName = sheet.Name.Replace(" ", "-").ToLower() + ".xlsx";

                ExcelPackage newFile = new(new FileInfo(destinationDir + newFileName));
                newFile.Workbook.Worksheets.Add(sheet.Name);
                var newSheet = newFile.Workbook.Worksheets[0];

                int lastCell = sheet.Dimension.End.Row - 1;
                var sourceRange = sheet.Cells["A1:I" + lastCell.ToString()];
                sourceRange.Copy(newSheet.Cells["A1"]);

                var format = new ExcelOutputTextFormat
                {
                    TextQualifier = '"',
                    Encoding = System.Text.Encoding.UTF8,
                    EOL = "\r\n"
                };

                newSheet.Column(3).Style.Numberformat.Format = "@";
                newSheet.DeleteColumn(10, 16374);

                var file = new FileInfo(destinationDir + sheet.Name.Replace(" ", "-").ToLower() + ".csv");

                newSheet.Cells["A1:I" + lastCell.ToString()].SaveToText(file, format);
            }
        }

        private string? GetReportPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "",
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
            string message = "";
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
                MessageBox.Show("Unable to build database, seek help :(");
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