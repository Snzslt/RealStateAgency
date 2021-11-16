using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace PrjWinRemax
{
    
    public static class clsGlobal
    {
        public static DataSet mySet;
        public static SqlConnection myCon;
        public static SqlDataAdapter adpAgency,adpEmployee, adpClient,adpHouse,adpLogin;
        public static Int32 Pass;
        public static Int32 Id;




    }
    
}
