using System;
using System.Linq;
using Cat = supermarket.Models.Category;

namespace supermarket.Middlewares.Category
{
    public static class CategoryValidator
    {
        public static void ValidateInsert(string categoryNumber, string categoryName)
        {
            if (categoryNumber.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Невалідний номер");
            }
            // If someone tries to add product with product_id, that already exists
            if (Cat.GetCategoryByNumber(categoryNumber) != null)
            {
                throw new Exception("Категорія з таким номером вже існує");
            }
            
            if (Cat.GetAllCategoriesNames().Contains(categoryName))
            {
                throw new Exception("Категорія з таким ім'ям вже існує");
            }

            if (categoryName.Length is > 50 or 0)
            {
                throw new Exception("Введіть назву категорії < 50 символів");
            }
        }

        public static void ValidateUpdate(string initCategoryNumber, string initCategoryName, 
            string changedCategoryNumber, string changedCategoryName)
        {
            if (changedCategoryNumber.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Невалідний номер");
            }
            
            if (initCategoryNumber != changedCategoryNumber && Cat.GetCategoryByNumber(changedCategoryNumber) != null)
            {
                throw new Exception("Категорія з таким номером вже існує");
            }
            
            if (initCategoryName != changedCategoryName && Cat.GetAllCategoriesNames().Contains(changedCategoryName))
            {
               throw new Exception("Категорія з таким ім'ям вже існує");
            }
            
            if (changedCategoryName.Length is > 50 or 0)
            {
                throw new Exception("Введіть назву категорії < 50 символів");
            }
        }

    }
}
