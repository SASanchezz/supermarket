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
using strPrdct = supermarket.Data.DbMaps.StoreProductMap;
using supermarket.Middlewares.StoreProduct;

namespace supermarket.Windows.ManagerMenu.StoreProducts.Changes
{
    public partial class EditStoreProductWindow : Window
    {
        //private const string _DEFAULT_UPC = "999999999909";
        //private string _storeProductId;
        public EditStoreProductWindow()
        {
            //_storeProductId = storeProductId;
            InitializeComponent();
            //FillBoxes();
        }

        ///*
        //* This method fills boxes in window with information from database
        //*/
        //private void FillBoxes()
        //{
        //    string[] product = DbQueries.GetStoreProductByID(_storeProductId)[0];

        //    upcBox.Text = product[(int)strPrdct.UPC];
        //    idProductBox.Text = product[(int)strPrdct.id_product]; ;
        //    priceBox.Text = product[(int)strPrdct.selling_price];
        //    productsNumberBox.Text = product[(int)strPrdct.products_number];
        //}

        ///*
        //* This method updates information in database from text boxes
        //*/
        //public void UpdateClick(object sender, RoutedEventArgs e)
        //{
        //    MainStoreProductWindow owner = (MainStoreProductWindow)Owner;
        //    //Renew buttons in MainProductWindow
        //    owner.DeleteOldButtons();

        //    string upc = (upcBox.Text != _storeProductId) ? upcBox.Text : _DEFAULT_UPC;
        //    string idProduct = idProductBox.Text;
        //    string price = priceBox.Text;
        //    string productsNumber = productsNumberBox.Text;

        //    string result = StoreProductValidator.ValidateNotProm(upc, idProduct, price, productsNumber);

        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    upc = upcBox.Text;
        //    string sql = string.Format("UPDATE Store_Product SET " +
        //        "UPC='{1}', id_product={2}, selling_price={3}, products_number={4} " +
        //        "WHERE UPC='{0}'",
        //        _storeProductId, upc, idProduct, price, productsNumber);
        //    DbUtils.Execute(sql);

        //    string[] fatherStoreProduct = DbQueries.GetStoreProductByID(upc)[0];
        //    string sqlProm = string.Format("UPDATE Store_Product SET " +
        //        "id_product={1}, selling_price={2}, products_number={3} " +
        //        "WHERE UPC='{0}'",
        //        fatherStoreProduct[(int)strPrdct.UPC_prom], idProduct, double.Parse(price)*0.8, productsNumber);
        //    DbUtils.Execute(sqlProm);

        //    owner.SetButtons();
        //    owner.Show();
        //    Close();
        //}
        ///*
        // * Delete production unit from database
        // */
        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    MainStoreProductWindow owner = (MainStoreProductWindow)Owner; //So we can renew buttons 
        //    owner.DeleteOldButtons();
        //    DbQueries.DeleteStoreProductByID(_storeProductId);
        //    owner.SetButtons();
        //    owner.Show();
        //    Close();
        //}

        //private void ReturnClick(object sender, RoutedEventArgs e)
        //{
        //    Owner.UpdateLayout();
        //    Owner.Show();
        //    Close();
        //}
    }
}
