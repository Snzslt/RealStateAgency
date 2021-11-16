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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Create a global dataset 
            clsGlobal.mySet = new DataSet();

            clsGlobal.myCon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DB_Remax1;Integrated Security=True");
            clsGlobal.myCon.Open();

           
            SqlCommand myCmd = new SqlCommand("SELECT * FROM Agency", clsGlobal.myCon);
            clsGlobal.adpAgency = new SqlDataAdapter(myCmd);
            clsGlobal.adpAgency.Fill(clsGlobal.mySet, "Agencies");

           
             myCmd = new SqlCommand("SELECT * FROM Employees", clsGlobal.myCon);
            clsGlobal.adpEmployee = new SqlDataAdapter(myCmd);
            clsGlobal.adpEmployee.Fill(clsGlobal.mySet, "Employees");

           
            myCmd = new SqlCommand("SELECT * FROM Clients", clsGlobal.myCon);
            clsGlobal.adpClient = new SqlDataAdapter(myCmd);
            clsGlobal.adpClient.Fill(clsGlobal.mySet, "Clients");

            myCmd = new SqlCommand("SELECT * FROM Houses", clsGlobal.myCon);
            clsGlobal.adpHouse = new SqlDataAdapter(myCmd);
            clsGlobal.adpHouse.Fill(clsGlobal.mySet, "Houses");

            myCmd = new SqlCommand("SELECT Id,Password FROM Employees", clsGlobal.myCon);
            clsGlobal.adpLogin = new SqlDataAdapter(myCmd);
            clsGlobal.adpLogin.Fill(clsGlobal.mySet, "Login");



        }

      

        private void btnLogin_Click(object sender, EventArgs e)
        {
          
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin lg = new frmLogin();
            lg.MdiParent = this;
            lg.Show();
           
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void allHomesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHouseSearch hs = new FrmHouseSearch();
            hs.Show();
            
        }

        private void allEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeSearch hs = new frmEmployeeSearch();
            hs.Show();
            
        }

        private void findByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHouseId hs = new frmHouseId();
            hs.Show();
            this.Hide();

        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmContactcs cs = new frmContactcs();
            cs.Show();

        }

        private void aboutAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin cs = new frmLogin();
            cs.Show();
            this.Hide();
        }

        private void findByIdToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmployeeId emp = new frmEmployeeId();
            emp.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void aboutAppToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutApp ap = new AboutApp();
            ap.Show();
        }
    }
}
