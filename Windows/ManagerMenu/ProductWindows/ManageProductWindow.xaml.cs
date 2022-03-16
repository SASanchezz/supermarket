using System;
using System.Windows;
using supermarket.Utils;
using prdct = supermarket.Data.DbMaps.ProductMap;
using ctgry = supermarket.Data.DbMaps.CategoryMap;
using supermarket.Middlewares.AddProduct;
using supermarket.Data;
using System.Windows.Controls;
using System.Collections.Generic;

namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    /// <summary>
    /// Interaction logic for ManageProductWindow.xaml
    /// </summary>
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

        public void Update_Button(object sender, RoutedEventArgs e)
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

            MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
            owner.SetProductButtons();
            owner.Show();
            Close();
        }

        private void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.UpdateLayout();
            Owner.Show();
            Close();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            DbQueries.DeleteProductByID(_productId);
            MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
            owner.SetProductButtons();
            owner.Show();
            Close();
        }
    }
}
