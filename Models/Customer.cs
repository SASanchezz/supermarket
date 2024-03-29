﻿using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using supermarket.Utils;

namespace supermarket.Models
{
    internal static class Customer
    {
        //Native
        public const int card_number = 0;
        public const int surname = 1;
        public const int name = 2;
        public const int patronymic = 3;
        public const int phone_number = 4;
        public const int city = 5;
        public const int street = 6;
        public const int zipcode = 7;
        public const int percent = 8;

        public static List<string[]> GetAllCustomers(double minPercent = -1, double maxPercent = 101)
        {
            string minPercentString = minPercent.ToString().Replace(',', '.');
            string maxPercentString = maxPercent.ToString().Replace(',', '.');
            
            string sql =
                $"SELECT * FROM Customer_Card WHERE percent>={minPercentString} AND percent<={maxPercentString}";
            
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        
        public static string[] GetCustomerByCardNumber(string cardNumber)
        {
            string sql = $"SELECT * FROM Customer_Card WHERE card_number ='{cardNumber}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }
        
        public static string[] GetCustomerByPhone(string phone)
        {
            string sql = $"SELECT * FROM Customer_Card WHERE phone_number ='{phone}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static List<string[]> GetCustomersLikeSNPOrCardOrPhone(string subString)
        {
            string sql = $"SELECT * FROM Customer_Card ";

            if (subString != "")
            {
                sql += $"WHERE card_number LIKE '%{subString}%'" +
                    $" OR cust_surname LIKE '%{subString}%'" +
                    $" OR cust_name LIKE '%{subString}%'" +
                    $" OR cust_patronymic LIKE '%{subString}%'" +
                    $" OR phone_number LIKE '%{subString}%'";
            }

            List<string[]> result = DbUtils.FindAll(sql);

            if (result.Count > 0) return result;


            sql = $"SELECT * FROM Customer_Card ";
            result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static int DeleteCustomerByCardNumber(string cardNumber)
        {
            string sql = $"DELETE FROM Customer_Card WHERE card_number ={cardNumber}";
            int response = DbUtils.Execute(sql);
            return response;
        }

        public static void AddCustomer(string cardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            string sql = "INSERT INTO Customer_Card " +
                         "(card_number, cust_surname, cust_name, cust_patronymic, phone_number, city, street, zip_code, percent) " +
                         $"VALUES ('{cardNumber}', '{surname}', '{name}', '{patronymic}', '{phoneNumber}', '{city}', '{street}', '{zipcode}', {percent})";

            DbUtils.Execute(sql);
        }

        public static void UpdateCustomer(string initCardNumber, string changedCardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            string sql = "UPDATE Customer_Card " +
                         $"SET card_number='{changedCardNumber}', cust_surname='{surname}', cust_name='{name}', " +
                         $"cust_patronymic='{patronymic}', phone_number='{phoneNumber}', city='{city}', " +
                         $"street='{street}', zip_code='{zipcode}', percent={percent} " +
                         $"WHERE card_number='{initCardNumber}'";

            DbUtils.Execute(sql);
        }
    }
}
