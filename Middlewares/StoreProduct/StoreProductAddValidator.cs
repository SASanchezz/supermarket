using System.Linq;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.Middlewares.StoreProduct
{
    class StoreProductAddValidator
    {
        public static string ValidateNotProm(string UPC, string idProduct, string sellingPrice, string productNumber)
        {
            if (UPC.Any(x => !char.IsDigit(x)) || UPC.Length > 12)
            {
                return "Невалідний UPC";
            }
            if (StrProduct.GetStoreProductByUPC(UPC) != null)
            {
                return "Товар з таким UPC вже існує";
            }

            try
            {
                int.Parse(idProduct);
            } catch
            {
                return "Невалідний id продукту";
            }
            if (Models.Product.GetProductByID(idProduct) == null)
            {
                return "Такого товару не існує";
            }

            if(StrProduct.GetAllStoreProductsOfProduct(idProduct) != null)
            {
                return "Для даного товару вже існує товар в магазині";
            }

            try
            {
                double.Parse(sellingPrice);
            } catch
            {
                return "Введена некоректна ціна";
            }
            if (double.Parse(sellingPrice) < 0) return "Введена некоректна ціна";

            try
            {
                int.Parse(productNumber);
            }
            catch
            {
                return "Введена некоректна кількість товару";
            }

            if (int.Parse(productNumber) < 0) return "Введена некоректна кількість товару";

            return ""; //Alright
        }

        public static string ValidateProm(string UPCFather, string UPCProm)
        {

            if (UPCFather.Any(x => !char.IsDigit(x)) || UPCFather.Length > 12)
            {
                return "Невалідний батьківський UPC";
            }
            if (UPCProm.Any(x => !char.IsDigit(x)) || UPCProm.Length > 12)
            {
                return "Невалідний акційний UPC";
            }

            if (StrProduct.GetStoreProductByUPC(UPCFather)[StrProduct.promotional_product] == "True")
            {
                return "Ваш неакційний товар є акційним";
            }

            if (StrProduct.GetStoreProductByUPC(UPCProm) != null)
            {
                return "Акційний товар з таким UPC вже існує";
            }

            if (StrProduct.GetStoreProductByUPC(UPCFather) == null)
            {
                return "UPC неакційного товару не існує";
            }
            //If such UPCFather already has UPC_Prom
            if (StrProduct.GetStoreProductByUPC(UPCFather)[StrProduct.UPC_prom] != null)
            {
                return "Цей товар вже має акційний товар";
            }

            return ""; //Alright
        }

    }
}
