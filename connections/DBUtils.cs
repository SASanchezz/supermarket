using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace supermarket.connections
{
    public class DBUtils
    {

        public static MySqlConnection Db()
        {
            MySqlConnection connection = GetDBConnection();
            connection.Open();

            return connection;
        }

        private static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "supermarket";
            string username = "supermarket";
            string password = "S8p3Rm0rq3d";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}
