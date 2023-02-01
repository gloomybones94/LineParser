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
                    string? rowValue = row.Cells[1].Value.ToString();
                    if (string.IsNullOrEmpty(rowValue))
                    {
                        return;
                    }
                }

                buildReportButton.Enabled = true;
            }
        }

        private void buildReportButton_Click(object sender, EventArgs e)
        {
            bool validUnitPrices = false;

            foreach (DataGridViewRow row in subReportView.Rows)
            {
                if (!double.TryParse(row.Cells[1].Value.ToString(), out double n))
                {
                    MessageBox.Show($"{row.Cells[0].Value} has an invalid unit price.","Invalid Unit Price");
                    validUnitPrices = false;
                    break;
                }
                else
                {
                    validUnitPrices = true;
                }
            }

            if (validUnitPrices)
            {
                LoadReports(directory, dateString);
                string fileName = $@"{directory}\line-music-{fileDate}.txt";
                Data.BuildReport(fileName);
                Data.FillFileTotals(fileTotalView);
                Data.Cleanup();
            }

        }

        private void LoadReports(string reportDirectory, string reportDate)
        {

            DataGridViewRowCollection rows = subReportView.Rows;

            if (rows.Count > 0)
            {
                List<Royalty_Report> reportFiles = new();

                foreach (DataGridViewRow row in rows)
                {
                    string? reportName = row.Cells[0].Value.ToString();
                    decimal unitPrice = Convert.ToDecimal(row.Cells[1].Value);
                    string? reportPath = row.Cells[2].Value.ToString();
                    Royalty_Report report = new(reportPath, reportDate, reportName, unitPrice);
                    reportFiles.Add(report);
                }

                Data.LoadReportFiles(reportFiles, reportDirectory);
            }
            else
            {
                MessageBox.Show("There are no files to load...");
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
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        file.Delete();
                    }

                    dir.Delete();
                    this.Close();
                }
            }
            else if (res == DialogResult.No)
            {
                this.Close();
            }
        }
    }
}
