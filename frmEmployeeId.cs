using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjWinRemax
{
    public partial class frmEmployeeId : Form
    {
        public frmEmployeeId()
        {
            InitializeComponent();
        }
        DataTable tabEmployee;
        private void frmEmployeeId_Load(object sender, EventArgs e)
        {
            tabEmployee = clsGlobal.mySet.Tables["Employees"];
            var allEmployee = from comp in tabEmployee.AsEnumerable()
                              select new
                              {
                                  RefEmp = comp.Field<int>("Id")
                              };

            lstEmployee.DataSource = allEmployee.ToList();
            lstEmployee.ValueMember = "RefEmp";
            lstEmployee.SelectedIndex = -1;
        }

        private void Find_Click(object sender, EventArgs e)
        {
            tabEmployee = clsGlobal.mySet.Tables["Employees"];
            if (txtId.Text == "")
            {
                MessageBox.Show("Please Insert Id in the box!!!! \n You can see the list of Idies at topright side");
            }
            else
            {
                int refmp = Convert.ToInt16(txtId.Text);


                var selEmp = from emp in tabEmployee.AsEnumerable()
                               where emp.Field<int>("Id") == refmp
                               select emp;

                txtName.Text = selEmp.ElementAt(0)["FullName"].ToString();
                txtPhone.Text = selEmp.ElementAt(0)["Phone"].ToString();
                txtEmail.Text = selEmp.ElementAt(0)["Email"].ToString();

            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

