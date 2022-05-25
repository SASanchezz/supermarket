using supermarket.Middlewares.StoreProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using supermarket.Utils;
using strPrdct = supermarket.Data.DbMaps.StoreProductMap;

namespace supermarket.Windows.ManagerMenu.StoreProduct.Changes.PromStoreProduct
{
    public partial class AddPromStoreProductWindow : Window
    {
        public AddPromStoreProductWindow()
        {
            InitializeComponent();
        }

        ///*
        //* This method is realization for adding new product to database
        //*/
        //public void AddClick(object sender, RoutedEventArgs e)
        //{
        //    string upcProm = upcPromBox.Text;
        //    string upc = upcParentBox.Text;

        //    //Validate enetered data
        //    string result = StoreProductValidator.ValidateProm(upc, upcProm);

        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }
        //    string[] fatherStoreProduct = DbQueries.GetStoreProductByID(upc)[0];

        //    string sqlInsert = String.Format("INSERT INTO Store_Product " +
        //        "(UPC, UPC_Prom, id_product, selling_price, products_number, promotional_product) " +
        //        "VALUES ({0}, null, {1}, {2}, {3}, 1)",
        //        upcProm, fatherStoreProduct[(int)strPrdct.id_product],
        //        double.Parse(fatherStoreProduct[(int)strPrdct.selling_price]) * 0.8,
        //        fatherStoreProduct[(int)strPrdct.products_number]);

        //    DbUtils.Execute(sqlInsert);

        //    string sqlUpdate = String.Format("UPDATE Store_Product " +
        //        "SET UPC_Prom = '{1}' " +
        //        "WHERE (UPC = '{0}')",
        //        upc, upcProm);

        //    DbUtils.Execute(sqlUpdate);


        //    MainStoreProductWindow owner = (MainStoreProductWindow)Owner;
        //    //Renew buttons in MainProductWindow (i.e. add new product window)
        //    owner.DeleteOldButtons();
        //    owner.SetButtons();
        //    owner.Show();
        //    Close();
        //}

        //public void ReturnClick(object sender, RoutedEventArgs e)
        //{
        //    Owner.Show();
        //    Close();
        //}
    }
}
