using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace supermarket.connections
{
    public class DBUtils
    {

        public static void Execute(MySqlConnection db, string sql)
        {
            MySqlCommand cmd = new(sql, db);
            cmd.ExecuteNonQuery();
        }

        public static List<string> FindAll(MySqlConnection db, string sql)
        {
            MySqlCommand cmd = new(sql, db);

            using DbDataReader reader = cmd.ExecuteReader();
            int columnsNumber = reader.FieldCount;
            List<string> output = new();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string rowData = "";
                    for (int i = 0; i < columnsNumber; ++i)
                    {
                        rowData += reader.GetString(i) + ',';
                    }
                    rowData = rowData[..^1];
                    output.Add(rowData);
                }
            }
            return output;
        }

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
