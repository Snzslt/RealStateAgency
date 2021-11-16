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
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
        }
        DataTable tabEmployee, tabAgency;
        int currentIndex;
        string mode;
        private void datBirthdate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void la_Click(object sender, EventArgs e)
        {

        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtGender_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboAgent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
           

            tabAgency = clsGlobal.mySet.Tables["Agencies"];
            tabEmployee = clsGlobal.mySet.Tables["Employees"];

            foreach (DataRow myrow in tabAgency.Rows)
            {
                cboAgent.Items.Add(myrow["Name"].ToString());
            }

            currentIndex = 0;
            Display();
            ActivateButtons(true, true, true, false, false, true, true);

        }
        private void Display()
        {
          
            datBirthdate.Value = Convert.ToDateTime(tabEmployee.Rows[currentIndex]["BirthDate"]);
            txtName.Text = tabEmployee.Rows[currentIndex]["FullName"].ToString();
            txtPhone.Text = tabEmployee.Rows[currentIndex]["Phone"].ToString();
            txtEmail.Text = tabEmployee.Rows[currentIndex]["Email"].ToString();
            txtGender.Text = tabEmployee.Rows[currentIndex]["Gender"].ToString();
            txtSalary.Text = tabEmployee.Rows[currentIndex]["Salary"].ToString();
            txtPassword.Text = tabEmployee.Rows[currentIndex]["Password"].ToString();
            txtRole.Text = tabEmployee.Rows[currentIndex]["Role"].ToString();
            
            lblInfo.Text = "Employee " + (currentIndex + 1) + " on a total of " + tabEmployee.Rows.Count;

            DataColumn[] keys = new DataColumn[1];
            keys[0] = tabAgency.Columns["Id"];
            tabAgency.PrimaryKey = keys;

            DataRow myrow = tabAgency.Rows.Find(tabEmployee.Rows[currentIndex]["RefAgency"]);
            cboAgent.Text = myrow["Name"].ToString();


            lblInfo.Text = "Employee " + (currentIndex + 1) + " on a total of " + tabEmployee.Rows.Count;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            Display();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

            if (currentIndex > 0)
            {
                currentIndex--;
                Display();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < (tabEmployee.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabEmployee.Rows.Count - 1;
            Display();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtGender.Text =txtSalary.Text= txtPhone.Text = txtEmail.Text = txtRole.Text=txtPassword.Text=  "";
            datBirthdate.ResetText();
            cboAgent.Text = "";
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
                currentRow = tabEmployee.NewRow();
                currentRow["FullName"] = txtName.Text;
                currentRow["Role"] = txtRole.Text;
                currentRow["Gender"] = txtGender.Text;
                currentRow["Salary"] = Convert.ToDecimal(txtSalary.Text);
                currentRow["Phone"] = Convert.ToDecimal(txtPhone.Text);
                currentRow["Email"] = txtEmail.Text;
                currentRow["BirthDate"] = datBirthdate.Value;
                currentRow["Password"] = txtPassword.Text;

                foreach (DataRow myrow in tabAgency.Rows)
                {
                    if (myrow["Name"].ToString() == cboAgent.SelectedItem.ToString())
                    {
                        currentRow["RefAgency"] = myrow["Id"];
                    }
                }
                tabEmployee.Rows.Add(currentRow);
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpEmployee);
                clsGlobal.adpEmployee.Update(clsGlobal.mySet, "Employees");
                // Refresh the datatable with the content of the database in case of changes
                clsGlobal.mySet.Tables.Remove("Employees");
                clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employees");

                tabEmployee = clsGlobal.mySet.Tables["Employees"];
                currentIndex = tabEmployee.Rows.Count - 1;

            }
            else if (mode == "edit")
            {
                currentRow = tabEmployee.Rows[currentIndex];
                currentRow["FullName"] = txtName.Text;
                currentRow["Role"] = txtRole.Text;
                currentRow["Gender"] = txtGender.Text;
                currentRow["Salary"] = Convert.ToDecimal(txtSalary.Text);
                currentRow["Phone"] = Convert.ToDecimal(txtPhone.Text);
                currentRow["Email"] = txtEmail.Text;
                currentRow["BirthDate"] = datBirthdate.Value;
                currentRow["Password"] = txtPassword.Text;
              
                foreach (DataRow myrow in tabAgency.Rows)
                {
                    if (myrow["Name"].ToString() == cboAgent.SelectedItem.ToString())
                    {
                        currentRow["RefAgency"] = myrow["Id"];
                    }
                }
                // Save (or synchronize) the contents of the myset.tabels["Employees"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpEmployee);
                clsGlobal.adpEmployee.Update(clsGlobal.mySet, "Employees");
                // Refresh the datatable with the content of the database in case of changes
                clsGlobal.mySet.Tables.Remove("Employees");
                clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employees");

                tabEmployee = clsGlobal.mySet.Tables["Employees"];
            }
            mode = "";
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this Employee ?", "Deletion Warning",
              MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                // Delete the record(or datarow) positioned at the currentindex
                tabEmployee.Rows[currentIndex].Delete();

                // Save (or synchronize) the contents of the myset.tabels["Companies"] to the database
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpEmployee);
                clsGlobal.adpEmployee.Update(clsGlobal.mySet, "Employees");
                // Refresh the datatable with the content of the database in case of changes
                clsGlobal.mySet.Tables.Remove("Employees");
                clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employees");

                tabEmployee = clsGlobal.mySet.Tables["Employees"];
                currentIndex = 0;
                Display();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
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

    }
}
