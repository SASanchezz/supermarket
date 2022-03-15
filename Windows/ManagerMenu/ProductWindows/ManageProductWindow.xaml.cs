using System;
using System.Windows;
using supermarket.Utils;
using prdct = supermarket.Data.DbMaps.ProductMap;
using supermarket.Middlewares.AddProduct;
using supermarket.Data;
using System.Windows.Controls;

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
            FillBoxes();
        }

        private void FillBoxes()
        {
            string[] product = DbQueries.GetEmployeeByID(_productId)[(int)prdct.id_product];

            ComboBox categoryList = (ComboBox)FindName("categoryList");

            idBox.Text = product[(int)prdct.id_product];
            categoryList.Text = product[(int)prdct.category_number];
            nameBox.Text = product[(int)prdct.product_name];
            characteristicBox.Text = product[(int)prdct.characteristics];
        }

        public void Update_Button(object sender, RoutedEventArgs e)
        {
            ComboBox categoryList = (ComboBox)FindName("categoryList");

            string idProduct = idBox.Text;
            int categoryNumber = int.Parse(categoryList.Text.Split(" - ")[1]);
            string name = nameBox.Text;
            string characteristic = characteristicBox.Text;

            string result = AddProductValidator.Validate(idProduct, name, categoryNumber, characteristic);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            string sql = string.Format("UPDATE Employee SET " +
                "id_product='{0}', category_number={1}, product_name='{2}', characteristics={3}" +
                "WHERE id_product = {0}",
                idProduct, categoryNumber, name, characteristic);

            DbUtils.Execute(sql);

            MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
            owner.SetProductButtons();
            owner.Show();
            Close();
        }

        public void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.UpdateLayout();
            Owner.Show();
            Close();
        }

        public void Delete_Button(object sender, RoutedEventArgs e)
        {
            DbQueries.DeleteEmployeeByID(_productId);
            MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
            owner.SetProductButtons();
            owner.Show();
            Close();

        }
    }
}
