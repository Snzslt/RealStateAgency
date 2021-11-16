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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabEmployee,tabClient,tabAgency;

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabEmployee;
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void showToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabClient;
        }

        private void showToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabAgency;
        }

        private void homePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMain mn = new frmMain();
            mn.Show();
            this.Close();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmployee emp = new frmEmployee();
            emp.Show();
            
        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmClient cl = new frmClient();
            cl.Show();
           
        }

        private void agenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmAgency cl = new frmAgency();
            cl.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHouse hs = new frmHouse();
            hs.Show();
            
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["Houses"];
            tabEmployee = clsGlobal.mySet.Tables["Employees"];
            tabClient = clsGlobal.mySet.Tables["Clients"];
            tabAgency = clsGlobal.mySet.Tables["Agencies"];

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tabHouse;
                }
    }
}
