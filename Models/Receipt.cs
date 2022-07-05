using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
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
        public const int real_sum_total = 9;

        static int ERROR = -1;
        static int GOOD = 1;

        public static List<string[]> GetAllReceipts(string idCashier, DateTime minPrintDate, DateTime maxPrintDate, 
            string likeReceiptId = "")
        {
            string sql = "SELECT receipt_number, Receipt.id_employee, card_number, print_date, sum_total, " +
                         "vat, empl_name, empl_surname, empl_patronymic " +
                         "FROM Receipt LEFT JOIN Employee ON Receipt.id_employee=Employee.id_employee " +
                         $"WHERE DATE(print_date) >= '{minPrintDate.ToString(Constants.DateStringFormat)}' " +
                         $"AND DATE(print_date) <= '{maxPrintDate.ToString(Constants.DateStringFormat)}'";

            if (idCashier != Constants.AllString)
            {
                sql += $" AND Receipt.id_employee LIKE '%{idCashier}%'";
            }

            if (likeReceiptId != "")
            {
                sql += $" AND Receipt.receipt_number LIKE '%{likeReceiptId}%'";
            }

            List<string[]> result = DbUtils.FindAll(sql);

            List<string[]> newList = new();
            result.ForEach(receipt =>
            {
                string[] newArray = new string[10];
                Array.Copy(receipt, newArray, 9);
                newArray[9] = Math.Round(getRealReceiptSum(receipt[receipt_number]), 2).ToString();
                newList.Add(newArray);

            });
            
            return newList.Count > 0 ? newList : null;
        }

        public static string[] GetReceiptByReceiptNumber(string receiptNumber)
        {
            string sql = $"SELECT * " +
                         $"FROM Receipt " +
                         $"WHERE receipt_number = '{receiptNumber}'";

            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static double GetAllReceiptsSum(string idCashier, DateTime minPrintDate, DateTime maxPrintDate)
        {
            string whereClause = $"WHERE DATE(print_date) >= '{minPrintDate.ToString(Constants.DateStringFormat)}' " +
                                 $"AND DATE(print_date) <= '{maxPrintDate.ToString(Constants.DateStringFormat)}' ";

            whereClause = idCashier == Constants.AllString ? whereClause : whereClause += $"AND Receipt.id_employee LIKE '%{idCashier}%' ";

            string sql = "SELECT COALESCE(SUM(sum_total), 0) " +
                         "FROM Receipt LEFT JOIN Employee ON Receipt.id_employee=Employee.id_employee " + whereClause;

            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? double.Parse(result[0][0]) : 0;
        }

        public static string AddReceipt( string employeeId, string cardNumber, string sumTotal)
        {
            string receiptId = IdUtils.ReceiptId();
            string vat = (double.Parse(sumTotal) * Constants.VatPercent).ToString();
            sumTotal = sumTotal.Replace('.', ',');

            if (cardNumber == "")
            {
                cardNumber = "NULL";
            } else
            {
                string[] customer = Customer.GetCustomerByCardNumber(cardNumber);
                sumTotal = (double.Parse(sumTotal) * (100 - double.Parse(customer[Customer.percent].Replace('.', ','))) / 100).ToString();
                cardNumber = $"'{cardNumber}'";
            }

            string sql = "INSERT INTO Receipt " +
                         "(receipt_number, id_employee, card_number, print_date, sum_total, vat) " +
                         $"VALUES ('{receiptId}', {employeeId}, {cardNumber}, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', {sumTotal.Replace(',', '.')}, {vat.Replace(',', '.')})";

            DbUtils.Execute(sql);

            return receiptId;
        }

        public static int DeleteReceiptByReceiptNumber(string receiptNumber)
        {
            List<string[]> receiptSales = Sale.GetAllSalesByCheckNumber(receiptNumber);
            receiptSales.ForEach(sale =>
            {
                string sql = $"UPDATE Store_Product " +
                                $"SET products_number = products_number + {sale[Sale.product_number]} " +
                                $"WHERE UPC = '{sale[Sale.upc]}'";

                DbUtils.Execute(sql);
            });

            string sql = $"DELETE FROM Receipt WHERE receipt_number = '{receiptNumber}'";
            int response = DbUtils.Execute(sql);
            if (response == GOOD)
            {

            }
            return response;
        }

        public static double getRealReceiptSum(string receiptNumber)
        {
            double finalSum = 0;

            List<string[]> receiptSales = Sale.GetAllSalesByCheckNumber(receiptNumber);
            receiptSales.ForEach(sale =>
            {
                finalSum += double.Parse(sale[Sale.selling_price]);
            });

            return Math.Round(finalSum, 2);
        }

        public static string GetReceiptCustomerDiscount(string receiptNumber)
        {
            string[] receipt = GetReceiptByReceiptNumber(receiptNumber);
            if (receipt[card_number] == null || receipt[card_number] == "") return "0";

            string[] customer = Customer.GetCustomerByCardNumber(receipt[card_number]);

            return customer[Customer.percent];
        }
    }
}