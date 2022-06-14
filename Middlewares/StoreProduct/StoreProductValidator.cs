using System;
using System.Linq;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.Middlewares.StoreProduct
{
    public static class StoreProductValidator
    {
        public static void ValidateInsertNotProm(string UPC, string idProduct, string sellingPrice, string productNumber)
        {
            if (UPC.Any(x => !char.IsDigit(x)) || UPC.Length > 12)
            {
                throw new Exception("Невалідний UPC");
            }
            
            if (StrProduct.GetStoreProductByUPC(UPC) != null)
            {
                throw new Exception("Товар з таким UPC вже існує");
            }

            try
            {
                int.Parse(idProduct);
            } 
            catch
            {
                throw new Exception("Невалідний id продукту");
            }
            
            if (Models.Product.GetProductByID(idProduct) == null)
            {
                throw new Exception("Такого товару не існує");
            }

            if (StrProduct.GetAllStoreProductsOfProduct(idProduct) != null)
            {
                throw new Exception("Для даного товару вже існує товар в магазині");
            }

            try
            {
                double.Parse(sellingPrice);
            } 
            catch
            {
                throw new Exception("Введена некоректна ціна");
            }

            if (double.Parse(sellingPrice) < 0)
            {
                throw new Exception("Введена некоректна ціна");
            }

            try
            {
                int.Parse(productNumber);
            }
            catch
            {
                throw new Exception("Введена некоректна кількість товару");
            }

            if (int.Parse(productNumber) < 0)
            {
                throw new Exception("Введена некоректна кількість товару");
            }
        }
        
        public static void ValidateInsertProm(string UPCFather, string UPCProm)
        {
            if (UPCFather.Any(x => !char.IsDigit(x)) || UPCFather.Length > 12)
            {
                throw new Exception("Невалідний батьківський UPC");
            }
            
            if (UPCProm.Any(x => !char.IsDigit(x)) || UPCProm.Length > 12)
            {
                throw new Exception("Невалідний акційний UPC");
            }

            if (StrProduct.GetStoreProductByUPC(UPCFather)[StrProduct.promotional_product] == "True")
            {
                throw new Exception("Ваш неакційний товар є акційним");
            }

            if (StrProduct.GetStoreProductByUPC(UPCProm) != null)
            {
                throw new Exception("Акційний товар з таким UPC вже існує");
            }

            if (StrProduct.GetStoreProductByUPC(UPCFather) == null)
            {
                throw new Exception("UPC неакційного товару не існує");
            }
            //If such UPCFather already has UPC_Prom
            if (StrProduct.GetStoreProductByUPC(UPCFather)[StrProduct.UPC_prom] != null)
            {
                throw new Exception("Цей товар вже має акційний товар");
            }
        }
        
        public static void ValidateUpdateNotProm(string initUPC, string changedUPC, string idProduct, string sellingPrice, string productNumber)
        {
            if (changedUPC.Any(x => !char.IsDigit(x)) || changedUPC.Length > 12)
            {
                throw new Exception("Невалідний UPC");
            }
            if (initUPC != changedUPC && StrProduct.GetStoreProductByUPC(changedUPC) != null)
            {
                throw new Exception("Товар з таким UPC вже існує");
            }

            try
            {
                double.Parse(sellingPrice);
            } 
            catch
            {
                throw new Exception("Введена некоректна ціна");
            }

            if (double.Parse(sellingPrice) < 0)
            {
                throw new Exception("Введена некоректна ціна");
            }

            try
            {
                int.Parse(productNumber);
            }
            catch
            {
                throw new Exception("Введена некоректна кількість товару");
            }

            if (int.Parse(productNumber) < 0)
            {
                throw new Exception("Введена некоректна кількість товару");
            }

            if (Models.Product.GetProductByID(idProduct) == null)
            {
                throw new Exception("Такого товару не існує");
            }
        }

        public static void ValidateUpdateProm(string initUPCProm, string changedUPCProm, string changedUPCFather)
        {
            string[] oldFatherStoreProduct = StrProduct.GetStoreProductByPromUPC(initUPCProm);

            
            if (changedUPCProm.Any(x => !char.IsDigit(x)) || changedUPCProm.Length > 12)
            {
                throw new Exception("Невалідний акційний UPC");
            }

            if (changedUPCFather.Any(x => !char.IsDigit(x)) || changedUPCFather.Length > 12)
            {
                throw new Exception("Невалідний батьківський UPC");
            }

            if (changedUPCProm != initUPCProm && StrProduct.GetStoreProductByPromUPC(changedUPCProm) != null)
            {
                throw new Exception("Акційний товар вже має свій батьківський");
            }

            if (changedUPCFather == oldFatherStoreProduct[StrProduct.UPC]) 
            {
                return; //Alright
            }
            
            //Don't check other conditions because it has no sense, father upc wasn't changed
            if (StrProduct.GetStoreProductByUPC(changedUPCFather) == null)
            {
                throw new Exception("UPC неакційного товару не існує");
            }
            //If such UPCFather already has UPC_Prom
            if (oldFatherStoreProduct[StrProduct.UPC] != changedUPCFather && StrProduct.GetStoreProductByUPC(changedUPCFather)[StrProduct.UPC_prom] != null)
            {
                throw new Exception("Батьківський товар вже має акційний товар");
            }
        }
    }
}