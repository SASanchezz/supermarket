using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.Utils;
using supermarket.Data;

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

            string sql = string.Format("SELECT * FROM Customer_Card WHERE percent>{0} AND percent<{1}",
                minPercent, maxPercent);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        public static string[] GetCustomerByCardNumber(string cardNumber)
        {
            string sql = string.Format("SELECT * FROM Customer_Card WHERE card_number ='{0}'", cardNumber);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }
        public static string[] GetCustomerByPhone(string phone)
        {
            string sql = string.Format("SELECT * FROM Customer_Card WHERE phone_number ='{0}'", phone);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static void DeleteCustomerByCardNumber(string cardNumber)
        {
            string sql = string.Format("DELETE FROM Customer_Card WHERE card_number ={0}", cardNumber);
            DbUtils.Execute(sql);
        }

        public static void AddCustomer(string cardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            string sql = String.Format("INSERT INTO Customer_Card " +
                "(card_number, cust_surname, cust_name, cust_patronymic, phone_number, city, street, zip_code, percent) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8})",
                cardNumber, surname, name, patronymic, phoneNumber, city, street, zipcode, percent);

            DbUtils.Execute(sql);
        }

        public static void UpdateCustomer(string initCardNumber, string changedCardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            string sql = string.Format("UPDATE Customer_Card SET " +
                "card_number='{1}', cust_surname='{2}', cust_name='{3}', cust_patronymic='{4}', phone_number='{5}', city='{6}', street='{7}', zip_code='{8}', percent={9} " +
                "WHERE card_number='{0}'",
                initCardNumber, changedCardNumber, surname, name, patronymic, phoneNumber, city, street, zipcode, percent);

            DbUtils.Execute(sql);
        }
    }
}
