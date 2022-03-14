using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using MySql.Data.MySqlClient;
using supermarket.Connections;

namespace supermarket.Utils
{
    public static class DbUtils
    {

        static MySqlConnection s_connection;

        public static void SetDbConnection()
        {
            s_connection = Db.GetDBConnection();
            s_connection.Open();
        }

        public static MySqlConnection GetDb()
        {
            return s_connection;
        }

        public static void Execute(string sql)
        {
            MySqlCommand cmd = new(sql, GetDb());
            cmd.ExecuteNonQuery();
        }

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
                        rowData[i] = reader.GetString(i);
                    }
                    rowData = rowData[..^1];
                    output.Add(rowData);
                }
            }
            return output;
        }

    }
}
