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
    public partial class frmEmployeeSearch : Form
    {
        public frmEmployeeSearch()
        {
            InitializeComponent();
        }
        DataTable tabAgency, tabEmployee;
       
        int currentIndex;
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < (tabEmployee.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void frmEmployeeSearch_Load(object sender, EventArgs e)
        {

            tabAgency = clsGlobal.mySet.Tables["Agencies"];
            tabEmployee = clsGlobal.mySet.Tables["Employees"];

            

            currentIndex = 0;
            Display();
           
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            Display();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabEmployee.Rows.Count - 1;
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

        private void Display()
        {

           
            txtName.Text = tabEmployee.Rows[currentIndex]["FullName"].ToString();
            txtPhone.Text = tabEmployee.Rows[currentIndex]["Phone"].ToString();
            txtEmail.Text = tabEmployee.Rows[currentIndex]["Email"].ToString();
           

            lblInfo.Text = "Employee " + (currentIndex + 1) + " on a total of " + tabEmployee.Rows.Count;

        }
    }
}
