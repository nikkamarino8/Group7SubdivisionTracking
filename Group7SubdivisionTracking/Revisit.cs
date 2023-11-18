using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{
    public partial class Revisit : Form
    {
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
        public Revisit()
        {
            InitializeComponent();

        }

        private void Revisit_Load(object sender, EventArgs e)
        {
            cboSex.Items.Add("Female");
            cboSex.Items.Add("Male");

            cboRelation.Items.Add("Guest");
            cboRelation.Items.Add("Family Visitation");
            cboRelation.Items.Add("Delivery");
            cboRelation.Items.Add("Service Personel");

            cboPurpose.Items.Add("Visiting Resident");
            cboPurpose.Items.Add("Family Visitation");
            cboPurpose.Items.Add("Package Delivery");
            cboPurpose.Items.Add("Maintenance/Service");
            cboPurpose.Items.Add("others");
         
            txtPlateno.Text = SelectedPlateNumber;
            txtVisfname.Text = SelectedFirstName;
            txtVislname.Text = SelectedLastName;
            cboSex.Text = SelectedSex;
            txtHouseno.Text = SelectedHouseNo;
            cboRelation.Text = SelectedRelation;
            cboPurpose.Text = SelectedPurpose;
            datetimeEntrance.Text = SelectedEntranceDateTime.ToString();

            if (!string.IsNullOrEmpty(SelectedPurpose) && cboPurpose.Items.Contains(SelectedPurpose))
            {
                cboPurpose.SelectedItem = SelectedPurpose;
            }
            else
            {
                cboPurpose.SelectedItem = "others";

                txtPurposespecify.Text = SelectedPurpose;
            }
        }

        
        

        private void cboPurpose_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void btnRevisit_Click(object sender, EventArgs e)
        {
            try
            {
                using (OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
                {
                    con.Open();

                    string insertQuery = "INSERT INTO subdivisionvisitortracking.visitorinfo " +
                        "(`Plate Number`, `First Name`, `Last Name`, Sex, `ResHouseNo.`, Relation, Purpose, `Entrance Date Time`) " +
                        "VALUES('" + txtPlateno.Text + "', '" + txtVisfname.Text + "', '" + txtVislname.Text + "', '" + cboSex.Text + "', " +
                        "'" + txtHouseno.Text + "', '" + cboRelation.Text + "', '" + cboPurpose.Text + "', NOW())";

                    using (OdbcCommand insertCommand = new OdbcCommand(insertQuery, con))
                    {
                        insertCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Revisit successful! New record added to visitorinfo.");

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error");
            }
        }
    }
}
