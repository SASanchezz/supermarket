using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using supermarket.Connections;
/*
 * This class initiate mysql connection and uses Connections.Db class to do this
 * 
 * Also it contains methods for executing SQL queries as Execute() and FindAll()
 */
namespace supermarket.Utils
{
    public static class DbUtils
    {

        static MySqlConnection s_connection;

        /*
         * This method initiates connection with MySQL server
         */
        public static void SetDbConnection()
        {
            s_connection = Db.GetDBConnection();
            s_connection.Open();
        }

        public static MySqlConnection GetDb()
        {
            return s_connection;
        }

        /*
         * This method executes most SQL query BESIDES "SELECT"
         */
        public static void Execute(string sql)
        {
            MySqlCommand cmd = new(sql, GetDb());
            cmd.ExecuteNonQuery();
        }

        /*
         * This method executes SELECT SQL queries to retrieve some data from DB
         */
        public static List<string[]> FindAll(string sql)
        {
            MySqlConnection db = GetDb();

            MySqlCommand cmd = new(sql, db);

            using DbDataReader reader = cmd.ExecuteReader();
            int columnsNumber = reader.FieldCount;
            List<string[]> output = new();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string[] rowData = new string[columnsNumber];
                    for (int i = 0; i < columnsNumber; ++i)
                    {   
                        try
                        {
                            rowData[i] = reader.GetString(i);
                        } catch { rowData[i] = null; }
                    }
                    output.Add(rowData);
                }
            }
            return output;
        }

    }
}
