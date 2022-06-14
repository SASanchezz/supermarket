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
        private const string AllString = "��";

        public static List<string[]> GetAllReceipts(string idCashier, DateTime minPrintDate, DateTime maxPrintDate, string likeReceiptId = "")
        {
            const string dateFormat = "yyyy-MM-dd";
            string minPrintDateString = minPrintDate.ToString(dateFormat);
            string maxPrintDateString = maxPrintDate.ToString(dateFormat);
            
            string sql = "SELECT receipt_number, Receipt.id_employee, card_number, print_date, sum_total, " +
                         "vat, empl_name, empl_surname, empl_patronymic " +
                         "FROM Receipt LEFT JOIN Employee ON Receipt.id_employee=Employee.id_employee " +
                         $"WHERE DATE(print_date) >= '{minPrintDateString}' AND DATE(print_date) <= '{maxPrintDateString}'";

            if (idCashier != AllString)
            {
                sql += $" AND Receipt.id_employee LIKE '%{idCashier}%'";
            }

            if (likeReceiptId != "")
            {
                sql += $" AND Receipt.receipt_number LIKE '%{likeReceiptId}%'";
            }

            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }

        public static double GetAllReceiptsSum(string idCashier, DateTime minPrintDate, DateTime maxPrintDate)
        {
            const string dateFormat = "yyyy-MM-dd";
            string minPrintDateString = minPrintDate.ToString(dateFormat);
            string maxPrintDateString = maxPrintDate.ToString(dateFormat);

            string whereClause = $"WHERE DATE(print_date) >= '{minPrintDateString}' AND DATE(print_date) <= '{maxPrintDateString}' ";

            whereClause = idCashier == AllString ? whereClause : whereClause += $"AND Receipt.id_employee LIKE '%{idCashier}%' ";

            string sql = "SELECT COALESCE(SUM(sum_total), 0) " +
                         "FROM Receipt LEFT JOIN Employee ON Receipt.id_employee=Employee.id_employee " + whereClause;

            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? double.Parse(result[0][0]) : 0;
        }



        public static void AddReceipt( string employeeId, string cardNumber, string sumTotal)
        {
            string vat = (double.Parse(sumTotal) * 0.8).ToString().Replace(',', '.');
            sumTotal = sumTotal.Replace(',', '.');

            string sql = "INSERT INTO Receipt " +
                         "(receipt_number, id_employee, card_number, print_date, sum_total, vat) " +
                         $"VALUES ('{IdUtils.ReceiptId()}', '{employeeId}', '{cardNumber}', {DateTime.Now}, {sumTotal}, {vat})";

            DbUtils.Execute(sql);
        }

        public static int DeleteReceiptByReceiptNumber(string receiptNumber)
        {
            string sql = $"DELETE FROM Receipt WHERE receipt_number ={receiptNumber}";
            int response = DbUtils.Execute(sql);
            return response;
        }
    }
}