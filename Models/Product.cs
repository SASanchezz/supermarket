using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.Utils;
using supermarket.Data;

namespace supermarket.Models
{
    internal static class Product
    {
        //Native
        public const int id_product = 0;
        public const int category_number = 1;
        public const int product_name = 2;
        public const int characteristics = 3;
        public const int manufacturer = 4;
        //Joined
        public const int category_name = 5;

        public static List<string[]> GetAllProducts()
        {
            return GetAllProducts(Array.Empty<string>());
        }

        public static List<string[]> GetAllProducts(string[] categoryNames) // NOT id of category
        {
            string categoryFilter = categoryNames.Length == 0 ? "" : string.Format("WHERE Category.category_name IN ('{0}')", string.Join("',", categoryNames));

            string sql =
                "SELECT id_product, Product.category_number, product_name, characteristics, manufacturer, Category.category_name AS category_name " +
                "FROM Product LEFT JOIN Category " +
                "ON Product.category_number = Category.category_number " + categoryFilter;
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        public static string[] GetProductByID(string productId)
        {
            string sql = string.Format("SELECT * FROM Product WHERE id_product={0}", productId);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static void DeleteProductByID(string productId)
        {
            string sql = string.Format("DELETE FROM Product WHERE id_product={0}", productId);
            DbUtils.Execute(sql);
        }

        public static void AddProduct(string productId, string categoryNumber, string name, string characteristic)
        {
            string sql = String.Format("INSERT INTO Product " +
                "(id_product, category_number, product_name, characteristics) " +
                "VALUES ({0}, {1}, '{2}', '{3}')",
                productId, categoryNumber, name, characteristic);

            DbUtils.Execute(sql);
        }

        public static void UpdateProduct(string initProductId, string changedProductId, string categoryNumber, string name, string characteristic)
        {
            string sql = string.Format("UPDATE Product SET " +
                "id_product={1}, category_number={2}, product_name='{3}', characteristics='{4}' " +
                "WHERE id_product={0}",
                initProductId, changedProductId, categoryNumber, name, characteristic);

            DbUtils.Execute(sql);
        }
    }
}
