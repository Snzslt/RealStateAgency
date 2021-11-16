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
    public partial class FrmHouseSearch : Form
    {
        public FrmHouseSearch()
        {
            InitializeComponent();
        }
        DataTable tabHouse, tabSeller;
        int currentIndex;
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmHouseSearch_Load(object sender, EventArgs e)
        {

            tabSeller = clsGlobal.mySet.Tables["Clients"];
            tabHouse = clsGlobal.mySet.Tables["Houses"];
          


            currentIndex = 0;
            Display();

          
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
            if (currentIndex < (tabHouse.Rows.Count - 1))
            {
                currentIndex++;
                Display();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            currentIndex = tabHouse.Rows.Count - 1;
            Display();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           frmMain mn = new frmMain();
           mn.Show();
            this.Close();



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
           
            lblInfo.Text = "House " + (currentIndex + 1) + " on a total of " + tabHouse.Rows.Count;

            



        }
       

    }
}
