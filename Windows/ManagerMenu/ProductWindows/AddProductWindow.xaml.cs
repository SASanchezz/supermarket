using System;
using System.Windows;
using supermarket.Middlewares.AddProduct;
using supermarket.Data;
using supermarket.Utils;
using System.Collections.Generic;
using System.Windows.Controls;
using ctgry = supermarket.Data.DbMaps.CategoryMap;

namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
            SetCategoryList();
        }

        private void SetCategoryList()
        {
            List<string[]> categoryList = DbQueries.GetAllCategories();
            ComboBox comboBox = new();
            comboBox.Name = "categoryList";

            foreach (string[] category in categoryList)
            {
                comboBox.Items.Add(category[(int)ctgry.category_name] + " - " + category[(int)ctgry.category_number]);
            }

            RegisterName(comboBox.Name, comboBox);
            productPanel.Children.Add(comboBox);

        }

        public void Add_Button(object sender, RoutedEventArgs e)
        {
            ComboBox categoryList = (ComboBox)FindName("categoryList");

            string productId = idBox.Text;
            int categoryNumber = int.Parse(categoryList.Text.Split(" - ")[1]);
            string name = nameBox.Text;
            string characteristic = characteristicBox.Text;

            string result = AddProductValidator.Validate(productId, name, categoryNumber, characteristic);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            string sql = String.Format("INSERT INTO Product " +
                "(id_product, category_number, product_name, characteristics) " +
                "VALUES ({0}, {1}, '{2}', '{3}')",
                productId, categoryNumber, name, characteristic);

            DbUtils.Execute(sql);

            MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
            owner.SetProductButtons();
            owner.Show();
            Close();
        }

        public void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}
