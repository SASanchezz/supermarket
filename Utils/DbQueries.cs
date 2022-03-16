using System.Collections.Generic;
/*
* This class contains simple SQL queries, that can be often used,
* easily explained with method name and don't require much arguments
*/
namespace supermarket.Utils
{
    public static class DbQueries
    {

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

        public static void DeleteEmployeeByID(string employeeId)
        {
            string sql = string.Format("DELETE FROM Employee WHERE id_employee='{0}'", employeeId);
            DbUtils.Execute(sql);
        }


        public static List<string[]> GetAllProducts()
        {
            string sql = "SELECT * FROM Product";
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetProductByID(string productId)
        {
            string sql = string.Format("SELECT * FROM Product WHERE id_product='{0}'", productId);
            return DbUtils.FindAll(sql);
        }

        public static void DeleteProductByID(string productId)
        {
            string sql = string.Format("DELETE FROM Product WHERE id_product={0}", productId);
            DbUtils.Execute(sql);
        }


        public static List<string[]> GetAllCategories()
        {
            string sql = "SELECT * FROM Category";
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetCategoryByID(string categoryNumber)
        {
            string sql = string.Format("SELECT * FROM Category WHERE category_number='{0}'", categoryNumber);
            return DbUtils.FindAll(sql);
        }

    }
}
