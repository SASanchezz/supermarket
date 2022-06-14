using System;
using System.Linq;
using supermarket.Utils;

/*
 * This class ccontains validation for adding new product to database
 */

namespace supermarket.Middlewares.Product
{
    public static class ProductValidator
    {
        public static void Validate(string productId, string name, string characteristics, string manufacturer)
        {
            //If someone tries to add product with product_id, that already exists
            if (productId.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Невалідний код продукту");
            }
            if (DbQueries.GetProductByID(productId).Any())
            {
                throw new Exception("Товар з таким кодом вже існує");
            }

            if (name.Length is > 50 or 0)
            {
                throw new Exception("Введіть назву товару < 50 символів");
            }

            if (characteristics.Length is > 100 or 0)
            {
                throw new Exception("Введіть характеристику товару < 100 символів");
            }

            if (manufacturer.Length is > 50 or 0)
            {
                throw new Exception("Введіть виробника товару < 50 символів");
            }
        }

        public static void ValidateUpdate(string initIdProduct, string changedIdProduct, string name, string characteristics, string manufacturer)
        {

            if (initIdProduct.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Невалідний код продукту");
            }
            try
            {
                int.Parse(changedIdProduct);
            }
            catch
            {
                throw new Exception("Введіть корректний номер карти");
            }
            if (initIdProduct != changedIdProduct && DbQueries.GetProductByID(changedIdProduct).Any())
            {
                throw new Exception("Клієнт з таким номером карти вже існує");
            }
            if (name.Length is > 50 or 0)
            {
                throw new Exception("Введіть назву товару < 50 символів");
            }

            if (characteristics.Length is > 100 or 0)
            {
                throw new Exception("Введіть характеристику товару < 100 символів");
            }

            if (manufacturer.Length is > 50 or 0)
            {
                throw new Exception("Введіть виробника товару < 50 символів");
            }
        }
    }
}
