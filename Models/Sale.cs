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
        
        public static List<string[]> GetAllSalesByCheckNumber(string checkNumber)
        {
            string sql = "SELECT Sale.UPC, Sale.product_number, Product.product_name, Store_Product.selling_price," +
                         "Sale.selling_price " +
                         "FROM Sale " +
                         "LEFT JOIN Product ON Sale.product_number = Product.id_product " +
                         "LEFT JOIN Store_Product ON Store_Product.UPC = Sale.UPC " +
                         $"WHERE check_number = {checkNumber}";

            List<string[]> result = DbUtils.FindAll(sql);
            
            return result.Count > 0 ? result : null;
        }
    }
}