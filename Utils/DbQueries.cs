using supermarket.Data;
using System.Collections.Generic;
using prdct = supermarket.Data.DbMaps.ProductMap;
/*
* This class contains simple SQL queries, that can be often used,
* easily explained with method name and don't require much arguments
*/
namespace supermarket.Utils
{
    public static class DbQueries
    {
        /*
         * Employees
         */
        public static List<string[]> GetAllEmployee()
        {
            string sql = "SELECT * FROM Employee";
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetEmployeeByID(string employeeId)
        {
            string sql = string.Format("SELECT * FROM Employee WHERE id_employee='{0}'", employeeId);
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetEmployeesByRole(int employeeRole)
        {
            string sql = string.Format("SELECT * FROM Employee WHERE empl_role_id='{0}'", employeeRole);
            return DbUtils.FindAll(sql);
        }

        public static void DeleteEmployeeByID(string employeeId)
        {
            string sql = string.Format("DELETE FROM Employee WHERE id_employee='{0}'", employeeId);
            DbUtils.Execute(sql);
        }

        /*
         * Products
         */
        public static List<string[]> GetAllProducts(params string[] sorts)
        {
            string filter = "";
            string order = "";
            try
            {
                string categoryNumbers = sorts[1];
                filter = (sorts[1] == "") ? "" : string.Format(" WHERE Product.{0} in ({1})", sorts[0], sorts[1]);

                //get asc\desc parameters for columns
                order = Utils.ParseOrder(2, sorts); //2 - To pass filter options
                order = (order == "") ? "" : " ORDER BY " + order[..^2];
            } catch { }
            
            string sql =
                "SELECT id_product, Product.category_number, product_name, characteristics, manufacturer, Category.category_name AS category_name" +
                " FROM Product LEFT JOIN Category" +
                " ON Product.category_number = Category.category_number"
                + filter + order;
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetProductByID(string productId)
        {
            string sql = string.Format("SELECT * FROM Product WHERE id_product={0}", productId);
            return DbUtils.FindAll(sql);
        }

        public static void DeleteProductByID(string productId)
        {
            string sql = string.Format("DELETE FROM Product WHERE id_product={0}", productId);
            DbUtils.Execute(sql);
        }


        /*
         * Categories
         */
        public static List<string[]> GetAllCategories(params string[] sorts)
        {
            string order = "";
            try
            {

                //get asc\desc parameters for columns
                order = Utils.ParseOrder(0, sorts);
                order = (order == "") ? "" : " ORDER BY " + order[..^2];
            }
            catch { }

            string sql = "SELECT * FROM Category" + order;
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetCategoryByID(string categoryNumber)
        {
            string sql = string.Format("SELECT * FROM Category WHERE category_number='{0}'", categoryNumber);
            return DbUtils.FindAll(sql);
        }
        public static void DeleteCategoryByID(string categoryNumber)
        {
            string sql = string.Format("DELETE FROM Product WHERE category_number={0}", categoryNumber);
            DbUtils.Execute(sql);
        }


        /*
         * Customers
         */
        public static List<string[]> GetAllCustomers(params string[] sorts)
        {
            string order = "";
            string filter = "";
            try
            {
                string categoryNumbers = sorts[1];
                filter = (sorts[1] == "") ? "" : string.Format(" WHERE Customer_Card.{0} {1}", sorts[0], sorts[1]);
                filter = (sorts[3] == "") ? filter : filter += string.Format(" AND Customer_Card.{0} {1}", sorts[2], sorts[3]);

                //get asc\desc parameters for columns
                order = Utils.ParseOrder(4, sorts);
                order = (order == "") ? "" : " ORDER BY " + order[..^2];
            }
            catch { }

            string sql = "SELECT * FROM Customer_Card" + filter + order;
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetCustomerByID(string cardNumber)
        {
            string sql = string.Format("SELECT * FROM Customer_Card WHERE card_number ='{0}'", cardNumber);
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetCustomerByPhone(string phone)
        {
            string sql = string.Format("SELECT * FROM Customer_Card WHERE phone_number ='{0}'", phone);
            return DbUtils.FindAll(sql);
        }

        public static void DeleteCustomerByID(string cardNumber)
        {
            string sql = string.Format("DELETE FROM Customer_Card WHERE card_number ={0}", cardNumber);
            DbUtils.Execute(sql);
        }


        /*
         * Market Products
         */
        public static List<string[]> GetAllStoreProducts(string filters = "", string orders = "")
        {
            string sql = string.Format("SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name" +
                " FROM Store_Product" +
                " LEFT JOIN Product" +
                " ON Store_Product.id_product  = Product.id_product" +
                " {0} {1}", filters, orders);

            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetStoreProductByID(string UPC)
        {
            string sql = string.Format("SELECT * FROM Store_Product WHERE UPC ='{0}'", UPC);
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetStoreProductByPromUpc(string UPC_Prom)
        {
            string sql = string.Format("SELECT * FROM Store_Product WHERE UPC_Prom ='{0}'", UPC_Prom);
            return DbUtils.FindAll(sql);
        }

        public static void DeleteStoreProductByID(string UPC)
        {
            string sql = string.Format("DELETE FROM Store_Product WHERE UPC = '{0}'", UPC);
            DbUtils.Execute(sql);
        }
    }
}
