using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Windows.Forms;

namespace Group7SubdivisionTracking
{
    public partial class Visitors : Form
    {
        OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;");
        OdbcCommand command;
        OdbcDataReader mdr;
        public Visitors()
        {
            InitializeComponent();
            txtPlateno.Enabled = chkVehicle.Checked;
            FILLVISDGV();
            FILLVISLOGDGV();
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

        //pag nag click ng button na dashboard lalabas yung form na dashboard
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardForm frm1 = new DashboardForm();
            frm1.Show();

        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LogInForm frm1 = new LogInForm();
            frm1.Show();
        }

        //exit button yung x sa taas ng form
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //hihide tong form na to tas ilalabas yung loginform
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm form1 = new LogInForm();
            form1.Show();
        }

        //tignan kung naka check yung checkbox na vehicle, pag naka check pwede a mag type sa textbox na plate number, pag hindi naka check di ka makakatype
        private void chkVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVehicle.Checked == true)
            {
                txtPlateno.Enabled = true;
            }
            else
            {
                txtPlateno.Enabled = false;
            }
        }

        //load ng form na to
        private void Visitors_Load(object sender, EventArgs e)
        {
            searchData("");

            AdjustButtonVisibility();
            txtPurposespecify.Visible = false;
            lblSpecify.Visible = false;

            txtPlateno.Enabled = chkVehicle.Checked;
            FILLVISDGV();
            FILLVISLOGDGV();

            //DataGridViewButtonColumn revisitButtonColumn = new DataGridViewButtonColumn();
            //revisitButtonColumn.HeaderText = "Revisit User";
            //revisitButtonColumn.Text = "Re-Visitor";
            //revisitButtonColumn.UseColumnTextForButtonValue = true;
            //visitorlogDataGridView.Columns.Add(revisitButtonColumn);

            foreach (DataGridViewColumn column in VisDataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewColumn column in visitorlogDataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            AddActionButtonColumn();
            VisDataGridView.CellClick += VisDataGridView_CellClick;

            SetColumnVisibility("First Name", true);
            SetColumnVisibility("Entrance Date Time", true);
            SetColumnVisibility("Exit Date Time", false);

            SetColumnVisibility("VisID", false);
            SetColumnVisibility("Plate Number", true);
            SetColumnVisibility("Last Name", true);
            SetColumnVisibility("Sex", false);
            SetColumnVisibility("Res House No.", false);
            SetColumnVisibility("Relation", false);
            SetColumnVisibility("Purpose", false);

            SetColumnVisLogVisibility("VisLogID", false);
            SetColumnVisLogVisibility("VisID", false);
            SetColumnVisLogVisibility("ResID", false);
            SetColumnVisLogVisibility("UserID", false);

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


            FILLVISDGV();
            FILLVISLOGDGV();
            cboPurpose.SelectedIndexChanged += CboPurpose_SelectedIndexChanged;
        }

        //i cclear nya yung mga textboxes pag nag click ng clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHouseno.Clear();
            txtPlateno.Clear();
            txtVisfname.Clear();
            txtVislname.Clear();

            cboRelation.SelectedIndex = -1;
            cboPurpose.SelectedIndex = -1;

            datetimeEntrance.Value = DateTime.Now;


        }

        //oag na click tong button na to, kukunin nya lahat ng data sa textboxes, combo boxes at date time picker tas chechek nya kung may blank, pag may blank mag eerror message
        private void btnAddvisitor_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVisfname.Text) || string.IsNullOrEmpty(txtVislname.Text) || string.IsNullOrEmpty(cboSex.Text) ||
                string.IsNullOrEmpty(cboRelation.Text) || string.IsNullOrEmpty(cboPurpose.Text))
            {
                MessageBox.Show("Please fill out all required information!", "Error");
                return;
            }

            //check kung valid yung house number
            if (!IsHouseNumberValid(txtHouseno.Text))
            {
                MessageBox.Show("Invalid House Number! Please enter a valid house number.", "Error");
                return;
            }

            string plateNumber = chkVehicle.Checked ? txtPlateno.Text : "N/A";

            string checkExistingQuery = "SELECT VisID FROM subdivisionvisitortracking.visitorinfo WHERE `Plate Number` = '" + plateNumber + "' AND `First Name` = '" + txtVisfname.Text + "' AND `Last Name` = '" + txtVislname.Text + "' AND `Exit Date Time` IS NULL";

            try
            {
                using (OdbcCommand checkCommand = new OdbcCommand(checkExistingQuery, con))
                {
                    con.Open();
                    object result = checkCommand.ExecuteScalar();

                    if (result != null)
                    {
                        int existingVisID = Convert.ToInt32(result);
                        string updateQuery = "UPDATE subdivisionvisitortracking.visitorinfo SET `Entrance Date Time` = NOW() WHERE VisID = " + existingVisID;

                        using (OdbcCommand updateCommand = new OdbcCommand(updateQuery, con))
                        {
                            updateCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Visitor information updated successfully!");
                    }
                    else
                    {
                        string purpose = cboPurpose.Text;

                        if (purpose.Equals("others", StringComparison.OrdinalIgnoreCase))
                        {
                            purpose = txtPurposespecify.Text;
                        }

                        string insertQuery = "INSERT INTO subdivisionvisitortracking.visitorinfo " +
                        "(`Plate Number`, `First Name`, `Last Name`, Sex, `ResHouseNo.`, Relation, Purpose, `Entrance Date Time`) " +
                        "VALUES('" + plateNumber + "', '" + txtVisfname.Text + "', '" + txtVislname.Text + "', '" + cboSex.Text + "', " +
                        "'" + txtHouseno.Text + "', '" + cboRelation.Text + "', '" + purpose + "', NOW())";


                        using (OdbcCommand insertCommand = new OdbcCommand(insertQuery, con))
                        {
                            insertCommand.ExecuteNonQuery();
                        }

                        MessageBox.Show("Visitor information saved successfully!");
                    }
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

            FILLVISDGV();
            FILLVISLOGDGV();
            VisDataGridView.Refresh();
            btnClear_Click(sender, e);
        }

        //method para i hide or show yung piling column
        private void SetColumnVisibility(string columnName, bool isVisible)
        {
            if (VisDataGridView.Columns.Contains(columnName))
            {
                VisDataGridView.Columns[columnName].Visible = isVisible;
            }
        }
        
        //method para i hide or show yung piling column
        private void SetColumnVisLogVisibility(string columnName, bool isVisible)
        {
            if (visitorlogDataGridView.Columns.Contains(columnName))
            {
                visitorlogDataGridView.Columns[columnName].Visible = isVisible;
            }
        }

        //method para mag add ng button column sa visitor info data grid view
        private void AddActionButtonColumn()
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Exit";
            btnColumn.Name = "btnAction";
            btnColumn.Text = "Exit";
            btnColumn.UseColumnTextForButtonValue = true;
            VisDataGridView.Columns.Add(btnColumn);
        }

        //method para ma move yung visitor info sa visitor log kung naka exit na yung visitor
        private void MoveToVisitorLog(int visitorId)
        {
            using (OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
            {
                con.Open();

                string selectQuery = "SELECT * FROM subdivisionvisitortracking.visitorinfo WHERE VisID = " + visitorId;

                using (OdbcCommand selectCommand = new OdbcCommand(selectQuery, con))
                {
                    using (OdbcDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int visId = Convert.ToInt32(reader["VisID"]);
                            string plateNumber = reader["Plate Number"].ToString();
                            DateTime entranceDateTime = Convert.ToDateTime(reader["Entrance Date Time"]);

                            string houseNo = reader["ResHouseNo."].ToString();
                            int resId = GetResIdByHouseNo(houseNo);

                            if (resId != -1)
                            {
                                int userId = Convert.ToInt32(UserSession.UserID);
                                DateTime exitDateTime = DateTime.Now;
                                string insertQuery = "INSERT INTO subdivisionvisitortracking.visitorlog (VisID, ResID, UserID, PlateNumber, EntranceDateTime, ExitDateTime) " +
                       "VALUES (" + visId + ", " + resId + ", " + userId + ", '" + plateNumber + "', '" + entranceDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + exitDateTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                                using (OdbcCommand insertCommand = new OdbcCommand(insertQuery, con))
                                {
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                MessageBox.Show("ResID for HouseNo '" + houseNo + "' not found.", "Error");
                            }
                        }
                    }
                }
            }
        }

        //cell click event para sa button column sa visitor info data grid view para ma exit yung visitor pag na click yung button na exit sa data grid view na to 
        private void VisDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == VisDataGridView.Columns["btnAction"].Index && e.RowIndex >= 0)
            {
                string exitDateTime = VisDataGridView.Rows[e.RowIndex].Cells["Exit Date Time"].Value.ToString();

                if (string.IsNullOrEmpty(exitDateTime))
                {
                    string firstName = VisDataGridView.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
                    string lastName = VisDataGridView.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();
                    string plateNumber = VisDataGridView.Rows[e.RowIndex].Cells["Plate Number"].Value.ToString();

                    DialogResult result = MessageBox.Show("Is visitor " + firstName + " " + lastName + " with Plate Number " + plateNumber + " really leaving?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                    if (result == DialogResult.Yes)
                    {
                        int visitorId = Convert.ToInt32(VisDataGridView.Rows[e.RowIndex].Cells["VisID"].Value);
                        string updateQuery = "UPDATE subdivisionvisitortracking.visitorinfo SET `Exit Date Time` = NOW() WHERE VisID = " + visitorId;

                        try
                        {
                            using (OdbcCommand command = new OdbcCommand(updateQuery, con))
                            {
                                con.Open();
                                command.ExecuteNonQuery();
                            }

                            MoveToVisitorLog(visitorId);

                            FILLVISDGV();
                            FILLVISLOGDGV();
                            MessageBox.Show("Exit Date Time updated for " + firstName + " " + lastName + " (Plate Number: " + plateNumber + ") and moved to visitorlog");

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
                    }
                }
                else
                {
                    MessageBox.Show("Exit Date Time is already set for this visitor.");
                }
            }
        }

        //pag na click to i eexport to csv yung laman nung data grid view at database na visitor log
        private void btnExportcsv_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.Title = "Export CSV File";

            saveFileDialog.FileName = DateTime.Now.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture) + DateTime.Now.Year + ".csv";

            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    for (int i = 0; i < visitorlogDataGridView.Columns.Count; i++)
                    {
                        streamWriter.Write(visitorlogDataGridView.Columns[i].HeaderText);
                        if (i < visitorlogDataGridView.Columns.Count - 1)
                            streamWriter.Write(",");
                    }
                    streamWriter.WriteLine();

                    for (int i = 0; i < visitorlogDataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < visitorlogDataGridView.Columns.Count; j++)
                        {
                            streamWriter.Write(visitorlogDataGridView.Rows[i].Cells[j].Value);
                            if (j < visitorlogDataGridView.Columns.Count - 1)
                                streamWriter.Write(",");
                        }
                        streamWriter.WriteLine();
                    }
                }
            }
        }

        //i ffill nya ng data yung data grid view na visitor info galing sa visitor into table sa database
        private void FILLVISDGV()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            con.Open();

            string dataQuery = "SELECT * FROM visitorinfo WHERE `Exit Date Time` IS NULL";
            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(dataQuery, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            VisDataGridView.DataSource = dt;
            con.Close();
        }

        //ganun rin pero dito nag join ng first name, lastname, purpose at relation sa visitorinfo table tapos yung iba galing sa visitorlog table
        private void FILLVISLOGDGV()
        {
            con.Open();

            string dataQuery = "SELECT vl.VisLogID, vl.VisID, vl.ResID, vl.UserID, vl.PlateNumber, vl.EntranceDateTime, vl.ExitDateTime, " +
                               "vi.`First Name`, vi.`Last Name`, vi.Relation, vi.Purpose " +
                               "FROM subdivisionvisitortracking.visitorlog vl " +
                               "JOIN subdivisionvisitortracking.visitorinfo vi ON vl.VisID = vi.VisID";

            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(dataQuery, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                int visId = Convert.ToInt32(row["VisID"]);

                string selectVisitorInfoQuery = "SELECT `First Name`, `Last Name`, `Relation`, `Purpose` FROM subdivisionvisitortracking.visitorinfo WHERE VisID = " + visId;

                using (OdbcCommand selectCommand = new OdbcCommand(selectVisitorInfoQuery, con))
                {
                    using (OdbcDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            row["First Name"] = reader["First Name"].ToString();
                            row["Last Name"] = reader["Last Name"].ToString();
                            row["Relation"] = reader["Relation"].ToString();
                            row["Purpose"] = reader["Purpose"].ToString();
                        }
                    }
                }
            }

            visitorlogDataGridView.DataSource = dt;

            con.Close();
        }
        //nakatago buttons pag guard yung naka login
        private void AdjustButtonVisibility()
        {
            if (UserSession.UserType == "User(Guard)")
            {
                btnUsers.Visible = false;
                btnExportcsv.Visible = false;
            }
            else
            {
                btnUsers.Visible = true;
            }

            if (UserSession.UserType == "Admin")
            {
                tabPage1.Visible = false;
            }
            else
            {
                tabPage1.Visible = true;
            }
        }

        
        private void txtVisfname_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isFirstLetterTyped = false;

        //para hindi makatype ng number sa first name
        private void txtVisfname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            else if (char.IsLetter(e.KeyChar) && !isFirstLetterTyped)
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
                isFirstLetterTyped = true;
            }

        }

        //para hindi makatype ng number sa last name
        private void txtVislname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

        }

        //para naka uppercase yung mga letters sa plate number
        private void txtPlateno_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtPlateno.Text;

            string cleanedText = new string(inputText.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());

            string uppercasedText = cleanedText.ToUpper();

            txtPlateno.Text = uppercasedText;

            txtPlateno.SelectionStart = txtPlateno.Text.Length;
        }

        //minimize button
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtHouseno_TextChanged(object sender, EventArgs e)
        {

        }

        //check kung valid yung house number galing sa reshouseno column sa resinfo table
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

        //para walang letter sa house number
        private void txtHouseno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        //kukuha yung res id galing sa resinfo table gamit yung house number
        private int GetResIdByHouseNo(string houseNo)
        {
            int resId = -1;

            try
            {
                using (OdbcConnection con = new OdbcConnection("Driver={MySQL ODBC 8.0 ANSI Driver};Server=localhost;Database=subdivisionvisitortracking;User=root;Password=;"))
                {
                    con.Open();

                    string selectQuery = "SELECT ResID FROM resinfo WHERE ResHouseNo = '" + houseNo + "'";

                    using (OdbcCommand command = new OdbcCommand(selectQuery, con))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            resId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving ResID: " + ex.Message, "Error");
            }

            return resId;
        }

        //double click sa row header ng visitor info data grid view para ma edit yung visitor info
        private void VisDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < VisDataGridView.Rows.Count)
            {
                int visId = Convert.ToInt32(VisDataGridView.Rows[e.RowIndex].Cells["VisID"].Value);
                string plateNumber = VisDataGridView.Rows[e.RowIndex].Cells["Plate Number"].Value.ToString();
                string firstName = VisDataGridView.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
                string lastName = VisDataGridView.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();
                string sex = VisDataGridView.Rows[e.RowIndex].Cells["Sex"].Value.ToString();
                string houseNo = VisDataGridView.Rows[e.RowIndex].Cells["ResHouseNo."].Value.ToString();
                string relation = VisDataGridView.Rows[e.RowIndex].Cells["Relation"].Value.ToString();
                string purpose = VisDataGridView.Rows[e.RowIndex].Cells["Purpose"].Value.ToString();
                DateTime entranceDateTime = Convert.ToDateTime(VisDataGridView.Rows[e.RowIndex].Cells["Entrance Date Time"].Value);
                string exitDateTime = VisDataGridView.Rows[e.RowIndex].Cells["Exit Date Time"].Value.ToString();

                EditActiveVisitor frm = new EditActiveVisitor();

                frm.SelectedVisId = visId;
                frm.SelectedPlateNumber = plateNumber;
                frm.SelectedFirstName = firstName;
                frm.SelectedLastName = lastName;
                frm.SelectedSex = sex;
                frm.SelectedHouseNo = houseNo;
                frm.SelectedRelation = relation;
                frm.SelectedPurpose = purpose;
                frm.SelectedEntranceDateTime = entranceDateTime;
                frm.SelectedExitDateTime = string.IsNullOrEmpty(exitDateTime) ? null : (DateTime?)Convert.ToDateTime(exitDateTime);

                frm.Show();
            }
        }

        //lalabas yung analytics form pag na click yung button na analytics
        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            this.Hide();
            Analytics frm1 = new Analytics();
            frm1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //pag others nakalagay visible yung textbox at label na specify, pero pag hindi others nakalagay invisible yung textbox at label na specify
        private void CboPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.Equals(cboPurpose.SelectedItem?.ToString(), "others", StringComparison.OrdinalIgnoreCase))
            {

                txtPurposespecify.Visible = true;
                lblSpecify.Visible = true;
            }
            else
            {
                txtPurposespecify.Visible = false;
                lblSpecify.Visible = false;
            }
        }

        private void VisDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVisitors_Click(object sender, EventArgs e)
        {
            FILLVISDGV();

        }

        //pag click ng button na users lalabas yung form na users
        private void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            Users frm1 = new Users();
            frm1.Show();
        }

        private void visitorlogDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        //seach button para sa visitor log data grid view
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtValuetoSearch.Text;
            searchData(searchText);
        }

        //search data na nasa visitor log data grid view at database
        private void searchData(string searchText)
        {
            con.Open();

            string dataQuery = "SELECT * FROM visitorinfo WHERE `Exit Date Time` IS NOT NULL";

            if (!string.IsNullOrEmpty(searchText))
            {
                dataQuery += " AND (`First Name` LIKE '%" + searchText + "%' OR `Last Name` LIKE '%" + searchText + "%' OR `Purpose` LIKE '%" + searchText + "%' OR `Relation` LIKE '%" + searchText + "%' OR `Plate Number` LIKE '%" + searchText + "%')";
            }

            OdbcDataAdapter dataAdapter = new OdbcDataAdapter(dataQuery, con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            visitorlogDataGridView.DataSource = dt;

            con.Close();
        }

        //double click sa row header ng visitor log data grid view para ma revisit yung visitor, lalabas yung revisit form tapos i papasa nya yung data na yun sa revisit form
        private void visitorlogDataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < visitorlogDataGridView.Rows.Count)
            {
                int visId = Convert.ToInt32(visitorlogDataGridView.Rows[e.RowIndex].Cells["VisID"].Value);
                string plateNumber = visitorlogDataGridView.Rows[e.RowIndex].Cells["Plate Number"].Value.ToString();
                string firstName = visitorlogDataGridView.Rows[e.RowIndex].Cells["First Name"].Value.ToString();
                string lastName = visitorlogDataGridView.Rows[e.RowIndex].Cells["Last Name"].Value.ToString();

                string selectVisitorInfoQuery = "SELECT * FROM subdivisionvisitortracking.visitorinfo WHERE VisID = " + visId;

                using (OdbcCommand selectCommand = new OdbcCommand(selectVisitorInfoQuery, con))
                {
                    con.Open();

                    using (OdbcDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string sex = reader["Sex"].ToString();
                            string houseNo = reader["ResHouseNo."].ToString();
                            string relation = reader["Relation"].ToString();
                            string purpose = reader["Purpose"].ToString();
                            DateTime entranceDateTime = Convert.ToDateTime(reader["Entrance Date Time"]);
                            string exitDateTime = reader["Exit Date Time"].ToString();

                            Revisit frm = new Revisit();

                            frm.SelectedVisId = visId;
                            frm.SelectedPlateNumber = plateNumber;
                            frm.SelectedFirstName = firstName;
                            frm.SelectedLastName = lastName;
                            frm.SelectedSex = sex;
                            frm.SelectedHouseNo = houseNo;
                            frm.SelectedRelation = relation;
                            frm.SelectedPurpose = purpose;
                            frm.SelectedEntranceDateTime = entranceDateTime;
                            frm.SelectedExitDateTime = string.IsNullOrEmpty(exitDateTime) ? null : (DateTime?)Convert.ToDateTime(exitDateTime);

                            frm.Show();
                        }
                    }
                }

                con.Close();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
