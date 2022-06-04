using System;
using System.Collections.Generic;
using System.Windows;
using supermarket.Utils;

namespace supermarket.Models
{
    internal static class Receipt
    {
        public const int receipt_number = 0;
        public const int id_employee = 1;
        public const int card_number = 2;
        public const int print_date = 3;
        public const int sum_total = 4;
        public const int vat = 5;
        // added
        public const int name_employee = 6;
        public const int surname_employee = 7;
        public const int patronymic_employee = 8;
        
        //private static DateTime _minPrintDate = DateTime.Today.AddYears(-3); to validator
        
        public static List<string[]> GetAllReceipts(string idCashier, DateTime minPrintDate, DateTime maxPrintDate)
        {
            const string dateFormat = "yyyy-MM-dd";
            string minPrintDateString = minPrintDate.ToString(dateFormat);
            string maxPrintDateString = maxPrintDate.ToString(dateFormat);
            
            string sql = "SELECT receipt_number, Receipt.id_employee, card_number, print_date, sum_total, " +
                         "vat, empl_name, empl_surname, empl_patronymic " +
                         "FROM Receipt LEFT JOIN Employee ON Receipt.id_employee=Employee.id_employee " +
                         $"WHERE DATE(print_date) >= '{minPrintDateString}' AND DATE(print_date) <= '{maxPrintDateString}'";

            if (idCashier != "")
            {
                sql += $" AND Receipt.id_employee = {idCashier}";
            }
            
            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }
        
    }
}