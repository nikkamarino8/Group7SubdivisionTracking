using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Group7SubdivisionTracking
{
    public partial class Analytics : Form
    {
        OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");
        OdbcCommand command;
        OdbcDataReader mdr;
        public Analytics()
        {
            InitializeComponent();
            UpdateLatestVisitorInfo();
            UpdateVisitorCount();
            UpdateFrequentlyVisitedHouseThisWeek();
            UpdateFrequentVisitorThisWeek();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
        }

        //pag guard naka login nakatago yung button na users
        private void AdjustButtonVisibility()
        {
            if (UserSession.UserType == "User(Guard)")
            {
                btnUsers.Visible = false;
            }
            else
            {
                btnUsers.Visible = true;
            }
        }

        //button papunta sa dashboard form
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardForm frm1 = new DashboardForm();
            frm1.Show();
        }

        //button papunta sa visitors form
        private void btnVisitors_Click(object sender, EventArgs e)
        {
            this.Hide();
            Visitors frm1 = new Visitors();
            frm1.Show();
        }

        //button papunta sa analytics form
        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analytics frm1 = new Analytics();
            frm1.Show();
        }

        //exit button 
        private void pctExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //minimize button
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //logout button papunta sa login form
        private void pctLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            LogInForm frm = new LogInForm();
            frm.Show();
        }

        //pag load ng form lalabas yung mga data na nasa form
        private void Analytics_Load(object sender, EventArgs e)
        {
            LoadTimeOfVisitData();
            AdjustButtonVisibility();
            totalVisitor.Titles.Add("Total Visitors per Month").ForeColor = Color.Black;

            commonPurpose.Titles.Add("Common Purpose Distribution").ForeColor = Color.Black;
            CustomizePieChartAppearance(commonPurpose);
            CustomizeChartAppearance();
            LoaddbData();
            LoadCommonPurposeData();

        }

        //naglalagay ng data sa chart na total visitor per month 
        private void LoaddbData()
        {
            try
            {
                con.Open();

                string query = "SELECT MONTH(EntranceDateTime) AS Month, COUNT(*) AS TotalVisitors " +
                               "FROM visitorlog " +
                               "GROUP BY MONTH(EntranceDateTime)";

                command = new OdbcCommand(query, con);

                mdr = command.ExecuteReader();

                while (mdr.Read())
                {
                    totalVisitor.Series["Total Visitors"].Points.AddXY(
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mdr.GetInt32(0)),
                        mdr.GetInt32(1)
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        //customize ng chart na total visitor per month
        private void CustomizeChartAppearance()
        {
            totalVisitor.BackColor = Color.White;

            totalVisitor.ForeColor = Color.Black;

            totalVisitor.Series["Total Visitors"].Color = Color.PaleVioletRed;


            totalVisitor.Series["Total Visitors"]["BarLabelStyle"] = "Center";
            totalVisitor.Series["Total Visitors"]["PointWidth"] = "0.6";
            totalVisitor.Series["Total Visitors"]["PixelPointDepth"] = "20";

            totalVisitor.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
            totalVisitor.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;

        }

        //naglalagay ng data sa chart na common purpose distribution, kung anong purpose ng mga visitor 
        private void LoadCommonPurposeData()
        {
            try
            {
                con.Open();

                string query = "SELECT Purpose, COUNT(*) AS PurposeCount " +
                               "FROM visitorlog INNER JOIN visitorinfo ON visitorlog.VisID = visitorinfo.VisID " +
                               "GROUP BY Purpose";

                command = new OdbcCommand(query, con);

                mdr = command.ExecuteReader();

                Dictionary<string, int> purposeData = new Dictionary<string, int>();

                while (mdr.Read())
                {
                    string purpose = mdr.GetString(0);
                    int count = mdr.GetInt32(1);

                    purposeData[purpose] = count;
                }

                foreach (var kvp in purposeData)
                {
                    commonPurpose.Series["commonPurpose"].Points.AddXY(
                        kvp.Key + " (" + kvp.Value + ")",
                        kvp.Value
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void CustomizePieChartAppearance(Chart chart)
        {

        }

        //button papunta sa users form
        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Users frm = new Users();
            frm.Show();
        }

        private void commonPurpose_Click(object sender, EventArgs e)
        {

        }

        //chart para sa time of visit, kung anong oras dumadating yung visitor
        private void LoadTimeOfVisitData()
        {
            try
            {
                con.Open();

                string query = "SELECT HOUR(EntranceDateTime) AS Hour, COUNT(*) AS VisitCount " +
                               "FROM visitorlog " +
                               "WHERE MONTH(EntranceDateTime) = MONTH(CURRENT_DATE) " +
                               "GROUP BY HOUR(EntranceDateTime)";

                command = new OdbcCommand(query, con);

                mdr = command.ExecuteReader();

                while (mdr.Read())
                {
                    int hour = mdr.GetInt32(0);
                    int visitCount = mdr.GetInt32(1);

                    string formattedTime = new DateTime(2023, 1, 1, hour, 0, 0).ToString("h tt");

                    TimeofVisit.Series[0].Points.AddXY(formattedTime, visitCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void totalVisitor_Click(object sender, EventArgs e)
        {

        }

        private void lblFrequentVisitor_Click(object sender, EventArgs e)
        {

        }

        private void lblLatestVisitor_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalVisitor_Click(object sender, EventArgs e)
        {

        }

        private void lblFVisited_Click(object sender, EventArgs e)
        {

        }

        private void TimeofVisit_Click(object sender, EventArgs e)
        {

        }

        //update yung name ng latest visitor
        private void UpdateLatestVisitorInfo()
        {
            string connectionString = "DRIVER={MySQL ODBC 8.0 Unicode Driver};server=localhost;port=3306;database=subdivisionvisitortracking;user=root;password=";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                string latestVisitorQuery = "SELECT `First Name`, `Last Name` FROM visitorinfo ORDER BY `Entrance Date Time` DESC LIMIT 1";

                using (OdbcCommand latestVisitorCommand = new OdbcCommand(latestVisitorQuery, connection))
                {
                    OdbcDataReader reader = latestVisitorCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        lblLatestVisitor.Text = "Latest Visitor: " + reader["First Name"].ToString() + " " + reader["Last Name"].ToString();
                    }
                    else
                    {
                        lblLatestVisitor.Text = "No visitors today";
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        //update yung total visitor today
        private void UpdateVisitorCount()
        {
            string connectionString = "DRIVER={MySQL ODBC 8.0 Unicode Driver};server=localhost;port=3306;database=subdivisionvisitortracking;user=root;password=";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                string countQuery = "SELECT COUNT(*) FROM visitorinfo WHERE DATE(`Entrance Date Time`) = CURDATE()";

                using (OdbcCommand countCommand = new OdbcCommand(countQuery, connection))
                {
                    int visitorCount = Convert.ToInt32(countCommand.ExecuteScalar());
                    lblTotalVisitor.Text = "Total Visitors Today: " + visitorCount.ToString();
                }
                connection.Close();
            }
        }

        //update yung frequently visited house this week, head of the family naka display at kung ilang beses na visit this week
        private void UpdateFrequentlyVisitedHouseThisWeek()
        {
            string connectionString = "DRIVER={MySQL ODBC 8.0 Unicode Driver};server=localhost;port=3306;database=subdivisionvisitortracking;user=root;password=";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                string frequentlyVisitedHouseQuery = "SELECT r.`ResHeadofFamily`, COUNT(*) as VisitsCount " +
                                                     "FROM visitorlog v " +
                                                     "JOIN resinfo r ON v.`ResID` = r.`ResID` " +
                                                     "WHERE YEARWEEK(v.`EntranceDateTime`, 1) = YEARWEEK(NOW(), 1) " +
                                                     "AND WEEKDAY(v.`EntranceDateTime`) BETWEEN 0 AND 6 " +
                                                     "GROUP BY r.`ResHeadofFamily` " +
                                                     "ORDER BY VisitsCount DESC LIMIT 1";

                using (OdbcCommand frequentlyVisitedHouseCommand = new OdbcCommand(frequentlyVisitedHouseQuery, connection))
                {
                    using (OdbcDataReader reader = frequentlyVisitedHouseCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string headOfFamily = reader["ResHeadofFamily"].ToString();
                            int visitsCount = Convert.ToInt32(reader["VisitsCount"]);

                            lblFVisited.Text = $"Frequently Visited Household: {headOfFamily} ({visitsCount} visit{(visitsCount != 1 ? "s" : "")}) this week.";
                        }
                        else
                        {
                            lblFVisited.Text = "No visits this week";
                        }
                    }
                }

                connection.Close();
            }
        }

        //update yung frequent visitor this week, kung sino yung pinaka madalas na dumadating na visitor this week
        private void UpdateFrequentVisitorThisWeek()
        {
            string connectionString = "DRIVER={MySQL ODBC 8.0 Unicode Driver};server=localhost;port=3306;database=subdivisionvisitortracking;user=root;password=";

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                connection.Open();

                string frequentVisitorQuery = "SELECT `First Name`, `Last Name`, COUNT(*) as VisitsCount FROM visitorinfo WHERE YEARWEEK(`Entrance Date Time`, 1) = YEARWEEK(NOW(), 1) GROUP BY `First Name`, `Last Name` ORDER BY VisitsCount DESC LIMIT 1";

                using (OdbcCommand frequentVisitorCommand = new OdbcCommand(frequentVisitorQuery, connection))
                {
                    OdbcDataReader reader = frequentVisitorCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        int visitsCount = Convert.ToInt32(reader["VisitsCount"]);
                        if (visitsCount > 0)
                        {
                            lblFrequentVisitor.Text = "Frequent Visitor This Week: " + reader["First Name"].ToString() + " " + reader["Last Name"].ToString();
                        }
                        else
                        {
                            lblFrequentVisitor.Text = "No visits this week";
                        }
                    }
                    else
                    {
                        lblFrequentVisitor.Text = "No visits this week";
                    }

                    reader.Close();
                }

                connection.Close();
            }
        }
    }
}

