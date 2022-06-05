using System.Linq;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.Middlewares.StoreProduct
{
    class StoreProductEditValidator
    {
        public static string ValidateNotProm(string initUPC, string changedUPC, string idProduct, string sellingPrice, string productNumber)
        {
            if (changedUPC.Any(x => !char.IsDigit(x)) || changedUPC.Length > 12)
            {
                return "Невалідний UPC";
            }
            if (initUPC != changedUPC && StrProduct.GetStoreProductByUPC(changedUPC) != null)
            {
                return "Товар з таким UPC вже існує";
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

            if (Models.Product.GetProductByID(idProduct) == null) return "Такого товару не існує";

            return ""; //Alright
        }

        public static string ValidateProm(string initUPCProm, string changedUPCProm, string changedUPCFather)
        {
            string[] oldFatherStoreProduct = StrProduct.GetStoreProductByPromUPC(initUPCProm);

            
            if (changedUPCProm.Any(x => !char.IsDigit(x)) || changedUPCProm.Length > 12)
            {
                return "Невалідний акційний UPC";
            }

            if (changedUPCFather.Any(x => !char.IsDigit(x)) || changedUPCFather.Length > 12)
            {
                return "Невалідний батьківський UPC";
            }

            if (changedUPCProm != initUPCProm && StrProduct.GetStoreProductByPromUPC(changedUPCProm) != null)
            {
                return "Акційний товар вже має свій батьківський";
            }

            if (changedUPCFather == oldFatherStoreProduct[StrProduct.UPC]) return ""; //Alright
            //Don't check other conditions because it has no sense, father upc wasn't changed
            if (StrProduct.GetStoreProductByUPC(changedUPCFather) == null)
            {
                return "UPC неакційного товару не існує";
            }
            //If such UPCFather already has UPC_Prom
            if (oldFatherStoreProduct[StrProduct.UPC] != changedUPCFather && StrProduct.GetStoreProductByUPC(changedUPCFather)[StrProduct.UPC_prom] != null)
            {
                return "Батьківський товар вже має акційний товар";
            }

            return ""; //Alright
        }

    }
}
