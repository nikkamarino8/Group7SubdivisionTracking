using System;
using System.Data.Odbc;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{

    public partial class LogInForm : Form
    {
        OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");
        OdbcCommand command;
        OdbcDataReader mdr;
        public LogInForm()
        {
            InitializeComponent();
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

        //pinapakita yung create account form
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            CreateAccount frm1 = new CreateAccount();
            frm1.Show();
        }

        //login button, kinukuha n'ya yung input sa textboxes then chinecheck kung anong klaseng user s'ya, if admin or guard, then dito rin chinecheck yung
        //kung approved na ba yung account n'ya or hindi pa, kung hindi pa, hindi s'ya makakapaglogin, kung approved na, pwede n'ya nang gamitin yung account n'ya, tapos dito rin n'ya tinatawag yung SaveAccountInfo() method
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please input Email and Password", "Error");
            }
            else if (cboUsertype.SelectedItem == null)
            {
                MessageBox.Show("Please select a user type", "Error");
            }
            else
            {
                con.Open();

                string selectQuery = "SELECT * FROM subdivisionvisitortracking.userinfo WHERE Email = '" + txtEmail.Text + "' AND Password = '" + txtPassword.Text + "' AND UserType = '" + cboUsertype.SelectedItem.ToString() + "' AND IsApproved = 1;";
                command = new OdbcCommand(selectQuery, con);
                mdr = command.ExecuteReader();

                if (mdr.Read())
                {
                    int isApproved = Convert.ToInt32(mdr["IsApproved"]);

                    if (isApproved == 2)
                    {
                        MessageBox.Show("This account is archived. Please contact the administrator for assistance.", "Archived Account");
                        con.Close();
                        return;
                    }
                    else if (isApproved == 3)
                    {
                        MessageBox.Show("Your account has been declined. Please contact the administrator for more information.", "Declined Account");
                        con.Close();
                        return;
                    }

                    SaveToAccountInfo();

                    this.Hide();
                    DashboardForm frm3 = new DashboardForm();
                    frm3.ShowDialog();
                }
                else
                {
                    string userTypeCheckQuery = "SELECT * FROM subdivisionvisitortracking.userinfo WHERE Email = '" + txtEmail.Text + "' AND Password = '" + txtPassword.Text + "';";
                    command = new OdbcCommand(userTypeCheckQuery, con);
                    mdr = command.ExecuteReader();

                    if (mdr.Read())
                    {
                        int isApproved = Convert.ToInt32(mdr["IsApproved"]);

                        if (isApproved == 0)
                        {
                            MessageBox.Show("Please wait for admin approval.", "Information");
                        }
                        else if (isApproved == 2)
                        {
                            MessageBox.Show("This account is archived. Please contact the administrator for assistance.", "Archived Account");
                        }
                        else if (isApproved == 3)
                        {
                            MessageBox.Show("Your account has been declined. Please contact the administrator for more information.", "Declined Account");
                        }
                        else
                        {
                            MessageBox.Show("We couldn't find an account that matches your selected user type. Please choose the correct user type.", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Login Information! Try again.", "Error");
                    }
                }

                con.Close();
            }
        }

        //method na nagssave ng account info sa accountinfo table sa database na ginagamit natin sa program na 'to para ma-track yung last login ng user at yung user type n'ya at yung email n'ya para ma compare dun sa data na nasa userinfo table
        private void SaveToAccountInfo()
        {
            try
            {
                using (OdbcConnection accountInfoCon = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
                {
                    accountInfoCon.Open();

                    string selectUserIdQuery = "SELECT UserID FROM subdivisionvisitortracking.userinfo WHERE Email = ?";
                    using (OdbcCommand selectUserIdCommand = new OdbcCommand(selectUserIdQuery, accountInfoCon))
                    {
                        selectUserIdCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                        object userIdObj = selectUserIdCommand.ExecuteScalar();

                        if (userIdObj != null)
                        {
                            int userId = Convert.ToInt32(userIdObj);

                            string selectQuery = "SELECT COUNT(*) FROM subdivisionvisitortracking.accountinfo WHERE UserID = ?";
                            using (OdbcCommand selectCommand = new OdbcCommand(selectQuery, accountInfoCon))
                            {
                                selectCommand.Parameters.AddWithValue("@UserID", userId);
                                int count = Convert.ToInt32(selectCommand.ExecuteScalar());

                                if (count > 0)
                                {
                                    string updateQuery = "UPDATE subdivisionvisitortracking.accountinfo SET `Last Login` = NOW() WHERE UserID = ?";
                                    using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, accountInfoCon))
                                    {
                                        updateCommand.Parameters.AddWithValue("@UserID", userId);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    string insertQuery = "INSERT INTO subdivisionvisitortracking.accountinfo (UserID, Email, Password, Usertype, `Last Login`) " +
                                                         "VALUES (?, ?, ?, ?, NOW())";

                                    using (OdbcCommand insertCommand = new OdbcCommand(insertQuery, accountInfoCon))
                                    {
                                        insertCommand.Parameters.AddWithValue("@UserID", userId);
                                        insertCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                                        insertCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
                                        insertCommand.Parameters.AddWithValue("@Usertype", cboUsertype.SelectedItem.ToString());

                                        insertCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            UserSession.Email = txtEmail.Text;
                            UserSession.Password = txtPassword.Text;
                            UserSession.UserType = cboUsertype.SelectedItem.ToString();
                            UserSession.LastLogin = GetLastLogin(txtEmail.Text);
                            UserSession.FirstName = GetFirstName(txtEmail.Text);
                            UserSession.UserID = userId.ToString();
                        }
                        else
                        {
                            MessageBox.Show("UserID not found for the logged-in user.", "Error");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving account information: " + ex.Message);
            }
        }

        //clear button, clears the textboxes
        private void clearButton_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
        }

        //exit button, closes the program
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //niloload n'ya na agad yung combo boxes data pag nagload yung form
        private void LogInForm_Load(object sender, EventArgs e)
        {
            cboUsertype.Items.Add("User(Guard)");
            cboUsertype.Items.Add("Admin");
        }

        //show password checkbox, para makita yung password na nilalagay n'ya
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        //method na nagreretrieve ng first name ng user, para madisplay sa "WELCOME Fname!"
        private string GetFirstName(string userEmail)
        {
            string firstName = "";
            try
            {
                using (OdbcConnection connection = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
                {
                    connection.Open();

                    string selectQuery = "SELECT FirstName FROM subdivisionvisitortracking.userinfo WHERE Email = '" + userEmail + "'";

                    using (OdbcCommand selectCommand = new OdbcCommand(selectQuery, connection))
                    {
                        OdbcDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            firstName = reader["FirstName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving first name: " + ex.Message);
            }

            return firstName;
        }

        //method na nagreretrieve ng user id ng user, para ma save sa UserSession class
        private string GetUserID(string userEmail)
        {
            string userID = "";
            try
            {
                using (OdbcConnection connection = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
                {
                    connection.Open();

                    string selectQuery = "SELECT UserID FROM subdivisionvisitortracking.userinfo WHERE Email = '" + userEmail + "'";

                    using (OdbcCommand selectCommand = new OdbcCommand(selectQuery, connection))
                    {
                        OdbcDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            userID = reader["UserID"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving user UserID: " + ex.Message);
            }

            return userID;
        }

        //method na nagreretrieve ng last login ng user, para ma save sa UserSession class
        private string GetLastLogin(string userEmail)
        {
            string lastLogin = "";
            try
            {
                using (OdbcConnection connection = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
                {
                    connection.Open();

                    string selectQuery = "SELECT `Last Login` FROM subdivisionvisitortracking.accountinfo WHERE Email = '" + userEmail + "'";

                    using (OdbcCommand selectCommand = new OdbcCommand(selectQuery, connection))
                    {
                        OdbcDataReader reader = selectCommand.ExecuteReader();

                        if (reader.Read())
                        {
                            DateTime lastLoginDate = Convert.ToDateTime(reader["Last Login"]);
                            lastLogin = lastLoginDate.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving last login: " + ex.Message);
            }

            return lastLogin;
        }

        //minimize button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
