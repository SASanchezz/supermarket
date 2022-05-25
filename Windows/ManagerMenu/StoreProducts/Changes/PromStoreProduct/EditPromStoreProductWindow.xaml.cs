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
using strPrdct = supermarket.Data.DbMaps.StoreProductMap;

namespace supermarket.Windows.ManagerMenu.StoreProduct
{
    public partial class ManagePromStoreProductWindow : Window
    {
        //private const string _DEFAULT_UPC = "999999999909";
        //private string _promStoreProductId;
        //private string[] _fatherStoreProduct;
        public ManagePromStoreProductWindow(string promStoreProductId)
        {
            //_promStoreProductId = promStoreProductId;
            //_fatherStoreProduct = DbQueries.GetStoreProductByPromUpc(_promStoreProductId)[0]; //Find not prom product with such upc_prom
            InitializeComponent();
            //FillBoxes();
        }

        ///*
        //* This method fills boxes in window with information from database
        //*/
        //private void FillBoxes()
        //{

        //    upcPromBox.Text = _promStoreProductId;
        //    upcParentBox.Text = _fatherStoreProduct[(int)strPrdct.UPC]; ;
        //}

        ///*
        //* This method updates information in database from text boxes
        //*/
        //public void UpdateClick(object sender, RoutedEventArgs e)
        //{
        //    MainStoreProductWindow owner = (MainStoreProductWindow)Owner;
        //    //Renew buttons in MainProductWindow
        //    owner.DeleteOldButtons();

        //    string upcProm = (upcPromBox.Text != _promStoreProductId) ? upcPromBox.Text : _DEFAULT_UPC;
        //    string upcParent = (upcParentBox.Text != _fatherStoreProduct[(int)strPrdct.UPC]) ? upcParentBox.Text : _DEFAULT_UPC;

        //    string result = StoreProductValidator.ValidateProm(upcParent, upcProm);
        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    upcProm = upcPromBox.Text;
        //    upcParent = upcParentBox.Text;
        //    string[] fatherStoreProduct = DbQueries.GetStoreProductByID(upcParent)[0];


        //    // Set null on Prom_UPC in case of UPC change
        //    if (upcParentBox.Text != _fatherStoreProduct[(int)strPrdct.UPC])
        //    {
        //        string sqlSetNull = string.Format("UPDATE Store_Product SET " +
        //        "UPC_Prom=null " +
        //        "WHERE UPC_Prom='{0}'", _promStoreProductId);
        //        DbUtils.Execute(sqlSetNull);
        //    }

        //    // Update father product table
        //    string sqlFather = string.Format("UPDATE Store_Product SET " +
        //        "UPC_Prom='{1}' " +
        //        "WHERE UPC='{0}'",
        //        upcParent, upcProm);
        //    DbUtils.Execute(sqlFather);

        //    // Update promotion product table
        //    string sqlProm = string.Format("UPDATE Store_Product SET " +
        //        "UPC='{1}', id_product={2}, selling_price={3}, products_number={4} " +
        //        "WHERE UPC='{0}'",
        //        _promStoreProductId, upcProm, fatherStoreProduct[(int)strPrdct.id_product], double.Parse(fatherStoreProduct[(int)strPrdct.selling_price]) * 0.8,
        //        fatherStoreProduct[(int)strPrdct.products_number]);
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
        //    DbQueries.DeleteStoreProductByID(_promStoreProductId);

        //    string sqlDeleteProm = string.Format("UPDATE Store_Product SET " +
        //        "UPC_Prom=null " +
        //        "WHERE UPC_Prom='{0}'", _promStoreProductId);
        //    DbUtils.Execute(sqlDeleteProm);


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
