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
    public partial class frmAgent : Form
    {
        public frmAgent()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabEmployee, tabClient, tabAgency;

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var allHs = from cl in tabClient.AsEnumerable()


                        where cl.Field<int>("RefAgent") == 1
                        select new
                        {
                            Name = cl.Field<string>("FullName"),
                            Emails = cl.Field<string>("Email"),
                            Birthdate= cl.Field<DateTime>("Birthdate"),
                            Gender =cl.Field<string>("Gender"),
                            Phone = cl.Field<decimal>("Phone")

                        };
             dataGridView1.DataSource = allHs.ToList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AgentClient ac = new AgentClient();
            ac.Show();
        }

        private void frmAgent_Load(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["Houses"];
            tabEmployee = clsGlobal.mySet.Tables["Employees"];
            tabClient = clsGlobal.mySet.Tables["Clients"];
            
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var allHs = from hs in tabHouse.AsEnumerable()
                        from cl in tabClient.AsEnumerable()
                      

                        where hs.Field<int>("RefSeller") == cl.Field<int>("Id") && cl.Field<int>("RefAgent") == 1
                        select new
                        {
                            City = hs.Field<string>("City"),
                            Size = hs.Field<int>("Size"),
                            Address = hs.Field<string>("Addres")
                           
                        };
            dataGridView1.DataSource = allHs.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
