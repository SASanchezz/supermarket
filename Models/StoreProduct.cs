using System.Collections.Generic;
using supermarket.Utils;
using supermarket.Data;

namespace supermarket.Models
{
    internal static class StoreProduct
    {
        //Native
        public const int UPC = 0;
        public const int UPC_prom = 1;
        public const int id_product = 2;
        public const int selling_price = 3;
        public const int products_number = 4;
        public const int promotional_product = 5;
        //Joined
        public const int product_name = 6;

        public static List<string[]> GetAllStoreProducts()
        {
            string sql = "SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name" +
                " FROM Store_Product" +
                " LEFT JOIN Product" +
                " ON Store_Product.id_product  = Product.id_product ";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        
        public static List<string[]> GetAllStoreProductsOfProduct(string productId = "")
        {
            string whereClause = "WHERE 1";
            whereClause = productId == "" ? whereClause : whereClause += string.Format(" AND id_product={0}", productId);

            string sql = "SELECT * FROM Store_Product " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static List<string[]> GetAllStoreProducts(string isPromotional = Constants.AllString, string subUPC = "")
        {
            string whereClause = "WHERE 1";
            whereClause = isPromotional == Constants.AllString ? whereClause : whereClause +=
                $" AND promotional_product={Proms.promKeys[isPromotional]}";
            whereClause = subUPC == "" ? whereClause : whereClause += $" AND (UPC LIKE '%{subUPC}%' OR Product.product_name LIKE '%{subUPC}%')";

            string sql = string.Format("SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name" +
                " FROM Store_Product" +
                " LEFT JOIN Product" +
                " ON Store_Product.id_product  = Product.id_product " + whereClause);

            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        
        public static string[] GetStoreProductByUPC(string UPC)
        {

            string sql = $"SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name" +
                $" FROM Store_Product" +
                $" LEFT JOIN Product ON Store_Product.id_product  = Product.id_product " +
                $" WHERE UPC ='{UPC}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }
        
        public static string[] GetStoreProductByPromUPC(string UPC_Prom)
        {
            string sql = $"SELECT * FROM Store_Product WHERE UPC_Prom ='{UPC_Prom}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static List<string[]> GetFatherStoreProductLikeSubUPC(string subString = "")
        {
            string whereClause = "WHERE promotional_product = 0 ";

            whereClause = subString == "" ? whereClause : whereClause +=
                $"AND (UPC LIKE '%{subString}%' OR Product.product_name LIKE '%{subString}%')";


            string sql = "SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name " +
                "FROM (Store_Product LEFT JOIN Product " +
                "ON Store_Product.id_product = Product.id_product) " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            if (result.Count > 0) return result;

            sql = "SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name " +
                "FROM (Store_Product LEFT JOIN Product " +
                "ON Store_Product.id_product = Product.id_product) WHERE promotional_product = 0 ";
            result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static List<string[]> GetStoreProductsLikeUPCOrProductName(string subString = "")
        {
            string whereClause = "WHERE 1 ";

            whereClause = subString == "" ? whereClause : whereClause +=
                $"AND UPC LIKE '%{subString}%' OR Product.product_name LIKE '%{subString}%'";


            string sql = "SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name " +
                "FROM (Store_Product LEFT JOIN Product " +
                "ON Store_Product.id_product = Product.id_product) " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            if (result.Count > 0) return result;

            sql = "SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name " +
                "FROM Store_Product LEFT JOIN Product " +
                "ON Store_Product.id_product = Product.id_product";
            result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static int DeleteStoreProductByUPC(string UPC)
        {
            string sql = $"DELETE FROM Store_Product WHERE UPC = '{UPC}'";
            int response = DbUtils.Execute(sql);
            return response;
        }

        public static void AddNonPromStoreProduct(string upc, string idProduct, double price, string productNumber)
        {
            string sql = "INSERT INTO Store_Product " +
                         "(UPC, UPC_Prom, id_product, selling_price, products_number, promotional_product) " +
                         $"VALUES ({upc}, null, {idProduct}, {price}, {productNumber}, 0)";

            DbUtils.Execute(sql);
        }

        public static void AddPromStoreProduct(string FatherUPC, string upcProm, string amount)
        {
            string[] fatherStoreProduct = GetStoreProductByUPC(FatherUPC);

            string sqlInsert = "INSERT INTO Store_Product " +
                               "(UPC, UPC_Prom, id_product, selling_price, products_number, promotional_product) " +
                               $"VALUES ({upcProm}, null, {fatherStoreProduct[id_product]}, " +
                               $"{double.Parse(fatherStoreProduct[selling_price]) * Constants.PromPercent}, " +
                               $"{amount}, 1)";

            DbUtils.Execute(sqlInsert);

            string sqlUpdate = "UPDATE Store_Product " +
                               $"SET UPC_Prom = '{upcProm}' " +
                               $"WHERE (UPC = '{FatherUPC}')";

            DbUtils.Execute(sqlUpdate);
        }

        public static void UpdateNonPromStoreProduct(string initUpc, string changedUpc, string idProduct, double price, string productNumber)
        {
            string sql = "UPDATE Store_Product " +
                         $"SET UPC='{changedUpc}', id_product={idProduct}, " +
                         $"selling_price={price}, products_number={productNumber} " +
                         $"WHERE UPC='{initUpc}'";

            DbUtils.Execute(sql);

            string[] fatherStoreProduct = GetStoreProductByUPC(changedUpc);
            string sqlProm = "UPDATE Store_Product " +
                             $"SET id_product={idProduct}, selling_price={price * Constants.PromPercent} " +
                             $"WHERE UPC='{fatherStoreProduct[UPC_prom]}'";

            DbUtils.Execute(sqlProm);
        }

        public static void UpdatePromStoreProduct(string initUpcProm, string changedUpcProm, string changedUpcParent, string amount)
        {
            string[] oldFatherStoreProduct = GetStoreProductByPromUPC(initUpcProm);
            string[] newFatherStoreProduct = GetStoreProductByUPC(changedUpcParent);

            // Set Prom_UPC null on current father store product
            if (changedUpcParent != oldFatherStoreProduct[UPC])
            {
                string sqlSetNull = "UPDATE Store_Product " +
                                    "SET UPC_Prom=null " +
                                    $"WHERE UPC='{oldFatherStoreProduct[UPC]}'";

                DbUtils.Execute(sqlSetNull);
            }

            // Update father product table
            string sqlFather = "UPDATE Store_Product " +
                               $"SET UPC_Prom='{changedUpcProm}' " +
                               $"WHERE UPC='{changedUpcParent}'";

            DbUtils.Execute(sqlFather);

            // Update promotion product table
            string sqlProm = "UPDATE Store_Product " +
                             $"SET UPC='{changedUpcProm}', id_product={newFatherStoreProduct[id_product]}, " +
                             $"selling_price={double.Parse(newFatherStoreProduct[selling_price]) * Constants.PromPercent}, " +
                             $"products_number={amount} " +
                             $"WHERE UPC='{initUpcProm}'";

            DbUtils.Execute(sqlProm);
        }
    }
}
