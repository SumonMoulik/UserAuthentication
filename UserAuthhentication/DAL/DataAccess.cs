using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace UserAuthhentication.DAL
{
    public class DataAccess
    {
        private static SqlConnection sqlConn = null;
        string connectionString = "Data Source=DESKTOP-TI5AOPR;Initial Catalog=UserAuthentication_DB;Integrated Security=True";
        public DataAccess()
        {
            //
        }

        public string ConnectionString()
        {
            return connectionString;
        }
    }
}