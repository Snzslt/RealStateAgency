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
    public partial class AgentClient : Form
    {
        public AgentClient()
        {
            InitializeComponent();
        }
        DataTable tabEmployee, tabClient,tabcl;
        int currentIndex;
        string mode;

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
            if (currentIndex < (tabClient.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabClient.Rows.Count - 1;
            Display();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtName.Text = txtGender.Text = txtPhone.Text = txtEmail.Text = txtRole.Text = "";
            datBirthdate.ResetText();
            cboEmployee.Text = "";
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
                currentRow = tabClient.NewRow();
                currentRow["FullName"] = txtName.Text;
                currentRow["Role"] = txtRole.Text;
                currentRow["Gender"] = txtGender.Text;

                currentRow["PhoneNumber"] = Convert.ToDecimal(txtPhone.Text);
                currentRow["Email"] = txtEmail.Text;
                currentRow["BirthDate"] = datBirthdate.Value;


                foreach (DataRow myrow in tabEmployee.Rows)
                {
                    if (myrow["FullName"].ToString() == cboEmployee.SelectedItem.ToString())
                    {
                        currentRow["RefAgent"] = myrow["Id"];
                    }
                }
                tabClient.Rows.Add(currentRow);
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                clsGlobal.adpClient.Update(clsGlobal.mySet, "Clients");
                // Refresh the datatable with the content of the database in case of changes
                clsGlobal.mySet.Tables.Remove("Clients");
                clsGlobal.adpClient.Fill(clsGlobal.mySet, "Clients");

                tabClient = clsGlobal.mySet.Tables["Clients"];
                currentIndex = tabEmployee.Rows.Count - 1;

            }
            else if (mode == "edit")
            {
                currentRow = tabClient.Rows[currentIndex];
                currentRow["FullName"] = txtName.Text;
                currentRow["Role"] = txtRole.Text;
                currentRow["Gender"] = txtGender.Text;

                currentRow["PhoneNumber"] = Convert.ToDecimal(txtPhone.Text);
                currentRow["Email"] = txtEmail.Text;
                currentRow["BirthDate"] = datBirthdate.Value;


                foreach (DataRow myrow in tabClient.Rows)
                {
                    if (myrow["FullName"].ToString() == cboEmployee.SelectedItem.ToString())
                    {
                        currentRow["RefAgent"] = myrow["Id"];
                    }

                    SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                    clsGlobal.adpClient.Update(clsGlobal.mySet, "Clients");

                    clsGlobal.mySet.Tables.Remove("Clients");
                    clsGlobal.adpClient.Fill(clsGlobal.mySet, "Clients");

                    tabClient = clsGlobal.mySet.Tables["Clients"];
                }

            }
            mode = "";
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this Client ?", "Deletion Warning",
          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                tabClient.Rows[currentIndex].Delete();


                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpClient);
                clsGlobal.adpClient.Update(clsGlobal.mySet, "Clients");

                clsGlobal.mySet.Tables.Remove("Clients");
                clsGlobal.adpClient.Fill(clsGlobal.mySet, "Clients");

                tabClient = clsGlobal.mySet.Tables["Clients"];
                currentIndex = 0;
                Display();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AgentClient_Load(object sender, EventArgs e)

        {
            tabClient = clsGlobal.mySet.Tables["Clients"];
            
            tabEmployee = clsGlobal.mySet.Tables["Employees"];
            foreach (DataRow myrow in tabClient.Rows)
            {
                if (myrow["RefAgent"].ToString() == "1") {
                   
                }
            }


            foreach (DataRow myrow in tabClient.Rows)
            {
                cboEmployee.Items.Add(myrow["FullName"].ToString());
            }

            currentIndex = 0;
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        }
        private void Display()
        {
           
            datBirthdate.Value = Convert.ToDateTime(tabClient.Rows[currentIndex]["BirthDate"]);
            txtName.Text = tabClient.Rows[currentIndex]["FullName"].ToString();
            txtPhone.Text = tabClient.Rows[currentIndex]["PhoneNumber"].ToString();
            txtEmail.Text = tabClient.Rows[currentIndex]["Email"].ToString();
            txtGender.Text = tabClient.Rows[currentIndex]["Gender"].ToString();


            txtRole.Text = tabClient.Rows[currentIndex]["Role"].ToString();
           
            lblInfo.Text = "Client " + (currentIndex + 1) + " on a total of " + tabClient.Rows.Count;

            DataColumn[] keys = new DataColumn[1];
            keys[0] = tabEmployee.Columns["Id"];
            tabEmployee.PrimaryKey = keys;

            DataRow myrow = tabEmployee.Rows.Find(tabClient.Rows[currentIndex]["RefAgent"]);
            cboEmployee.Text = myrow["FullName"].ToString();



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
