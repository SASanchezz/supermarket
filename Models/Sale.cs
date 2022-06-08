using System;
using System.Collections.Generic;
using supermarket.Utils;

namespace supermarket.Models
{
    internal class Sale
    {
        private const int upc = 0;
        private const int check_number = 1;
        private const int product_number = 2;
        private const int selling_price = 3;
        
        public static List<string[]> GetAllSales(DateTime minPrintDate, DateTime maxPrintDate)
        {
            string dateFormat = "yyyy-MM-dd";
            string minPrintDateString = minPrintDate.ToString(dateFormat);
            string maxPrintDateString = maxPrintDate.ToString(dateFormat);
            
            string sql = "SELECT Sale.UPC, Product.id_product, Product.product_name, Sale.selling_price, " +
                         "Sale.product_number, Receipt.print_date " +
                         "FROM Sale " +
                         "LEFT JOIN Store_Product ON Store_Product.UPC = Sale.UPC " +
                         "LEFT JOIN Product ON Product.id_product = Store_Product.id_product " +
                         "LEFT JOIN Receipt ON Sale.check_number = Receipt.receipt_number " +
                         $"WHERE DATE(Receipt.print_date) >= '{minPrintDateString}' " +
                         $"AND DATE(Receipt.print_date) <= '{maxPrintDateString}'";

            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }
        
        public static List<string[]> GetAllSalesByCheckNumber(string checkNumber)
        {
            string sql = "SELECT Sale.UPC, Product.id_product, Product.product_name, Sale.product_number," +
                         "Sale.selling_price " +
                         "FROM Sale " +
                         "LEFT JOIN Store_Product ON Store_Product.UPC = Sale.UPC " +
                         "LEFT JOIN Product ON Store_Product.id_product = Product.id_product " +
                         $"WHERE check_number = {checkNumber}";

            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }
    }
}