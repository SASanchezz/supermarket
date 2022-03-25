using supermarket.Utils;
using System.Linq;

namespace supermarket.Middlewares.Category
{
    class CategoryValidator
    {

        public static string Validate(string categoryNumber, string categoryName)
        {
            if (categoryNumber.Any(x => !char.IsDigit(x)))
            {
                return "Невалідний номер";
            }
            //If someone tries to add product with product_id, that already exists
            if (DbQueries.GetCategoryByID(categoryNumber).Any())
            {
                return "Категорія з таким номером вже існує";
            }

            if (categoryName.Length is > 50 or 0)
            {
                return "Введіть назву категорії < 50 символів";
            }

            return ""; //Alright
        }

    }
}
