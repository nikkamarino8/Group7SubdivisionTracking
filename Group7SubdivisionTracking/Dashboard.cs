using System;
using System.Data.Odbc;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{
    public partial class DashboardForm : Form
    {
        OdbcConnection con;
        OdbcCommand command;

        //naka initialize yung method na to sa pag load ng form 
        public DashboardForm()
        {

            InitializeComponent();
            UpdateVisitorCount();
            UpdateLatestVisitorInfo();
            UpdateDateLabel();
            con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");

            AdjustButtonVisibility();
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
        }

        //pag load ng form lalabas yung first name ng user na naka login
        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblUserFirstName.Text = UserSession.FirstName + "!";
            lblLastLogin.Text = "Last Log In:   " + UserSession.LastLogin;

            UpdateDashboardLabels();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("UserSession.FirstName: " + UserSession.FirstName);
            lblUserFirstName.Text = UserSession.FirstName;
        }

        //pag click ng button na visitors lalabas yung form na visitors
        private void Visitors_Click(object sender, EventArgs e)
        {
            this.Hide();
            Visitors frm1 = new Visitors();
            frm1.Show();
        }

        //pag click ng button na users lalabas yung form na dashboard
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardForm frm2 = new DashboardForm();
            frm2.Show();
        }
        //pag click ng button na log out lalabas yung form na login
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LogInForm frm3 = new LogInForm();
            frm3.Show();
        }

        //pag click ng button na x mag cclose yung form
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //logout, babalik sa login form
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm form = new LogInForm();
            form.Show();
        }

        private void lblUserFirstName_Click(object sender, EventArgs e)
        {

        }

        //pag na click mag mminimize yung form
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //update sana yung labels sa dashboard
        private void btnDashboard_Click_1(object sender, EventArgs e)
        {
            UpdateDashboardLabels();
        }

        //update sana yung labels sa dashboard
        private void UpdateDashboardLabels()
        {
            lblLastLogin.Text = "Last Log In:   " + UserSession.LastLogin;

        }

        //pag click ng button na visitors lalabas yung form na analytics
        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analytics frm1 = new Analytics();
            frm1.Show();
        }

        //pag click ng button na users lalabas yung form na users
        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Users frm1 = new Users();
            frm1.Show();
        }

        //todays date, binago narin yung format para Month Day, Year
        private void UpdateDateLabel()
        {

            string formattedDate = DateTime.Now.ToString("MMMM dd, yyyy");

            lblTodayIs.Text = "Today's Date: " + formattedDate;
        }

        //i uupdate yung visitor count, kung nadagdagan ba tapos ididisplay yung total visitors today
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

                    lblcountings.Text = "Total Visitors Today: " + visitorCount.ToString();
                }

                connection.Close();
            }
        }

        //i uupdate yung latest visitor info, kung sino yung pinaka huling visitor na dumating yung first name at last name nya ididisplay
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

        private void lblLatestVisitor_Click(object sender, EventArgs e)
        {

        }
    }
}
