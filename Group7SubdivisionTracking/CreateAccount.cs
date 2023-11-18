using System;
using System.Data.Odbc;
using System.Linq;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{
    public partial class CreateAccount : Form
    {
        OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");
        OdbcCommand command;
        OdbcDataReader mdr;
        public CreateAccount()
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

        //button para makabalik sa login form
        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm frm1 = new LogInForm();
            frm1.Show();
        }

        //button para makapag create ng account
        private void btnCreateAcccount_Click(object sender, EventArgs e)
        {
            //validat kung may @ at . yung laman ng textbox
            if (!txtEmail.Text.Contains('@') || !txtEmail.Text.Contains('.'))
            {
                MessageBox.Show("Please Enter A Valid Email", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check kung match yung password at confirm password
            if (txtPassword.Text != txtCPassword.Text)
            {
                MessageBox.Show("Password doesn't match!", "Error");
                return;
            }

            //check kung may laman yung mga textbox
            if (string.IsNullOrEmpty(txtFName.Text) || string.IsNullOrEmpty(txtLName.Text) || string.IsNullOrEmpty(cboSex.Text) || string.IsNullOrEmpty(cboUsertype.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtCPassword.Text))
            {
                MessageBox.Show("Please fill out all information!", "Error");
                return;
            }

            using (OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
            {
                con.Open();

                string selectQuery = "SELECT * FROM subdivisionvisitortracking.userinfo WHERE Email = '" + txtEmail.Text + "'";
                using (OdbcCommand command = new OdbcCommand(selectQuery, con))
                {
                    using (OdbcDataReader mdr = command.ExecuteReader())
                    {
                        if (mdr.Read())
                        {
                            MessageBox.Show("Email already used!");
                            return;
                        }
                    }
                }

                
                string suffixValue = cboSuffix.SelectedItem?.ToString() == "Others" ? txtSpecifiedsuffix.Text : cboSuffix.Text;

                string insertQuery = "INSERT INTO subdivisionvisitortracking.userinfo(FirstName, LastName, Birthday, Sex, Suffix, Usertype, Email, Password, IsApproved) " +
                    "VALUES('" + txtFName.Text + "', '" + txtLName.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " +
                    "'" + cboSex.Text + "', '" + suffixValue + "','" + cboUsertype.Text + "', '" + txtEmail.Text + "', '" + txtPassword.Text + "', 0)";


                using (OdbcCommand command = new OdbcCommand(insertQuery, con))
                {
                    command.ExecuteNonQuery();
                }


                string getUserIdQuery = "SELECT UserID FROM subdivisionvisitortracking.userinfo WHERE Email = ?";
                int userId;
                using (OdbcCommand getUserIdCommand = new OdbcCommand(getUserIdQuery, con))
                {
                    getUserIdCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                    userId = (int)getUserIdCommand.ExecuteScalar();
                }

                MessageBox.Show("Account Successfully Created! Please wait for admin approval.", "Information");
            }

        }

        //iloload n'ya yung mga items pag nagload yung form
        private void CreateAccount_Load(object sender, EventArgs e)
        {
            txtSpecifiedsuffix.Visible = false;
            lblSpecify.Visible = false;
            cboSuffix.SelectedIndexChanged += CboSuffix_SelectedIndexChanged;
            cboSex.Items.Add("Female");
            cboSex.Items.Add("Male");
            cboSuffix.Items.Add("none");
            cboSuffix.Items.Add("Junior");
            cboSuffix.Items.Add("Senior");
            cboSuffix.Items.Add("Others");
            cboUsertype.Items.Add("User(Guard)");
            cboUsertype.Items.Add("Admin");
            dateTimePicker1.ValueChanged += DateTimePicker1_ValueChanged;
            dateTimePicker1.CloseUp += DateTimePicker1_CloseUp;
        }

        //kung magselect ng suffix na others, magiging visible yung textbox para sa specified suffix
        private void CboSuffix_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboSuffix.SelectedItem?.ToString() == "Others")
            {
                txtSpecifiedsuffix.Visible = true;
                txtSpecifiedsuffix.Enabled = true;
                lblSpecify.Enabled = true;
                lblSpecify.Visible = true;
            }
            else
            {
                txtSpecifiedsuffix.Visible = false;
                txtSpecifiedsuffix.Enabled = false;
                lblSpecify.Visible = false;
                lblSpecify.Enabled = false;
            }
        }
        
        //close button, yung x sa taas
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //clear lahat ng textboxes at comboboxes
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFName.Clear();
            txtLName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            txtCPassword.Clear();

            dateTimePicker1.Value = DateTime.Now;
            cboSex.SelectedIndex = -1;
            cboSuffix.SelectedIndex = -1;
            cboUsertype.SelectedIndex = -1;
        }

        //para hindi makapagtype ng numbers sa textbox
        private void txtFName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtLName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //minimize button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        //check na dapat 18 years old pataas yung age
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;

            if (IsUnder18(selectedDate))
            {
                MessageBox.Show("You must be 18 years or older to create an account.", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now.AddYears(-18);
            }
            else if (IsOver60(selectedDate))
            {
                MessageBox.Show("You cannot be older than 60 years to create an account.", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now.AddYears(-60);
            }

            CalculateAndDisplayAge();
        }

        //nag calculate ng datetimepicker, eto naman naka display sa textbox
        private void CalculateAndDisplayAge()
        {
            DateTime birthdate = dateTimePicker1.Value;
            int age = CalculateAge(birthdate);

            txtAge.Text = age.ToString();
        }

        //nag ccalculate kung ilang years old na yung user gamit data sa datetimepicker
        private int CalculateAge(DateTime birthdate)
        {
            cboSuffix.SelectedIndexChanged += CboSuffix_SelectedIndexChanged;
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthdate.Year;

            if (birthdate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        //dapat 18 years old pataas yung age
        private void DateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;

            if (IsUnder18(selectedDate))
            {
                MessageBox.Show("You must be 18 years or older to create an account.", "Invalid Age", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker1.Value = DateTime.Now.AddYears(-18);
            }
        }

        //check kung under 18 yung age
        private bool IsUnder18(DateTime birthdate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthdate.Year;

            if (birthdate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age < 18;
        }

        //check kung over 60 yung age
        private bool IsOver60(DateTime birthdate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthdate.Year;

            if (birthdate > currentDate.AddYears(-age))
            {
                age--;
            }

            return age > 60;
        }

        //para hindi makapagtype ng numbers sa textbox
        private void txtLName_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        //kung naka check yung show password, magiging visible yung password
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtCPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtCPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
