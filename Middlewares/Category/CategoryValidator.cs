using supermarket.Utils;
using System.Linq;
using System.Windows;
using Cat = supermarket.Models.Category;

namespace supermarket.Middlewares.Category
{
    internal static class CategoryValidator
    {
        public static string ValidateInsert(string categoryNumber, string categoryName)
        {
            if (categoryNumber.Any(x => !char.IsDigit(x)))
            {
                return "Невалідний номер";
            }
            // If someone tries to add product with product_id, that already exists
            if (Cat.GetCategoryByNumber(categoryNumber) != null)
            {
                return "Категорія з таким номером вже існує";
            }
            
            if (Cat.GetAllCategoriesNames().Contains(categoryName))
            {
                return "Категорія з таким ім'ям вже існує";
            }

            if (categoryName.Length is > 50 or 0)
            {
                return "Введіть назву категорії < 50 символів";
            }

            return ""; //Alright
        }

        public static string ValidateUpdate(string initCategoryNumber, string initCategoryName, 
            string changedCategoryNumber, string changedCategoryName)
        {
            if (changedCategoryNumber.Any(x => !char.IsDigit(x)))
            {
                return "Невалідний номер";
            }
            
            if (initCategoryNumber != changedCategoryNumber && Cat.GetCategoryByNumber(changedCategoryNumber) != null)
            {
                return "Категорія з таким номером вже існує";
            }
            
            if (initCategoryName != changedCategoryName && Cat.GetAllCategoriesNames().Contains(changedCategoryName))
            {
                return "Категорія з таким ім'ям вже існує";
            }
            
            if (changedCategoryName.Length is > 50 or 0)
            {
                return "Введіть назву категорії < 50 символів";
            }
            
            return "";
        }

    }
}
