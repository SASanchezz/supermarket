using System;
using System.Windows;
using supermarket.Utils;
using prdct = supermarket.Data.DbMaps.ProductMap;
using ctgry = supermarket.Data.DbMaps.CategoryMap;
using supermarket.Middlewares.AddProduct;
using supermarket.Data;
using System.Windows.Controls;
using System.Collections.Generic;
/*
* Class concerns ManageProductWindow that contains realiztion for managing products
*/
namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    public partial class ManageProductWindow : Window
    {
        private string _productId;

        public ManageProductWindow(string productId)
        {
            _productId = productId;
            InitializeComponent();
            SetCategoryList();
            FillBoxes();
        }

        /*
        * This method sets comboBox with categories that we retrieve from database
        * (the same as in AddProductWindow. Probably, we should make some inheritance later)
        */
        private void SetCategoryList()
        {
            List<string[]> categoryList = DbQueries.GetAllCategories();
            ComboBox comboBox = new();
            comboBox.Name = "categoryList";

            foreach (string[] category in categoryList)
            {
                comboBox.Items.Add(IdUtils.Compound(category[(int)ctgry.category_name], category[(int)ctgry.category_number]));
            }

            RegisterName(comboBox.Name, comboBox);
            productPanel.Children.Add(comboBox);
        }

        /*
        * This method fills boxes in window with information from database
        */
        private void FillBoxes()
        {
            string[] product = DbQueries.GetProductByID(_productId)[(int)prdct.id_product];

            ComboBox categoryList = (ComboBox)FindName("categoryList");
            string[] categoryRow = DbQueries.GetCategoryByID(product[(int)prdct.category_number])[0];

            idBox.Text = product[(int)prdct.id_product];
            categoryList.Text =  IdUtils.Compound(categoryRow[(int)ctgry.category_name], categoryRow[(int)ctgry.category_number]);
            nameBox.Text = product[(int)prdct.product_name];
            characteristicBox.Text = product[(int)prdct.characteristics];
        }

        /*
        * This method updates information in database from text boxes
        */
        public void UpdateClick(object sender, RoutedEventArgs e)
        {
            ComboBox categoryList = (ComboBox)FindName("categoryList");

            string idProduct = idBox.Text;
            string categoryNumber = IdUtils.Decompound(categoryList.Text)[1];
            string name = nameBox.Text;
            string characteristic = characteristicBox.Text;

            string result = AddProductValidator.Validate("anyId", name, categoryNumber, characteristic);


            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            string sql = string.Format("UPDATE Product SET " +
                "id_product={0}, category_number={1}, product_name='{2}', characteristics='{3}' " +
                "WHERE id_product={0}",
                idProduct, categoryNumber, name, characteristic);

            DbUtils.Execute(sql);

            MainProductWindow owner = (MainProductWindow)Owner;
            //Renew buttons in MainProductWindow
            owner.DeleteOldProductButtons();
            owner.SetProductButtons();
            owner.Show();
            Close();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Owner.UpdateLayout();
            Owner.Show();
            Close();
        }

        /*
         * Delete production unit from database
         */
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
            owner.DeleteOldProductButtons();
            DbQueries.DeleteProductByID(_productId);
            owner.SetProductButtons();
            owner.Show();
            Close();
        }
    }
}
