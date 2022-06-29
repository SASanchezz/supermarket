using System;
using System.Collections.Generic;
using supermarket.Utils;

namespace supermarket.Models
{
    internal class Sale
    {
        public const int upc = 0;
        public const int product_id = 1;
        public const int product_name = 2;
        public const int product_number = 3;
        public const int selling_price = 4;

        public static List<string[]> GetAllSales(string productId, DateTime minPrintDate, DateTime maxPrintDate)
        {
            string minPrintDateString = minPrintDate.ToString(Constants.DateStringFormat);
            string maxPrintDateString = maxPrintDate.ToString(Constants.DateStringFormat);
            
            string sql = "SELECT Sale.UPC, Product.id_product, Product.product_name, Sale.selling_price, " +
                         "Sale.product_number, Receipt.print_date " +
                         "FROM Sale " +
                         "LEFT JOIN Store_Product ON Store_Product.UPC = Sale.UPC " +
                         "LEFT JOIN Product ON Product.id_product = Store_Product.id_product " +
                         "LEFT JOIN Receipt ON Sale.check_number = Receipt.receipt_number " +
                         $"WHERE DATE(Receipt.print_date) >= '{minPrintDateString}' " +
                         $"AND DATE(Receipt.print_date) <= '{maxPrintDateString}'";

            if (productId != Constants.AllString)
            {
                sql += $" AND Product.id_product LIKE '%{productId}%'";
            }

            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }

        public static void AddSale(string UPC, string checkNumber, string productNumber, string sellingPrice)
        {
            string sql = "INSERT INTO Sale (UPC, check_number , product_number, selling_price) " +
                         $"VALUES ('{UPC}', '{checkNumber}', {productNumber}, {sellingPrice.Replace(',', '.')})";

            DbUtils.Execute(sql);

            string removeProducts = $"UPDATE Store_Product " +
                                    $"SET products_number = products_number - {productNumber} " +
                                    $"WHERE UPC = '{UPC}'";

            DbUtils.Execute(removeProducts);
        }

        public static List<string[]> GetAllSalesByCheckNumber(string checkNumber)
        {
            string sql = "SELECT Sale.UPC, Product.id_product, Product.product_name, Sale.product_number," +
                         "Sale.selling_price " +
                         "FROM Sale " +
                         "LEFT JOIN Store_Product ON Store_Product.UPC = Sale.UPC " +
                         "LEFT JOIN Product ON Store_Product.id_product = Product.id_product " +
                         $"WHERE check_number = '{checkNumber}'";

            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }
        
        public static string GetSumOfNumberOfProducts(string productId, DateTime minPrintDate, DateTime maxPrintDate)
        {
            string minPrintDateString = minPrintDate.ToString(Constants.DateStringFormat);
            string maxPrintDateString = maxPrintDate.ToString(Constants.DateStringFormat);
            
            string sql = "SELECT SUM(Sale.product_number) " +
                         "FROM Sale " +
                         "LEFT JOIN Store_Product ON Store_Product.UPC = Sale.UPC " +
                         "LEFT JOIN Product ON Store_Product.id_product = Product.id_product " +
                         "LEFT JOIN Receipt ON Sale.check_number = Receipt.receipt_number " +
                         $"WHERE DATE(Receipt.print_date) >= '{minPrintDateString}' " +
                         $"AND DATE(Receipt.print_date) <= '{maxPrintDateString}'";
            
            if (productId != Constants.AllString)
            {
                sql += $" AND Product.id_product LIKE '%{productId}%'";
            }
            
            List<string[]> result = DbUtils.FindAll(sql);
            return result[0][0] != null ? result[0][0] : "0";
        }
    }
}