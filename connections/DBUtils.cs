using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace supermarket.Connections
{
    public class DBUtils
    {
        static MySqlConnection connection;

        public static MySqlConnection Db()
        {
            return connection;
        }

        public static void Execute(string sql)
        {
            MySqlCommand cmd = new(sql, Db());
            cmd.ExecuteNonQuery();
        }

        public static List<string> FindAll(string sql)
        {
            MySqlConnection db = Db();

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

        public static void GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "supermarket";
            string username = "supermarket";
            string password = "S8p3Rm0rq3d";

            connection = DBMySQLUtils.GetDBConnection(host, port, database, username, password);

            connection.Open();
        }

    }
}
