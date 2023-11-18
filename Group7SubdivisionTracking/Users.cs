using System;
using System.Data;
using System.Data.Odbc;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{
    public partial class Users : Form
    {
        OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");
        OdbcCommand command;
        OdbcDataReader mdr;

        public Users()
        {
            InitializeComponent();
        }
        private void Users_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn archiveButtonColumn = new DataGridViewButtonColumn();
            archiveButtonColumn.HeaderText = "Archive User";
            archiveButtonColumn.Text = "Archive";
            archiveButtonColumn.UseColumnTextForButtonValue = true;
            ManageUsersDGV.Columns.Add(archiveButtonColumn);

            DataGridViewButtonColumn AcceptUserColumn = new DataGridViewButtonColumn();
            AcceptUserColumn.HeaderText = "Accept User";
            AcceptUserColumn.Text = "Accept";
            AcceptUserColumn.UseColumnTextForButtonValue = true;
            PendingUsersDGV.Columns.Add(AcceptUserColumn);

            DataGridViewButtonColumn DeclineUserColumn = new DataGridViewButtonColumn();
            DeclineUserColumn.HeaderText = "Decline User";
            DeclineUserColumn.Text = "Decline";
            DeclineUserColumn.UseColumnTextForButtonValue = true;
            PendingUsersDGV.Columns.Add(DeclineUserColumn);

            DataGridViewButtonColumn ArchivedUserColumn = new DataGridViewButtonColumn();
            ArchivedUserColumn.HeaderText = "Unarchive User";
            ArchivedUserColumn.Text = "Unarchive";
            ArchivedUserColumn.UseColumnTextForButtonValue = true;
            ArchivedDGV.Columns.Add(ArchivedUserColumn);

            ManageUsersDGV.CellContentClick += ManageUsersDGV_CellContentClick;

            FILLUSERSDGV();
            FILLPENDINGDGV();
            FILLARCHIVEDDGV();

            ManageUserColumnVisibility("UserID", false);
            ManageUserColumnVisibility("Email", false);
            ManageUserColumnVisibility("Password", false);
            ManageUserColumnVisibility("IsApproved", false);

            PendingUserColumnVisibility("UserID", false);
            PendingUserColumnVisibility("IsApproved", false);
            PendingUserColumnVisibility("Email", false);
            PendingUserColumnVisibility("Password", false);

            ArchivedUserVisibility("UserID", false);
            ArchivedUserVisibility("Email", false);
            ArchivedUserVisibility("Password", false);
            ArchivedUserVisibility("IsApproved", false);
        }

        private void pctExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            LogInForm frm = new LogInForm();
            frm.Show();
        }
        private void FILLUSERSDGV()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();

            string dataQuery = "SELECT * FROM userinfo WHERE IsApproved = 1";
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(dataQuery, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            ManageUsersDGV.DataSource = dt;
            con.Close();
        }

        private void FILLPENDINGDGV()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();

            string dataQuery = "SELECT * FROM userinfo WHERE IsApproved = 0";
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(dataQuery, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            PendingUsersDGV.DataSource = dt;
            con.Close();
        }
        private void FILLARCHIVEDDGV()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();

            string dataQuery = "SELECT * FROM userinfo WHERE IsApproved = 2";
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(dataQuery, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            ArchivedDGV.DataSource = dt;
            con.Close();
        }
        private void ManageUserColumnVisibility(string columnName, bool isVisible)
        {
            if (ManageUsersDGV.Columns.Contains(columnName))
            {
                ManageUsersDGV.Columns[columnName].Visible = isVisible;
            }
        }
        private void PendingUserColumnVisibility(string columnName, bool isVisible)
        {
            if (PendingUsersDGV.Columns.Contains(columnName))
            {
                PendingUsersDGV.Columns[columnName].Visible = isVisible;
            }
        }
        private void ArchivedUserVisibility(string columnName, bool isVisible)
        {
            if (ArchivedDGV.Columns.Contains(columnName))
            {
                ArchivedDGV.Columns[columnName].Visible = isVisible;
            }
        }
        private void ManageUsersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = ManageUsersDGV.Columns[e.ColumnIndex];

                if (column is DataGridViewButtonColumn && column.HeaderText == "Archive User")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to archive this user?", "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (ManageUsersDGV.Columns.Contains("UserID"))
                        {
                            int userId = Convert.ToInt32(ManageUsersDGV.Rows[e.RowIndex].Cells["UserID"].Value);

                            string updateQuery = "UPDATE userinfo SET IsApproved = 2 WHERE UserID = " + userId;

                            using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, con))
                            {
                                con.Open();
                                updateCommand.ExecuteNonQuery();
                                con.Close();
                            }

                            FILLUSERSDGV();
                            FILLARCHIVEDDGV();
                        }
                        else
                        {
                            MessageBox.Show("The 'UserID' column is not present in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void PendingUsersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = PendingUsersDGV.Columns[e.ColumnIndex];

                if (column is DataGridViewButtonColumn)
                {
                    if (column.HeaderText == "Accept User")
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to accept " + PendingUsersDGV.Rows[e.RowIndex].Cells["FirstName"].Value + "?", "Confirm Accept", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            AcceptUser(e);
                        }
                    }
                    else if (column.HeaderText == "Decline User")
                    {
                        DialogResult result = MessageBox.Show("Are you sure you want to decline " + PendingUsersDGV.Rows[e.RowIndex].Cells["FirstName"].Value + "?", "Confirm Decline", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            DeclineUser(e);
                        }
                    }
                }
            }
        }

        private void AcceptUser(DataGridViewCellEventArgs e)
        {
            if (PendingUsersDGV.Columns.Contains("UserID"))
            {
                int userId = Convert.ToInt32(PendingUsersDGV.Rows[e.RowIndex].Cells["UserID"].Value);
                string updateQuery = "UPDATE userinfo SET IsApproved = 1 WHERE UserID = " + userId;

                using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, con))
                {
                    con.Open();
                    updateCommand.ExecuteNonQuery();
                    con.Close();
                }

                FILLPENDINGDGV();
                FILLUSERSDGV();
            }
            else
            {
                MessageBox.Show("The 'UserID' column is not present in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeclineUser(DataGridViewCellEventArgs e)
        {
            if (PendingUsersDGV.Columns.Contains("UserID"))
            {
                int userId = Convert.ToInt32(PendingUsersDGV.Rows[e.RowIndex].Cells["UserID"].Value);
                string updateQuery = "UPDATE userinfo SET IsApproved = 3 WHERE UserID = " + userId;

                using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, con))
                {
                    con.Open();
                    updateCommand.ExecuteNonQuery();
                    con.Close();
                }

                FILLPENDINGDGV();
                FILLUSERSDGV();
            }
            else
            {
                MessageBox.Show("The 'UserID' column is not present in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardForm frm = new DashboardForm();
            frm.Show();
        }

        private void Visitors_Click(object sender, EventArgs e)
        {
            this.Hide();
            Visitors frm = new Visitors();
            frm.Show();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analytics frm = new Analytics();
            frm.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Users frm = new Users();
            frm.Show();
        }

        private void ArchivedDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = ArchivedDGV.Columns[e.ColumnIndex];

                if (column is DataGridViewButtonColumn && column.HeaderText == "Unarchive User")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to unarchive this user?", "Confirm Unarchive", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (ArchivedDGV.Columns.Contains("UserID"))
                        {
                            int userId = Convert.ToInt32(ArchivedDGV.Rows[e.RowIndex].Cells["UserID"].Value);

                            string updateQuery = "UPDATE userinfo SET IsApproved = 1 WHERE UserID = " + userId;

                            using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, con))
                            {
                                con.Open();
                                updateCommand.ExecuteNonQuery();
                                con.Close();
                            }

                            FILLARCHIVEDDGV();
                            FILLUSERSDGV();
                        }
                        else
                        {
                            MessageBox.Show("The 'UserID' column is not present in the DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

    }
}
