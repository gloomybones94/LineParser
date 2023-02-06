# LineParser

This tool splits the XLSX report files provided by Line Music into CSVs delimited by service type/subscription tier.
It then imports them into a local SQL Server database, transforms the data as necessary, and produces a tab-delimited
TXT file for importing into DAX.

# Installation

*Before installing the parser, make sure you have SQL Server installed. You'll want to have your instance set up with
Windows Authentication, as the parser does not support password authentication at this time.*

LineParser is published as a ClickOnce application through this GitHub repo.

Download the **setup.exe** file from the **Published** directory of the repo and run it. You'll get security warnings because
I don't have a publisher certification, but go ahead and tell it to run anyway. You'll need to click "More Info" on the
first "Windows Has protected your computer from...." pop-up to get the 'Run Anyway' button.

After the installer runs, you should have a desktop shortcut. Click it to run and you should get the initial form.

# First Time Setup

In the initial form window, you'll see a configuration form on the right-hand side. Enter the name of your local SQL Server
instance and click the 'Build DB' button. If your server name is entered correctly and you've set it up with Window Authentication,
you should get a message box telling you that the build was successful. Go ahead and click the 'Test Connection' button afterward
to make sure that you're all hooked up. *You'll only need to do this once.*

# Using the Parser

Select the date for the reporting period, then use the 'Select Report File' button to choose your raw report file.
*The program's output files will be in the directory that this file is in, so make sure it's easy to access!*

Once a file's been selected, the 'Go' button will be enabled. Click this, and the parser will take a few seconds to
split the raw report file into a CSV for each worksheet. Then, the second form will open. Here, enter the unit prices
for each of the service/subscription tiers. 

After all unit prices have been entered, click 'Build Report' to create the final import file. This will also populate the
lower data grid with information about each of the sub-reports. Use this to audit the output against the expected result.
**The output file will be in a folder named as yyyymm for the report date (ex. 202211 for November 2022)**
