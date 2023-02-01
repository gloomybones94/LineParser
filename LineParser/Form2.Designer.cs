namespace LineParser
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.subReportView = new System.Windows.Forms.DataGridView();
            this.subscriptionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buildReportButton = new System.Windows.Forms.Button();
            this.saveFileTotalsButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.fileTotalView = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.subReportView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileTotalView)).BeginInit();
            this.SuspendLayout();
            // 
            // subReportView
            // 
            this.subReportView.AllowUserToAddRows = false;
            this.subReportView.AllowUserToDeleteRows = false;
            this.subReportView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subReportView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.subscriptionType,
            this.unitPrice,
            this.filePath});
            this.subReportView.Location = new System.Drawing.Point(12, 12);
            this.subReportView.Name = "subReportView";
            this.subReportView.RowTemplate.Height = 25;
            this.subReportView.Size = new System.Drawing.Size(967, 210);
            this.subReportView.TabIndex = 0;
            this.subReportView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.subReportView_CellValueChanged);
            // 
            // subscriptionType
            // 
            this.subscriptionType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.subscriptionType.HeaderText = "Subscription Type";
            this.subscriptionType.Name = "subscriptionType";
            this.subscriptionType.ReadOnly = true;
            this.subscriptionType.Width = 250;
            // 
            // unitPrice
            // 
            this.unitPrice.HeaderText = "Unit Price";
            this.unitPrice.Name = "unitPrice";
            this.unitPrice.Width = 150;
            // 
            // filePath
            // 
            this.filePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.filePath.HeaderText = "File Path";
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            // 
            // buildReportButton
            // 
            this.buildReportButton.Enabled = false;
            this.buildReportButton.Location = new System.Drawing.Point(12, 228);
            this.buildReportButton.Name = "buildReportButton";
            this.buildReportButton.Size = new System.Drawing.Size(203, 42);
            this.buildReportButton.TabIndex = 1;
            this.buildReportButton.Text = "Build Report";
            this.buildReportButton.UseVisualStyleBackColor = true;
            this.buildReportButton.Click += new System.EventHandler(this.buildReportButton_Click);
            // 
            // saveFileTotalsButton
            // 
            this.saveFileTotalsButton.Enabled = false;
            this.saveFileTotalsButton.Location = new System.Drawing.Point(12, 274);
            this.saveFileTotalsButton.Name = "saveFileTotalsButton";
            this.saveFileTotalsButton.Size = new System.Drawing.Size(203, 42);
            this.saveFileTotalsButton.TabIndex = 1;
            this.saveFileTotalsButton.Text = "Save File Totals";
            this.toolTip1.SetToolTip(this.saveFileTotalsButton, "Not yet implemented");
            this.saveFileTotalsButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 320);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(203, 42);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // fileTotalView
            // 
            this.fileTotalView.AllowUserToAddRows = false;
            this.fileTotalView.AllowUserToDeleteRows = false;
            this.fileTotalView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileTotalView.Location = new System.Drawing.Point(234, 228);
            this.fileTotalView.Name = "fileTotalView";
            this.fileTotalView.ReadOnly = true;
            this.fileTotalView.RowTemplate.Height = 25;
            this.fileTotalView.Size = new System.Drawing.Size(745, 245);
            this.fileTotalView.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 552);
            this.Controls.Add(this.fileTotalView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveFileTotalsButton);
            this.Controls.Add(this.buildReportButton);
            this.Controls.Add(this.subReportView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Line Music Parser - Build Reports";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.subReportView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileTotalView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView subReportView;
        private Button buildReportButton;
        private Button saveFileTotalsButton;
        private Button cancelButton;
        private DataGridView fileTotalView;
        private DataGridViewTextBoxColumn subscriptionType;
        private DataGridViewTextBoxColumn unitPrice;
        private DataGridViewTextBoxColumn filePath;
        private ToolTip toolTip1;
    }
}