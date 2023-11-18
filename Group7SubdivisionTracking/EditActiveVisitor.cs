using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{
    public partial class EditActiveVisitor : Form
    {
        OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");
        OdbcCommand command;
        OdbcDataReader mdr;
        public int SelectedVisId { get; set; }
        public string SelectedPlateNumber { get; set; }
        public string SelectedFirstName { get; set; }
        public string SelectedLastName { get; set; }
        public string SelectedSex { get; set; }
        public string SelectedHouseNo { get; set; }
        public string SelectedRelation { get; set; }
        public string SelectedPurpose { get; set; }
        public DateTime SelectedEntranceDateTime { get; set; }
        public DateTime? SelectedExitDateTime { get; set; }
        public EditActiveVisitor()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnAddvisitor_Click(object sender, EventArgs e)
        {

        }

        private void chkVehicle_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtHouseno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtHouseno_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtVislname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtVisfname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVisfname_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPlateno_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditActiveVisitor_Load(object sender, EventArgs e)
        {
            cboSex.Items.Add("Female");
            cboSex.Items.Add("Male");

            cboRelation.Items.Add("Guest");
            cboRelation.Items.Add("Family Visitation");
            cboRelation.Items.Add("Delivery");
            cboRelation.Items.Add("Service Personnel");

            cboPurpose.Items.Add("Visiting Resident");
            cboPurpose.Items.Add("Family Visitation");
            cboPurpose.Items.Add("Package Delivery");
            cboPurpose.Items.Add("Maintenance/Service");
            cboPurpose.Items.Add("others");

            //txtVisID.Text = SelectedVisId.ToString();
            txtVisfname.Text = SelectedFirstName;
            txtVislname.Text = SelectedLastName;

            cboSex.SelectedItem = SelectedSex;
            cboRelation.SelectedItem = SelectedRelation;

            if (!cboPurpose.Items.Contains(SelectedPurpose))
            {
                cboPurpose.SelectedItem = "others";
                txtPurposespecify.Text = SelectedPurpose;
            }
            else
            {
                cboPurpose.SelectedItem = SelectedPurpose;
            }

            txtHouseno.Text = SelectedHouseNo;
            datetimeEntrance.Value = SelectedEntranceDateTime;


            if (SelectedExitDateTime.HasValue)
            {
                //datetimeExit.Value = SelectedExitDateTime.Value;
            }

            chkVehicle.Checked = !string.IsNullOrEmpty(SelectedPlateNumber);

            txtPlateno.Enabled = chkVehicle.Checked;

            if (!string.IsNullOrEmpty(SelectedPlateNumber))
            {
                txtPlateno.Text = SelectedPlateNumber;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVisfname.Text) || string.IsNullOrEmpty(txtVislname.Text) ||
                string.IsNullOrEmpty(cboSex.Text) || string.IsNullOrEmpty(cboRelation.Text) ||
                string.IsNullOrEmpty(cboPurpose.Text))
            {
                MessageBox.Show("Please fill out all required information!", "Error");
                return;
            }

            if (!IsHouseNumberValid(txtHouseno.Text))
            {
                MessageBox.Show("Invalid House Number! Please enter a valid house number.", "Error");
                return;
            }

            string plateNumber = chkVehicle.Checked ? txtPlateno.Text : "N/A";

            string updateQuery = "UPDATE subdivisionvisitortracking.visitorinfo SET " +
                     "`Plate Number` = '" + plateNumber + "', " +
                     "`First Name` = '" + txtVisfname.Text + "', " +
                     "`Last Name` = '" + txtVislname.Text + "', " +
                     "Sex = '" + cboSex.Text + "', " +
                     "`ResHouseNo.` = '" + txtHouseno.Text + "', " +
                     "Relation = '" + cboRelation.Text + "', ";



            string purpose = cboPurpose.Text.Equals("others", StringComparison.OrdinalIgnoreCase)
                                 ? txtPurposespecify.Text
                                 : cboPurpose.Text;

            updateQuery += "Purpose = '" + purpose + "' WHERE VisID = " + SelectedVisId;

            try
            {
                using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, con))
                {
                    con.Open();
                    updateCommand.ExecuteNonQuery();
                    MessageBox.Show("Visitor information updated successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            this.Close();

            btnClear_Click(sender, e);
        }



        private bool IsHouseNumberValid(string houseNumber)
        {
            try
            {
                string selectQuery = "SELECT ResHouseNo FROM resinfo WHERE ResHouseNo = '" + houseNumber + "'";

                con.Open();

                using (OdbcCommand command = new OdbcCommand(selectQuery, con))
                {
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void txtVisfname_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

        }

        private void txtVislname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtVislname_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }


        }

        private void txtPlateno_KeyPress(object sender, KeyPressEventArgs e)
        {
            string inputText = txtPlateno.Text;

            string cleanedText = new string(inputText.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());

            string uppercasedText = cleanedText.ToUpper();

            txtPlateno.Text = uppercasedText;

            txtPlateno.SelectionStart = txtPlateno.Text.Length;
        }

        private void txtPurposespecify_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
