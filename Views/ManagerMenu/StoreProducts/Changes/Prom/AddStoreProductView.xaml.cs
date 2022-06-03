using supermarket.Middlewares.StoreProduct;
using supermarket.Utils;
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

namespace supermarket.Views.ManagerMenu.StoreProducts.Changes.Prom
{
    public partial class AddStoreProductView : UserControl
    {
        public AddStoreProductView()
        {
            InitializeComponent();
        }

        ///*
        //* This method is realization for adding new product to database
        //*/
        //public void AddClick(object sender, RoutedEventArgs e)
        //{
        //    string upc = upcBox.Text;
        //    string idProduct = idProductBox.Text;
        //    string price = priceBox.Text;
        //    string productNumber = productsNumberBox.Text;

        //    //Validate enetered data
        //    string result = StoreProductValidator.ValidateNotProm(upc, idProduct, price, productNumber);

        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    string sql = String.Format("INSERT INTO Store_Product " +
        //        "(UPC, UPC_Prom, id_product, selling_price, products_number, promotional_product) " +
        //        "VALUES ({0}, null, {1}, {2}, {3}, 0)",
        //        upc, idProduct, price, productNumber);

        //    DbUtils.Execute(sql);

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

