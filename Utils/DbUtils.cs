﻿using System;
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
    internal static class DbUtils
    {
        const int ERROR = -1;
        const int GOOD = 1;
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
        public static int Execute(string sql)
        {
            MySqlCommand cmd = new(sql, GetDb());
            try
            {
                cmd.ExecuteNonQuery();
            } catch (Exception err)
            {
                Logger.Log("dbLogs.txt", cmd.CommandText);
                Logger.Log("dbLogs.txt", err.ToString());
                return ERROR;
            }
            return GOOD;
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

        public static string StringifyOrder(string[][] orders)
        {
            if (orders.Length == 0) return "";

            string finalOrder = "";
            foreach(string[] order in orders)
            {
                if (order[0] == "") continue;
                finalOrder += " " + order[0] + " " + order[1] + ",";
            }

            return (finalOrder == "") ? "" : "ORDER BY" + finalOrder.TrimEnd(',');
        }

        public static string StringifyFilter(string[][] filters)
        {
            if (filters[0].Length == 0) return "";

            string finalFilter = "WHERE";
            foreach (string[] order in filters)
            {
                finalFilter += " " + order[0] + order[1];
            }

            return finalFilter;
        }
    }
}
