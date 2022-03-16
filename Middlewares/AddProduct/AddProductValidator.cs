using System;
using System.Collections.Generic;
using System.Linq;
using supermarket.Utils;

namespace supermarket.Middlewares.AddProduct
{
    public static class AddProductValidator
    {

        public static string Validate(string productId, string name, string categoryNumber , string characteristics)
        {

            
            List<string[]> result = DbQueries.GetProductByID(productId);
            if (result.Any())
            {
                return "Товар з таким кодом вже існує";
            }

            if (name.Length is > 50 or 0)
            {
                return "Введіть назву товару < 50 символів";
            }

            if (characteristics.Length is > 100 or 0)
            {
                return "Введіть характеристику товару < 100 символів";
            }

            if (!DbQueries.GetCategoryByID(categoryNumber).Any())
            {
                return "Оберіть категорію зі списку";
            }

            return ""; //Alright
        }
    }
}
