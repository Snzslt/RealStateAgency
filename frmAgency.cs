using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PrjWinRemax
{
    public partial class frmAgency : Form
    {
        public frmAgency()
        {
            InitializeComponent();
        }
        // form global variables
        DataTable tabAgency;
        int currentIndex;
        string mode;
        private void frmAgency_Load(object sender, EventArgs e)
        {
            tabAgency = clsGlobal.mySet.Tables["Agencies"];
            currentIndex = 0;
            DisplayDatarow2Txt();
            ActivateButtons(true, true, true, false, false, true, true);
        }
        private void DisplayDatarow2Txt()
        {
           
            txtName.Text = tabAgency.Rows[currentIndex]["Name"].ToString();
            txtNumber.Text = tabAgency.Rows[currentIndex]["Id"].ToString();
            txtLocation.Text = tabAgency.Rows[currentIndex]["Location"].ToString();
            txtPhone.Text = tabAgency.Rows[currentIndex]["PhoneNumber"].ToString();

           
            lblInfo.Text = "Agency " + (currentIndex + 1) + " on a total of " + tabAgency.Rows.Count;
        }
        private void ActivateButtons(bool Add, bool Edit, bool Del, bool Save, bool Cancel, bool Navigs, bool Close)
        {
            btnAdd.Enabled = Add;
            btnEdit.Enabled = Edit;
            btnDelete.Enabled = Del;
            btnSave.Enabled = Save;
            btnCancel.Enabled = Cancel;
            btnFirst.Enabled = btnPrevious.Enabled = btnNext.Enabled = btnLast.Enabled = Navigs;
            btnClose.Enabled = Close;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            DisplayDatarow2Txt();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayDatarow2Txt();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < (tabAgency.Rows.Count - 1))
            {
                currentIndex++;
                DisplayDatarow2Txt();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {

            currentIndex = tabAgency.Rows.Count - 1;
            DisplayDatarow2Txt();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtLocation.Text = txtNumber.Text = txtPhone.Text = "";

            txtName.Focus();
            lblInfo.Text = "--- ADDING MODE ---";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode = "edit";
            txtName.Focus();
            lblInfo.Text = "--- EDITING MODE ---";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow;
            if (mode == "add")
            {
                currentRow = tabAgency.NewRow();
                currentRow["Name"] = txtName.Text;
                currentRow["PhoneNumber"] = Convert.ToDecimal(txtPhone.Text);
                currentRow["Location"] = txtLocation.Text;
                currentRow["Id"] = Convert.ToInt32(txtNumber.Text);
                tabAgency.Rows.Add(currentRow);

                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpAgency);
                clsGlobal.adpAgency.Update(clsGlobal.mySet, "Agencies");

                clsGlobal.mySet.Tables.Remove("Agencies");
                clsGlobal.adpAgency.Fill(clsGlobal.mySet, "Agencies");

                currentIndex = tabAgency.Rows.Count - 1;
            }

            else if (mode == "edit")
            {
                currentRow = tabAgency.Rows[currentIndex];

                currentRow = tabAgency.NewRow();
                currentRow["Name"] = txtName.Text;
                currentRow["PhoneNumber"] = Convert.ToDecimal(txtPhone.Text);
                currentRow["Location"] = txtLocation.Text;
                currentRow["Id"] = Convert.ToInt32(txtNumber.Text);
                tabAgency.Rows.Add(currentRow);

               
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpAgency);
                clsGlobal.adpAgency.Update(clsGlobal.mySet, "Agencies");
             
                clsGlobal.mySet.Tables.Remove("Agencies");
                clsGlobal.adpAgency.Fill(clsGlobal.mySet, "Agencies");
            }
            mode = "";
            DisplayDatarow2Txt();
            ActivateButtons(true, true, true, false, false, true, true);



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this Agency?", "Deletion Warning",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
               
                tabAgency.Rows[currentIndex].Delete();

               
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpAgency);
                clsGlobal.adpAgency.Update(clsGlobal.mySet, "Agencies");
                
                clsGlobal.mySet.Tables.Remove("Agencies");
                clsGlobal.adpAgency.Fill(clsGlobal.mySet, "Agencies");

                currentIndex = 0;
                DisplayDatarow2Txt();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisplayDatarow2Txt();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
