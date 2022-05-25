using System.Collections.Generic;
using supermarket.Utils;

namespace supermarket.Models
{
    public static class Category
    {
        //Native
        public const int number = 0;
        public const int name = 1;


        public static List<string[]> GetAllCategories()
        {
            string sql = "SELECT * FROM Category";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        public static string[] GetCategoryByID(string categoryNumber)
        {
            string sql = string.Format("SELECT * FROM Category WHERE category_number='{0}'", categoryNumber);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }
        public static void DeleteCategoryByID(string categoryNumber)
        {
            string sql = string.Format("DELETE FROM Product WHERE category_number={0}", categoryNumber);
            DbUtils.Execute(sql);
        }

        public static void AddCategory(string categoryNumber, string categoryName)
        {
            string sql = string.Format("INSERT INTO Category " +
                "(category_number, category_name) " +
                "VALUES ({0}, '{1}')",
                categoryNumber, categoryName);

            DbUtils.Execute(sql);
        }

        public static void UpdateCategory(string initCategoryNumber, string changedCategoryNumber, string categoryName)
        {
            string sql = string.Format("UPDATE Category SET" +
                "category_number={1}, category_name='{2}' ",
                "WHERE category_number = {0})",
                initCategoryNumber, changedCategoryNumber, categoryName);

            DbUtils.Execute(sql);
        }
       
    }
}
