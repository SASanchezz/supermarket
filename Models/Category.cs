using System.Collections.Generic;
using supermarket.Utils;

namespace supermarket.Models
{
    internal static class Category
    {
        //Native
        public const int number = 0;
        public const int name = 1;

        const int ERROR = -1;
        const int GOOD = 1;
        public static List<string[]> GetAllCategories()
        {
            string sql = "SELECT * FROM Category";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        public static string[] GetCategoryByNumber(string categoryNumber)
        {
            string sql = $"SELECT * FROM Category WHERE category_number='{categoryNumber}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static string[] GetAllCategoriesNames()
        {
            string sql = "SELECT category_name FROM Category ORDER BY category_name";
            List<string[]> list = DbUtils.FindAll(sql);
            var result = new string[list.Count];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = list[i][0];
            }

            return result.Length > 0 ? result : null;
        }

        public static string[] GetIDByName(string categoryName)
        {
            string sql = $"SELECT category_number FROM Category WHERE category_name='{categoryName}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static int DeleteCategoryByNumber(string categoryNumber)
        {
            string sql = $"DELETE FROM Category WHERE category_number={categoryNumber}";
            int response = DbUtils.Execute(sql);
            return response;
        }

        public static void AddCategory(string categoryNumber, string categoryName)
        {
            string sql = "INSERT INTO Category (category_number, category_name) " +
                         $"VALUES ({categoryNumber}, '{categoryName}')";

            DbUtils.Execute(sql);
        }

        public static void UpdateCategory(string initCategoryNumber, string changedCategoryNumber, string categoryName)
        {
            string sql = "UPDATE Category " +
                         $"SET category_number={changedCategoryNumber}, category_name='{categoryName}' " +
                         $"WHERE category_number={initCategoryNumber}";

            DbUtils.Execute(sql);
        }
    }
}
