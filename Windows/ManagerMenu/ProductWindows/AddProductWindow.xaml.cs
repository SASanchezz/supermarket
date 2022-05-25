using System;
using System.Windows;
using supermarket.Middlewares.Product;
using supermarket.Utils;
using System.Collections.Generic;
using System.Windows.Controls;
using ctgry = supermarket.Data.DbMaps.CategoryMap;

/*
* Class concerns AddProductWindow that adds new product to database
*/
namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
            //SetCategoryList();
        }

        ///*
        //* This method sets comboBox with categories that we retrieve from database
        //*/
        //private void SetCategoryList()
        //{
        //    List<string[]> categoryList = DbQueries.GetAllCategories();
        //    ComboBox comboBox = new();
        //    comboBox.Name = "categoryList";

        //    foreach (string[] category in categoryList)
        //    {
        //        //This thing just adds row to comboBox with "category_name - category_number"
        //        comboBox.Items.Add(IdUtils.Compound(category[(int)ctgry.category_name], category[(int)ctgry.category_number]));
        //    }
        //    RegisterName(comboBox.Name, comboBox);
        //    productPanel.Children.Add(comboBox);
        //}

        ///*
        //* This method is realization for adding new product to database
        //*/
        //public void AddClick(object sender, RoutedEventArgs e)
        //{
        //    ComboBox categoryList = (ComboBox)FindName("categoryList");

        //    string productId = idBox.Text;
        //    string categoryNumber = IdUtils.Decompound(categoryList.Text)[1];
        //    string name = nameBox.Text;
        //    string characteristic = characteristicBox.Text;

        //    //Validate enetered data
        //    string result = ProductValidator.Validate(productId, name, categoryNumber, characteristic);

        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    string sql = String.Format("INSERT INTO Product " +
        //        "(id_product, category_number, product_name, characteristics) " +
        //        "VALUES ({0}, {1}, '{2}', '{3}')",
        //        productId, categoryNumber, name, characteristic);

        //    DbUtils.Execute(sql);

        //    MainProductWindow owner = (MainProductWindow)Owner;
        //    //Renew buttons in MainProductWindow (i.e. add new product window)
        //    owner.DeleteOldProductButtons();
        //    owner.SetProductButtons(owner.SetSortList());
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
