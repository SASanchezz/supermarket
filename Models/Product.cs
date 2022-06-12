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

        private const string AllString = "Всі";

        public static List<string[]> GetAllProducts(string categoryName = AllString, string name = "") // NOT id of category
        {
            
            string whereClause = "WHERE 1 ";

            whereClause = categoryName == AllString ? whereClause : whereClause +=
                "AND Product.category_number IN " + 
                "(SELECT category_number " + 
                "FROM Category " +
                $"WHERE category_name='{categoryName}')";

            whereClause = name == "" ? whereClause : whereClause +=
                $"AND product_name LIKE '%{name}%'";


            string sql = "SELECT id_product, Product.category_number, product_name, characteristics, manufacturer, Category.category_name AS category_name " +
                "FROM (Product LEFT JOIN Category " +
                "ON Product.category_number = Category.category_number) " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static List<string[]> GetProductBySubNameOrId(string subString = "") // NOT id of category
        {
            string whereClause = "WHERE 1 ";

            whereClause = subString == "" ? whereClause : whereClause +=
                $"AND product_name LIKE '%{subString}%' OR id_product LIKE '%{subString}%'";


            string sql = "SELECT id_product, Product.category_number, product_name, characteristics, manufacturer, Category.category_name AS category_name " +
                "FROM (Product LEFT JOIN Category " +
                "ON Product.category_number = Category.category_number) " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            if (result.Count > 0) return result;

            sql = "SELECT id_product, Product.category_number, product_name, characteristics, manufacturer, Category.category_name AS category_name " +
                "FROM (Product LEFT JOIN Category " +
                "ON Product.category_number = Category.category_number) ";
            result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static string[] GetProductByID(string productId)
        {
            string sql = $"SELECT * FROM Product WHERE id_product={productId}";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static int DeleteProductByID(string productId)
        {
            string sql = $"DELETE FROM Product WHERE id_product={productId}";
            int response = DbUtils.Execute(sql);
            return response;
        }

        public static void AddProduct(string productId, string categoryNumber, string name, string characteristic, string manufacturer)
        {
            string sql = "INSERT INTO Product (id_product, category_number, product_name, characteristics, manufacturer) " +
                         $"VALUES ({productId}, {categoryNumber}, '{name}', '{characteristic}', '{manufacturer}')";

            DbUtils.Execute(sql);
        }

        public static void UpdateProduct(string initProductId, string changedProductId, string categoryNumber, string name, string characteristic, string manufacturer)
        {
            string sql = "UPDATE Product " +
                         $"SET id_product={changedProductId}, category_number={categoryNumber}, product_name='{name}', " +
                         $"characteristics='{characteristic}', manufacturer='{manufacturer}' " +
                         $"WHERE id_product={initProductId}";

            DbUtils.Execute(sql);
        }
    }
}
