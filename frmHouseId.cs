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
    public partial class frmHouseId : Form
    {
        public frmHouseId()
        {
            InitializeComponent();
        }
        DataTable tabHouse;


        private void lstHouse_Click(object sender, EventArgs e)
        {
           

            
        }
        private void frmHouseId_Load(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["Houses"];
            var allHouses = from comp in tabHouse.AsEnumerable()
                               select new
                               {
                                 RefHouse = comp.Field<int>("Id")
                               };

            lstHouse.DataSource = allHouses.ToList();
            lstHouse.ValueMember ="RefHouse";
            lstHouse.SelectedIndex = -1;
            //var allHs = from comp in tabHouse.AsEnumerable()
            //                select new
            //                {
            //                    Address = comp.Field<string>("Address"),
            //                    City = comp.Field<string>("City"),
            //                    Status = comp.Field<string>("Status"),
            //                    Type = comp.Field<string>("Type"),
            //                    NumberofRoom = comp.Field<int>("NRooms"),
            //                    NumberOfBath = comp.Field<int>("NBaths"),
            //                    Size= comp.Field<int>("Size"),
            //                    Price= comp.Field<decimal>("Price")
            //                };

            //dataGridView1.DataSource = allHs.ToList();
           
        }

        private void lstHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
        

        }

        private void Find_Click(object sender, EventArgs e)
        {
            tabHouse = clsGlobal.mySet.Tables["Houses"];
            if (txtId.Text == "")
            {
                MessageBox.Show("Please Insert Id in the box!!!! \n You can see the list of Idies at topright side");
            }
            else
            {
                int refHs = Convert.ToInt16(txtId.Text);


                var selHouse = from Hs in tabHouse.AsEnumerable()
                               where Hs.Field<int>("Id") == refHs
                               select Hs;

                txtAddress.Text = selHouse.ElementAt(0)["Address"].ToString();
                txtBath.Text = selHouse.ElementAt(0)["NBaths"].ToString();
                txtRoom.Text = selHouse.ElementAt(0)["NRooms"].ToString();
                txtSize.Text = selHouse.ElementAt(0)["Size"].ToString();
                txtType.Text = selHouse.ElementAt(0)["Type"].ToString();
                txtStatus.Text = selHouse.ElementAt(0)["Status"].ToString();
                txtPrice.Text = selHouse.ElementAt(0)["Price"].ToString();
                txtCity.Text = selHouse.ElementAt(0)["City"].ToString();
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmMain mn = new frmMain();
            mn.Show();
            this.Close();
        }
    }
}
