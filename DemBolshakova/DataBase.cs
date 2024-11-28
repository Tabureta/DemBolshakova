using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data.Common;

namespace DemBolshakova
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-BLTQNR9D; Initial Catalog=BoshakovaDemPractice; Integrated Security=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed) { sqlConnection.Open(); }
        }

        public void closeConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open) { sqlConnection.Close(); }
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}
