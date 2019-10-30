using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace myCy.BaseClass
{
    class DBConn
    {
        public static SqlConnection CyCon()
        {
            return new SqlConnection("server=.;database=db_MrCy;integrated security=true");
        }
    }
}
