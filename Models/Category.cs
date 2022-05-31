using System.Collections.Generic;
using supermarket.Utils;

namespace supermarket.Models
{
    internal static class Category
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

        public static string[] GetAllCategoriesNames()
        {
            string sql = "SELECT category_name FROM Category ORDER BY category_name";
            List<string[]> list = DbUtils.FindAll(sql);
            string[] result = new string[list.Count];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = list[i][0];
            }

            return result.Length > 0 ? result : null;
        }

        public static string[] GetIDByName(string categoryName)
        {
            string sql = string.Format("SELECT category_number FROM Category WHERE category_name='{0}'", categoryName);
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
