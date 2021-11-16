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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        DataTable tabEmployee;
        List<string> valid = new List<string>();


        private void frmLogin_Load(object sender, EventArgs e)
        {
          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Int32 chk = 0;
            clsGlobal.Id = Convert.ToInt32(txtPass.Text);
            clsGlobal.Pass = Convert.ToInt32(txtPass.Text);
            tabEmployee = clsGlobal.mySet.Tables["Employees"];
            foreach (DataRow myRow in tabEmployee.Rows)
            {    if (myRow["Id"].ToString() == txtId.Text && myRow["Password"].ToString() == txtPass.Text&& myRow["Role"].ToString()=="Admin")
                {
                    chk = 1;
                }
               else if (myRow["Id"].ToString() == txtId.Text && myRow["Password"].ToString() == txtPass.Text && myRow["Role"].ToString() == "Agent")
                {
                    chk = 2;
                }

            }
            if(chk == 1)
            {
                frmAdmin ad = new frmAdmin();
                ad.Show();
                this.Close();
            }
            else if (chk == 2)
            {
                frmAgent ad = new frmAgent();
                ad.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong User Name Or PassWord Try Again!!!!!!");
                txtId.Text = txtPass.Text = "";
                txtId.Focus();
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmMain mn = new frmMain();
            mn.Show();
            this.Close();
        }
    }
}
