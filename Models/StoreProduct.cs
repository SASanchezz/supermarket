using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static List<string[]> GetAllEmployee()
        {
            string sql = "SELECT * FROM Employee";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static List<string[]> GetAllStoreProducts(int promotional = -1)
        {
            string promotionalFilter = promotional == -1 ? "" : string.Format("WHERE promotional={0}", string.Join("',", promotional));

            string sql = string.Format("SELECT UPC, UPC_prom, Store_Product.id_product, selling_price, products_number, promotional_product, Product.product_name" +
                " FROM Store_Product" +
                " LEFT JOIN Product" +
                " ON Store_Product.id_product  = Product.id_product " + promotionalFilter);

            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        public static string[] GetStoreProductByUPC(string UPC)
        {
            string sql = string.Format("SELECT * FROM Store_Product WHERE UPC ='{0}'", UPC);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }
        public static string[] GetStoreProductByPromUPC(string UPC_Prom)
        {
            string sql = string.Format("SELECT * FROM Store_Product WHERE UPC_Prom ='{0}'", UPC_Prom);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static void DeleteStoreProductByUPC(string UPC)
        {
            string sql = string.Format("DELETE FROM Store_Product WHERE UPC = '{0}'", UPC);
            DbUtils.Execute(sql);
        }

        public static void AddNonPromStoreProduct(string upc, string idProduct, double price, string productNumber)
        {
            string sql = string.Format("INSERT INTO Store_Product " +
                "(UPC, UPC_Prom, id_product, selling_price, products_number, promotional_product) " +
                "VALUES ({0}, null, {1}, {2}, {3}, 0)",
                upc, idProduct, price, productNumber);

            DbUtils.Execute(sql);
        }

        public static void AddPromStoreProduct(string upc, string upcProm)
        {
            string[] fatherStoreProduct = GetStoreProductByUPC(upc);

            string sqlInsert = string.Format("INSERT INTO Store_Product " +
                "(UPC, UPC_Prom, id_product, selling_price, products_number, promotional_product) " +
                "VALUES ({0}, null, {1}, {2}, {3}, 1)",
                upcProm,
                fatherStoreProduct[id_product],
                double.Parse(fatherStoreProduct[selling_price]) * 0.8,
                fatherStoreProduct[products_number]);

            DbUtils.Execute(sqlInsert);

            string sqlUpdate = string.Format("UPDATE Store_Product " +
                "SET UPC_Prom = '{1}' " +
                "WHERE (UPC = '{0}')",
                upc, upcProm);

            DbUtils.Execute(sqlUpdate);
        }

        public static void UpdateNonPromStoreProduct(string initUpc, string changedUpc, string idProduct, double price, string productNumber)
        {
            string sql = string.Format("UPDATE Store_Product SET " +
                "UPC='{1}', id_product={2}, selling_price={3}, products_number={4} " +
                "WHERE UPC='{0}'",
                initUpc, changedUpc, idProduct, price, productNumber);
            DbUtils.Execute(sql);

            string[] fatherStoreProduct = GetStoreProductByUPC(changedUpc);
            string sqlProm = string.Format("UPDATE Store_Product SET " +
                "id_product={1}, selling_price={2}, products_number={3} " +
                "WHERE UPC='{0}'",
                fatherStoreProduct[UPC_prom], idProduct, price*0.8, productNumber);
            DbUtils.Execute(sqlProm);
        }



        public static void UpdatePromStoreProduct(string changedUpcParent, string initUpcProm, string changedUpcProm)
        {
            string[] oldFatherStoreProduct = GetStoreProductByPromUPC(initUpcProm);
            string[] newFatherStoreProduct = GetStoreProductByUPC(initUpcProm);


            // Set Prom_UPC null on current father store product
            if (changedUpcParent != oldFatherStoreProduct[UPC])
            {
                string sqlSetNull = string.Format("UPDATE Store_Product SET " +
                "UPC_Prom=null " +
                "WHERE UPC_Prom='{0}'", initUpcProm);
                DbUtils.Execute(sqlSetNull);
            }

            // Update father product table
            string sqlFather = string.Format("UPDATE Store_Product SET " +
                "UPC_Prom='{1}' " +
                "WHERE UPC='{0}'",
                changedUpcParent, changedUpcProm);
            DbUtils.Execute(sqlFather);

            // Update promotion product table
            string sqlProm = string.Format("UPDATE Store_Product SET " +
                "UPC='{1}', id_product={2}, selling_price={3}, products_number={4} " +
                "WHERE UPC='{0}'",
                initUpcProm, changedUpcProm, newFatherStoreProduct[id_product], double.Parse(newFatherStoreProduct[selling_price]) * 0.8,
                newFatherStoreProduct[products_number]);
            DbUtils.Execute(sqlProm);
        }
    }
}
