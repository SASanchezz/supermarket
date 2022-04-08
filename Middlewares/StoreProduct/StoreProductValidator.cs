using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using strPrdct = supermarket.Data.DbMaps.StoreProductMap;

namespace supermarket.Middlewares.StoreProduct
{
    class StoreProductValidator
    {
        private const string _DEFAULT_UPC = "999999999909";

        public static string ValidateNotProm(string UPC, string idProduct, string sellingPrice, string productNumber)
        {
            if (UPC.Any(x => !char.IsDigit(x)) || UPC.Length > 12)
            {
                return "Невалідний UPC";
            }
            if (DbQueries.GetStoreProductByID(UPC).Any())
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
            if (!DbQueries.GetProductByID(idProduct).Any())
            {
                return "Такого товару не існує";
            }
            try
            {
                double.Parse(sellingPrice);
            } catch
            {
                return "Введена некоректна ціна";
            }

            try
            {
                int.Parse(productNumber);
            }
            catch
            {
                return "Введена некоректна кількість товару";
            }

            return ""; //Alright
        }

        public static string ValidateProm(string UPCFather, string UPCProm)
        {

            if (UPCFather.Any(x => !char.IsDigit(x)) || UPCProm.Length > 12)
            {
                return "Невалідний UPC";
            }

            if (DbQueries.GetStoreProductByID(UPCProm).Any())
            {
                return "Акційний товар з таким UPC вже існує";
            }

            if (UPCFather == _DEFAULT_UPC) return ""; //Alright
            //Don't check other conditions because it has no sense, father upc wasn't changed
            if (!DbQueries.GetStoreProductByID(UPCFather).Any())
            {
                return "UPC неакційного товару не існує";
            }
            //If such UPCFather already has UPC_Prom
            string[] storeProduct = DbQueries.GetStoreProductByID(UPCFather)[0];
            if (UPCProm != _DEFAULT_UPC && storeProduct[(int)strPrdct.UPC_prom] != null)
            {
                return "Цей товар вже має акційний товар";
            }

            return ""; //Alright
        }

    }
}
