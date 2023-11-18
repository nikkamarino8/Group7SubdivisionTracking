namespace Group7SubdivisionTracking
{
    partial class Analytics
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Analytics));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnAnalytics = new System.Windows.Forms.Button();
            this.btnVisitors = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pctLogOut = new System.Windows.Forms.PictureBox();
            this.pctExit = new System.Windows.Forms.PictureBox();
            this.totalVisitor = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.commonPurpose = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblFrequentVisitor = new System.Windows.Forms.Label();
            this.lblLatestVisitor = new System.Windows.Forms.Label();
            this.lblTotalVisitor = new System.Windows.Forms.Label();
            this.lblFVisited = new System.Windows.Forms.Label();
            this.TimeofVisit = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalVisitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commonPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeofVisit)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnUsers);
            this.panel1.Controls.Add(this.btnAnalytics);
            this.panel1.Controls.Add(this.btnVisitors);
            this.panel1.Controls.Add(this.btnDashboard);
            this.panel1.Location = new System.Drawing.Point(1115, 304);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 466);
            this.panel1.TabIndex = 70;
            // 
            // btnUsers
            // 
            this.btnUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(74)))));
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.ForeColor = System.Drawing.Color.White;
            this.btnUsers.Location = new System.Drawing.Point(180, 393);
            this.btnUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(268, 73);
            this.btnUsers.TabIndex = 3;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnAnalytics
            // 
            this.btnAnalytics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnalytics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(74)))));
            this.btnAnalytics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalytics.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalytics.ForeColor = System.Drawing.Color.White;
            this.btnAnalytics.Location = new System.Drawing.Point(123, 283);
            this.btnAnalytics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAnalytics.Name = "btnAnalytics";
            this.btnAnalytics.Size = new System.Drawing.Size(324, 73);
            this.btnAnalytics.TabIndex = 2;
            this.btnAnalytics.Text = "Analytics";
            this.btnAnalytics.UseVisualStyleBackColor = false;
            this.btnAnalytics.Click += new System.EventHandler(this.btnAnalytics_Click);
            // 
            // btnVisitors
            // 
            this.btnVisitors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVisitors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(74)))));
            this.btnVisitors.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisitors.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVisitors.ForeColor = System.Drawing.Color.White;
            this.btnVisitors.Location = new System.Drawing.Point(63, 167);
            this.btnVisitors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVisitors.Name = "btnVisitors";
            this.btnVisitors.Size = new System.Drawing.Size(383, 73);
            this.btnVisitors.TabIndex = 1;
            this.btnVisitors.Text = "Visitors";
            this.btnVisitors.UseVisualStyleBackColor = false;
            this.btnVisitors.Click += new System.EventHandler(this.btnVisitors_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(34)))), ((int)(((byte)(74)))));
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Mongolian Baiti", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(3, 55);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(443, 73);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Group7SubdivisionTracking.Properties.Resources.minimize;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(1434, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(43, 37);
            this.pictureBox1.TabIndex = 78;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pctLogOut
            // 
            this.pctLogOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pctLogOut.BackColor = System.Drawing.Color.Transparent;
            this.pctLogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pctLogOut.Image = global::Group7SubdivisionTracking.Properties.Resources.logout;
            this.pctLogOut.Location = new System.Drawing.Point(1467, 1010);
            this.pctLogOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pctLogOut.Name = "pctLogOut";
            this.pctLogOut.Size = new System.Drawing.Size(59, 58);
            this.pctLogOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctLogOut.TabIndex = 72;
            this.pctLogOut.TabStop = false;
            this.pctLogOut.Click += new System.EventHandler(this.pctLogOut_Click);
            // 
            // pctExit
            // 
            this.pctExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctExit.BackColor = System.Drawing.Color.Transparent;
            this.pctExit.BackgroundImage = global::Group7SubdivisionTracking.Properties.Resources.btnClose;
            this.pctExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pctExit.Location = new System.Drawing.Point(1483, 11);
            this.pctExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pctExit.Name = "pctExit";
            this.pctExit.Size = new System.Drawing.Size(43, 37);
            this.pctExit.TabIndex = 71;
            this.pctExit.TabStop = false;
            this.pctExit.Click += new System.EventHandler(this.pctExit_Click);
            // 
            // totalVisitor
            // 
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.totalVisitor.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.totalVisitor.Legends.Add(legend1);
            this.totalVisitor.Location = new System.Drawing.Point(51, 44);
            this.totalVisitor.Name = "totalVisitor";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Total Visitors";
            this.totalVisitor.Series.Add(series1);
            this.totalVisitor.Size = new System.Drawing.Size(580, 379);
            this.totalVisitor.TabIndex = 79;
            this.totalVisitor.Text = "chart1";
            this.totalVisitor.Click += new System.EventHandler(this.totalVisitor_Click);
            // 
            // commonPurpose
            // 
            chartArea2.Name = "ChartArea1";
            this.commonPurpose.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.commonPurpose.Legends.Add(legend2);
            this.commonPurpose.Location = new System.Drawing.Point(657, 44);
            this.commonPurpose.Name = "commonPurpose";
            this.commonPurpose.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Font = new System.Drawing.Font("Constantia", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.LabelBackColor = System.Drawing.Color.Transparent;
            series2.LabelForeColor = System.Drawing.Color.Transparent;
            series2.Legend = "Legend1";
            series2.Name = "commonPurpose";
            series2.SmartLabelStyle.Enabled = false;
            this.commonPurpose.Series.Add(series2);
            this.commonPurpose.Size = new System.Drawing.Size(580, 379);
            this.commonPurpose.TabIndex = 80;
            this.commonPurpose.Text = "chart1";
            this.commonPurpose.Click += new System.EventHandler(this.commonPurpose_Click);
            // 
            // lblFrequentVisitor
            // 
            this.lblFrequentVisitor.AutoSize = true;
            this.lblFrequentVisitor.BackColor = System.Drawing.Color.Transparent;
            this.lblFrequentVisitor.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrequentVisitor.ForeColor = System.Drawing.Color.White;
            this.lblFrequentVisitor.Location = new System.Drawing.Point(45, 985);
            this.lblFrequentVisitor.Name = "lblFrequentVisitor";
            this.lblFrequentVisitor.Size = new System.Drawing.Size(287, 36);
            this.lblFrequentVisitor.TabIndex = 82;
            this.lblFrequentVisitor.Text = "Frequent Visitor:";
            this.lblFrequentVisitor.Click += new System.EventHandler(this.lblFrequentVisitor_Click);
            // 
            // lblLatestVisitor
            // 
            this.lblLatestVisitor.AutoSize = true;
            this.lblLatestVisitor.BackColor = System.Drawing.Color.Transparent;
            this.lblLatestVisitor.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatestVisitor.ForeColor = System.Drawing.Color.White;
            this.lblLatestVisitor.Location = new System.Drawing.Point(45, 828);
            this.lblLatestVisitor.Name = "lblLatestVisitor";
            this.lblLatestVisitor.Size = new System.Drawing.Size(255, 36);
            this.lblLatestVisitor.TabIndex = 83;
            this.lblLatestVisitor.Text = "Latest Visitor:";
            this.lblLatestVisitor.Click += new System.EventHandler(this.lblLatestVisitor_Click);
            // 
            // lblTotalVisitor
            // 
            this.lblTotalVisitor.AutoSize = true;
            this.lblTotalVisitor.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalVisitor.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVisitor.ForeColor = System.Drawing.Color.White;
            this.lblTotalVisitor.Location = new System.Drawing.Point(45, 909);
            this.lblTotalVisitor.Name = "lblTotalVisitor";
            this.lblTotalVisitor.Size = new System.Drawing.Size(239, 36);
            this.lblTotalVisitor.TabIndex = 84;
            this.lblTotalVisitor.Text = "Total Visitor:";
            this.lblTotalVisitor.Click += new System.EventHandler(this.lblTotalVisitor_Click);
            // 
            // lblFVisited
            // 
            this.lblFVisited.AutoSize = true;
            this.lblFVisited.BackColor = System.Drawing.Color.Transparent;
            this.lblFVisited.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFVisited.ForeColor = System.Drawing.Color.White;
            this.lblFVisited.Location = new System.Drawing.Point(45, 753);
            this.lblFVisited.Name = "lblFVisited";
            this.lblFVisited.Size = new System.Drawing.Size(319, 36);
            this.lblFVisited.TabIndex = 85;
            this.lblFVisited.Text = "Frequently Visited:";
            this.lblFVisited.Click += new System.EventHandler(this.lblFVisited_Click);
            // 
            // TimeofVisit
            // 
            chartArea3.Name = "ChartArea1";
            this.TimeofVisit.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.TimeofVisit.Legends.Add(legend3);
            this.TimeofVisit.Location = new System.Drawing.Point(51, 444);
            this.TimeofVisit.Name = "TimeofVisit";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series3.Legend = "Legend1";
            series3.Name = "Common Time Visitors Visit";
            series3.YValuesPerPoint = 2;
            this.TimeofVisit.Series.Add(series3);
            this.TimeofVisit.Size = new System.Drawing.Size(1186, 286);
            this.TimeofVisit.TabIndex = 86;
            this.TimeofVisit.Text = "chart1";
            this.TimeofVisit.Click += new System.EventHandler(this.TimeofVisit_Click);
            // 
            // Analytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Group7SubdivisionTracking.Properties.Resources.blueHouses;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1538, 1079);
            this.Controls.Add(this.TimeofVisit);
            this.Controls.Add(this.lblFVisited);
            this.Controls.Add(this.lblTotalVisitor);
            this.Controls.Add(this.lblLatestVisitor);
            this.Controls.Add(this.lblFrequentVisitor);
            this.Controls.Add(this.commonPurpose);
            this.Controls.Add(this.totalVisitor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pctLogOut);
            this.Controls.Add(this.pctExit);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Analytics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analytics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Analytics_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalVisitor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commonPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeofVisit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnAnalytics;
        private System.Windows.Forms.Button btnVisitors;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pctLogOut;
        private System.Windows.Forms.PictureBox pctExit;
        private System.Windows.Forms.DataVisualization.Charting.Chart totalVisitor;
        private System.Windows.Forms.DataVisualization.Charting.Chart commonPurpose;
        private System.Windows.Forms.Label lblFrequentVisitor;
        private System.Windows.Forms.Label lblLatestVisitor;
        private System.Windows.Forms.Label lblTotalVisitor;
        private System.Windows.Forms.Label lblFVisited;
        private System.Windows.Forms.DataVisualization.Charting.Chart TimeofVisit;
    }
}