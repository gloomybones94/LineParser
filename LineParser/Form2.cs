using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineParser
{
    public partial class Form2 : Form
    {
        public string directory;
        public string dateString;
        public string fileDate;

        public Form2(string directory, string dateString, string fileDate)
        {
            InitializeComponent();
            this.directory = directory;
            this.dateString = dateString;
            this.fileDate = fileDate;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadReportUI(directory, dateString, subReportView);

        }

        private static void LoadReportUI(string directory, string reportDate, DataGridView grid)
        {
            // Add a row to a datagridview for each sub-report file
            string[] reportPaths = Directory.GetFiles(directory);

            List<Royalty_Report> reportFiles = new();

            foreach (string reportPath in reportPaths)
            {
                string reportName = Path.GetFileNameWithoutExtension(reportPath);
                Royalty_Report report = new(reportPath, reportDate, reportName, null);
                reportFiles.Add(report);
                grid.Rows.Add(reportName,"",reportPath);
            }
        }

        private void subReportView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRowCollection rows = subReportView.Rows;

            if (rows.Count > 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    if (row.Cells[1].Value == null)
                    {
                        return;
                    }
                    else
                    {
                        string? rowValue = row.Cells[1].Value.ToString();
                    }
                }

                buildReportButton.Enabled = true;
            }
        }

        private static bool ValidateUnitPrices(DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (!double.TryParse(row.Cells[1].Value.ToString(), out double n))
                {
                    MessageBox.Show($"{row.Cells[0].Value} has an invalid unit price.", "Invalid Unit Price");
                    return false;
                }
            }

            return true;
        }

        private void buildReportButton_Click(object sender, EventArgs e)
        {
            bool validUnitPrices = ValidateUnitPrices(subReportView);

            if (validUnitPrices)
            {
                Cursor.Current = Cursors.WaitCursor;
                bool success = LoadReports(directory, dateString);
                Cursor.Current = Cursors.Default;

                if (!success)
                {
                    MessageBox.Show("Something when wrong during the report build. Please check your files and try again", "Error");
                }
            }

        }

        private bool LoadReports(string reportDirectory, string reportDate)
        {

            DataGridViewRowCollection rows = subReportView.Rows;

            if (rows.Count > 0)
            {
                List<Royalty_Report> reportFiles = new();

                foreach (DataGridViewRow row in rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[2].Value != null)
                    {
                        string reportName = row.Cells[0].Value.ToString();
                        decimal unitPrice = Convert.ToDecimal(row.Cells[1].Value);
                        string reportPath = row.Cells[2].Value.ToString();
                        Royalty_Report report = new(reportPath, reportDate, reportName, unitPrice);
                        reportFiles.Add(report);
                    }
                    else
                    {
                        MessageBox.Show("Missing report information, please double-check that all fields are filled in.", "Error");
                        return false ;
                    }
                }

                try
                {
                    Data.LoadReportFiles(reportFiles, reportDirectory);
                    string fileName = $@"{directory}\line-music-{fileDate}.txt";
                    Data.BuildReport(fileName);
                    Data.FillFileTotals(fileTotalView);
                    Data.Cleanup();
                    return true;
                }
                catch
                {
                    return false;
                }


            }
            else
            {
                MessageBox.Show("There are no files to load...");
                return false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            string msg = "Would you like to delete the split report files?";
            DialogResult res = MessageBox.Show(msg, "Remove files?", MessageBoxButtons.YesNoCancel);

            if (res == DialogResult.Yes)
            {
                DirectoryInfo dir = new(directory);

                if (dir != null)
                {
                    bool couldNotDelete = false;
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch
                        {
                            string fileName = file.Name;
                            MessageBox.Show($"Unable to delete {fileName}. Do you have it open?", "Cannot Delete File");
                            couldNotDelete = true;
                        }
                        
                    }

                    if (!couldNotDelete)
                    {
                        dir.Delete();
                    }
                    else
                    {
                        MessageBox.Show($"Could not delete directory {dir.Name} because it is not empty. Please delete it manually.");
                    }

                    Close();
                }
            }
            else if (res == DialogResult.No)
            {
                Close();
            }
        }
    }
}
