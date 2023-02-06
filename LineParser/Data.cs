using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using LineParser;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Drawing;
using System.IO;
using System.Data.Common;

namespace LineParser
{
    static class Data
    {
        public static string? connectionString;

		public static string? errorMessage;

        public static void LoadReportFiles(List<Royalty_Report> reports, string destinationDir)
        {
            //string connectionString = $"Data Source=GCYPLL3;Initial Catalog=Line_Music_Parser;Integrated Security=True";
            SqlConnection conn = new(connectionString);
			
            conn.Open();

            foreach (Royalty_Report report in reports)
            {
                string sql = @"Line_Music_loadStagingTable";
                SqlCommand cmd = new(sql, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add("@path", SqlDbType.NVarChar, -1).Value = report.reportPath;
                cmd.Parameters.Add("@directory", SqlDbType.NVarChar, -1).Value = destinationDir;
                cmd.Parameters.Add("@reportName", SqlDbType.VarChar, 50).Value = report.reportName;
                cmd.Parameters.Add("@reportDate", SqlDbType.VarChar, 10).Value = report.reportDate;
                cmd.Parameters.Add("@unitPrice", SqlDbType.Decimal, 10).Value = report.unitPrice;

                cmd.ExecuteNonQuery();
            }

            conn.Close();
        }

        public static void BuildConnectionString(string serverName, bool windowsAuth)
        {
            connectionString = $"Data Source ={serverName}; Initial Catalog=Line_Music_Parser;";
            if (windowsAuth)
            {
                connectionString += "Integrated Security=True";
            }
            else
            {
                MessageBox.Show("Sorry, I can't handle passwords yet.");
            }
        }

		public static void BuildReport(string fileName)
		{
			SqlConnection conn = new(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Report_Formatted";
			SqlCommand cmd = new(sql, conn);

			SqlDataReader reader = cmd.ExecuteReader();

            if (reader == null)
            {
                MessageBox.Show("Could not retrieve report data from the database.");
				conn.Close();
            }
            else
            {
                using (StreamWriter file = new StreamWriter(fileName, false))
                {
                    while (reader.Read())
                    {
                        file.WriteLine(
							reader[0] 
							+ "\t" + reader[1] 
							+ "\t" + reader[2] 
							+ "\t" + reader[3] 
							+ "\t" + reader[4]
                            + "\t" + reader[5]
                            + "\t" + reader[6]
                            + "\t" + reader[7]
                            + "\t" + reader[8]
                            + "\t" + reader[9]);
                    }
                }

                reader.Close();
				conn.Close();
            }
        }

		public static void FillFileTotals(DataGridView grid)
		{
			SqlConnection conn = new(connectionString);
            string sql = @"SELECT Report_Name, 
							CONVERT(DECIMAL(30,4), Report_Total),
							Report_Line_Count, 
							Original_Line_Count
							FROM [File_Totals]";
			conn.Open();
			SqlDataAdapter dataAdapter = new(sql, conn);
			DataSet dataSet = new();
			dataAdapter.Fill(dataSet);
			grid.DataSource = dataSet.Tables[0];

			grid.Columns[0].HeaderText = "Report Name";
			grid.Columns[0].Width = 150;
            grid.Columns[1].HeaderText = "Report Total";
            grid.Columns[1].Width = 100;
            grid.Columns[2].HeaderText = "Report Line Count";
            grid.Columns[2].Width = 100;
            grid.Columns[3].HeaderText = "Original Line Count";
			grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

			conn.Close();
        }

		public static void Cleanup()
		{
            SqlConnection conn = new(connectionString);

            conn.Open();
            string sql = @"Line_Music_Cleanup";
            SqlCommand cmd = new(sql, conn)
            {
                CommandType = CommandType.StoredProcedure
            };
			cmd.ExecuteNonQuery();
			conn.Close();
        }

        public static bool BuildSQLDB(string serverName)
        {
            string dbBuildCommand = "CREATE DATABASE Line_Music_Parser";
            string tableBuildCommand = @"CREATE TABLE [dbo].[Report_Formatted] (
		                                    [Quantity] INT,
		                                    [UnitPrice] DECIMAL(20,10),
		                                    [Territory] VARCHAR(10),
		                                    [Artist] NVARCHAR(MAX) NULL,
		                                    [Album] NVARCHAR(MAX) NULL,
		                                    [Track] NVARCHAR(MAX) NULL,
		                                    [ISRC] VARCHAR(20) NULL,
		                                    [UPC] VARCHAR(20),
		                                    [ForeignID] VARCHAR(100) NULL,
		                                    [Date] VARCHAR(10)
	                                    );

	                                    CREATE TABLE [dbo].[File_Totals] (
		                                    [Report_Name] VARCHAR(100) NULL,
		                                    [Report_Total] DECIMAL(30,20) NULL,
		                                    [Report_Line_Count] INT NULL,
		                                    [Original_Line_Count] INT NULL
	                                    );

	                                    CREATE TABLE [dbo].[Line_Music_Report_Staging] (
		                                    [territory] VARCHAR(5) NULL,
		                                    [trans_date] VARCHAR(50) NULL,
		                                    [upc] VARCHAR(20) NULL,
		                                    [isrc] VARCHAR(20) NULL,
		                                    [label_name] NVARCHAR(MAX) NULL,
		                                    [artist_name] NVARCHAR(MAX) NULL,
		                                    [release_name] NVARCHAR(MAX) NULL,
		                                    [track_name] NVARCHAR(MAX) NULL,
		                                    [qty] INT NULL
	                                    );

	                                    CREATE TABLE [dbo].[Line_Music_Report_Transform] (
		                                    [Report_Name] VARCHAR(50) NULL,
		                                    [Quantity] INT NULL,
		                                    [UnitPrice] DECIMAL(20,10) NULL,
		                                    [Territory] VARCHAR(10) NULL,
		                                    [Artist] NVARCHAR(MAX) NULL,
		                                    [Album] NVARCHAR(MAX) NULL,
		                                    [Track] NVARCHAR(MAX) NULL,
		                                    [ISRC] VARCHAR(20) NULL,
		                                    [UPC] VARCHAR(20) NULL,
		                                    [ForeignID] VARCHAR(100) NULL,
		                                    [Date] VARCHAR(10) NULL
	                                    );";

			string procs_loadReportTable = @"CREATE OR ALTER PROC [Line_Music_loadReportTable]
										(
											@reportDate VARCHAR(10)
										)
										AS
										BEGIN TRY
											INSERT INTO [dbo].[Report_Formatted]
												([Quantity], [UnitPrice], [Territory], [Artist], [Album], [Track], [ISRC], [UPC], [ForeignID], [Date])
											SELECT		[Quantity],
														[UnitPrice],
														[Territory],
														QUOTENAME([Artist],'""'),
														QUOTENAME([Album],'""'),
														QUOTENAME([Track],'""'),
														[ISRC],
														[UPC],
														NULL,
														@reportDate
											FROM		[dbo].[Line_Music_Report_Transform]
										END TRY
										BEGIN CATCH
											PRINT 'Report table loading failed';
											PRINT 'Error ' + CONVERT(varchar, ERROR_NUMBER(), 1) + ': ' + ERROR_MESSAGE();
										END CATCH";
			string procs_loadFileTotal = @"CREATE PROC [Line_Music_loadFileTotal]
										(
											@reportName VARCHAR(50)
										)
										AS
											DECLARE @lineCount INT
											SET @lineCount = (SELECT COUNT([Quantity]) FROM [dbo].[Line_Music_Report_Transform]);

											INSERT INTO [dbo].[File_Totals]
											([Report_Name], [Report_Total], [Report_Line_Count], [Original_Line_Count])
											SELECT		@reportName,
														SUM([UnitPrice] * [Quantity]),
														COUNT([Quantity]),
														@lineCount
											FROM		[Line_Music_Report_Transform]";
			string procs_loadTransformTable = @"CREATE PROC[Line_Music_loadTransformTable]

											@reportName VARCHAR(50),
											@reportDate VARCHAR(10),
											@unitPrice DECIMAL(20,10)

										AS
										TRUNCATE TABLE[dbo].[Line_Music_Report_Transform]
										BEGIN TRY
											INSERT INTO[dbo].[Line_Music_Report_Transform]
										([Report_Name],[Quantity], [UnitPrice], [Territory], [Artist], [Album], [Track], [ISRC], [UPC], [ForeignID], [Date])
												SELECT @reportName,
															[qty],
															@unitPrice,
															[territory],
															REPLACE([artist_name], '""', ''''), 
															REPLACE([release_name], '""', ''''),
															REPLACE([track_name], '""', ''''),
															[isrc],
															[upc],
															NULL,
															@reportDate
												FROM[Line_Music_Report_Staging]

												EXEC[Line_Music_loadFileTotal] @reportName;
										EXEC[Line_Music_loadReportTable] @reportDate;
										END TRY
										BEGIN CATCH
											PRINT 'Transform table loading failed';
										PRINT 'Error ' + CONVERT(varchar, ERROR_NUMBER(), 1) + ': ' + ERROR_MESSAGE();
										END CATCH";
			string procs_loadStagingTable = @"CREATE PROC [Line_Music_loadStagingTable]

											@path NVARCHAR(MAX),
											@directory NVARCHAR(MAX),
											@reportName VARCHAR(50),
											@reportDate VARCHAR(10),
											@unitPrice DECIMAL(20,10)

										AS
										TRUNCATE TABLE [Line_Music_Report_Staging]
										BEGIN TRY
											--SET @path = 'C:\Users\whelgeson\OneDrive - CD Baby\Documents\RATT Docs\Reports\Line Music\202211\basic.csv';
											--SET @directory = 'C:\Users\whelgeson\OneDrive - CD Baby\Documents\RATT Docs\Reports\Line Music\202211'
											DECLARE @sql NVARCHAR(MAX) =
											'BULK INSERT [dbo].[Line_Music_Report_Staging]
												FROM ''' + @path + '''' +
												+ ' WITH (
													KEEPNULLS,
													FORMAT = ''CSV'',
													FIRSTROW = 2,
													CODEPAGE = ''65001'', 
													ERRORFILE = ''' + @directory 
													+ '\errors.txt''
												);'
											EXEC(@sql);
											EXEC [Line_Music_loadTransformTable] @reportName, @reportDate, @unitPrice;
										END TRY
										BEGIN CATCH
											PRINT 'Bulk Insert Failed.';
											PRINT 'Error ' + CONVERT(varchar, ERROR_NUMBER(), 1) + ': ' + ERROR_MESSAGE();
										END CATCH";
			string procs_Cleanup = @"CREATE PROC [Line_Music_Cleanup]
									AS
									TRUNCATE TABLE[Line_Music_Report_Staging];
									TRUNCATE TABLE[Line_Music_Report_Transform];
									TRUNCATE TABLE[Report_Formatted];
									TRUNCATE TABLE[File_Totals];";

            string[] procs = 
			{ 
				procs_loadReportTable, 
				procs_loadFileTotal, 
				procs_loadTransformTable, 
				procs_loadStagingTable, 
				procs_Cleanup 
			};

            SqlConnection conn = new($"Data Source={serverName};Integrated Security=True;database=master");
            conn.Open();

            SqlCommand buildDB = new SqlCommand(dbBuildCommand, conn);
            SqlCommand buildTables = new SqlCommand(tableBuildCommand, conn);

            try
            {
                buildDB.ExecuteNonQuery();
                conn.ChangeDatabase("Line_Music_Parser");
                buildTables.ExecuteNonQuery();

                foreach (string proc in procs)
                {
                    SqlCommand addProc = new SqlCommand(proc, conn);
                    addProc.ExecuteNonQuery();
                }

                conn.Close();
                return true;
            }
            catch (Exception e)
            {
				errorMessage= e.Message;
                conn.Close();
                return false;
            }
        }

		public static bool TestConnection()
		{
			SqlConnection conn = new(connectionString);

			try
			{
				conn.Open();
				conn.Close();
				return true;
			}
			catch (SqlException)
			{
				conn.Close();
				return false;
			}
		}
    }
}