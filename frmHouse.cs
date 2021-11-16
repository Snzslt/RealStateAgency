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
    public partial class frmHouse : Form
    {
        public frmHouse()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabSeller;
        int currentIndex;
        string mode;
        private void txtRoom_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void la_Click(object sender, EventArgs e)
        {

        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cboAgent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmHouse_Load(object sender, EventArgs e)
        {

            tabSeller = clsGlobal.mySet.Tables["Clients"];
            tabHouse = clsGlobal.mySet.Tables["Houses"];
            var allSellerNames = from seller in tabSeller.AsEnumerable()
                                 where seller.Field<String>("Role") == "Seller"
                                 select new
                               {
                                   SellerName = seller.Field<string>("FullName"),
                                   RefSeller = seller.Field<int>("Id")
                               };
            cboSeller.DataSource = allSellerNames.ToList();
            cboSeller.ValueMember = "RefSeller";
            cboSeller.DisplayMember = "SellerName";
           
           
            currentIndex = 0;
            Display();
           
            ActivateButtons(true, true, true, false, false, true, true);

        }
        private void Display()
        {
            
            
            txtAddress.Text = tabHouse.Rows[currentIndex]["Address"].ToString();
            txtBath.Text = tabHouse.Rows[currentIndex]["NBaths"].ToString();
            txtRoom.Text = tabHouse.Rows[currentIndex]["NRooms"].ToString();
            txtSize.Text = tabHouse.Rows[currentIndex]["Size"].ToString();
            txtType.Text = tabHouse.Rows[currentIndex]["Type"].ToString();
            txtStatus.Text = tabHouse.Rows[currentIndex]["Status"].ToString();
            txtPrice.Text = tabHouse.Rows[currentIndex]["Price"].ToString();
            txtCity.Text = tabHouse.Rows[currentIndex]["City"].ToString();
            // lblinfo display the current record number on the total
            lblInfo.Text = "House " + (currentIndex + 1) + " on a total of " + tabHouse.Rows.Count;

            DataColumn[] keys = new DataColumn[1];
            keys[0] = tabSeller.Columns["Id"];
            tabSeller.PrimaryKey = keys;

            DataRow myrow = tabSeller.Rows.Find(tabHouse.Rows[currentIndex]["RefSeller"]);
            cboSeller.Text = myrow["FullName"].ToString();

            

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            Display();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabHouse.Rows.Count - 1;
            Display();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < (tabHouse.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                Display();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            mode = "add";
            txtAddress.Text = txtBath.Text = txtRoom.Text = txtSize.Text =txtType.Text=txtStatus.Text=txtPrice.Text=txtCity.Text= txtStatus.Text= "";
          
            cboSeller.Text = "";
            txtAddress.Focus();
            lblInfo.Text = "--- ADDING MODE ---";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode = "edit";
            txtAddress.Focus();
            lblInfo.Text = "--- EDITING MODE ---";
            ActivateButtons(false, false, false, true, true, false, false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow currentRow;
            if (mode == "add")
            {
                currentRow = tabHouse.NewRow();
                currentRow["Address"] = txtAddress.Text;
                currentRow["City"] = txtCity.Text;
                currentRow["NBaths"] = Convert.ToInt16(txtBath.Text);
                currentRow["NRooms"] = Convert.ToInt16(txtRoom.Text);
                currentRow["Size"] = Convert.ToInt16(txtSize.Text);
                currentRow["Type"] = txtType.Text;
                currentRow["Status"] = txtStatus.Text;
                currentRow["Price"] = Convert.ToDecimal(txtPrice.Text);
                currentRow["RefSeller"] = cboSeller.SelectedValue;
                tabHouse.Rows.Add(currentRow);


                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                clsGlobal.adpHouse.Update(clsGlobal.mySet, "Houses");

                clsGlobal.mySet.Tables.Remove("Houses");
                clsGlobal.adpHouse.Fill(clsGlobal.mySet, "Houses");

                tabHouse = clsGlobal.mySet.Tables["Houses"];
                currentIndex = tabHouse.Rows.Count - 1;
            }
            else if (mode == "edit")
            {
                currentRow = tabHouse.Rows[currentIndex];
                currentRow["Address"] = txtAddress.Text;
                currentRow["City"] = txtCity.Text;
                currentRow["NBaths"] = Convert.ToInt16(txtBath.Text);
                currentRow["NRooms"] = Convert.ToInt16(txtRoom.Text);
                currentRow["Size"] = Convert.ToInt16(txtSize.Text);
                currentRow["Type"] = txtType.Text;
                currentRow["Status"] = txtStatus.Text;
                currentRow["Price"] = Convert.ToDecimal(txtPrice.Text);

                 currentRow["RefSeller"] = cboSeller.SelectedValue; 

                
               
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                clsGlobal.adpHouse.Update(clsGlobal.mySet, "Houses");
              
                clsGlobal.mySet.Tables.Remove("Houses");
                clsGlobal.adpHouse.Fill(clsGlobal.mySet, "Houses");

                tabHouse = clsGlobal.mySet.Tables["Houses"];
            }
            mode = "";
            Display();
            ActivateButtons(true, true, true, false, false, true, true);
        
    }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete this House ?", "Deletion Warning",
             MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
               
                tabHouse.Rows[currentIndex].Delete();

              
                SqlCommandBuilder myBuilder = new SqlCommandBuilder(clsGlobal.adpHouse);
                clsGlobal.adpHouse.Update(clsGlobal.mySet, "Houses");
                
                clsGlobal.mySet.Tables.Remove("Houses");
                clsGlobal.adpHouse.Fill(clsGlobal.mySet, "Houses");

                tabHouse = clsGlobal.mySet.Tables["Houses"];
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
