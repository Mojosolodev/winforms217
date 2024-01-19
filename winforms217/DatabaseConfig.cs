using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace winforms217
{

    public class DatabaseConfig
    {
        private static readonly string ConnectionString = "Data Source=MOJOJOJO\\SQLEXPRESS;Initial Catalog=cités_u;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }


}
