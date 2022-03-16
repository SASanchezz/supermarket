using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using supermarket.Utils;
using prdct = supermarket.Data.DbMaps.ProductMap;
using ctgry = supermarket.Data.DbMaps.CategoryMap;
/*
* Class concerns MainProductWindow that contains buttons to add\ manage product unit
*/
namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    public partial class MainProductWindow : Window
    {
        public MainProductWindow()
        {
            InitializeComponent();
            SetProductButtons();
        }

        /*
        * This method deletes all buttons of products in MainProductWindow
        */
        public void DeleteOldProductButtons()
        {
            List<string[]> productList = DbQueries.GetAllProducts();
            foreach (string[] product in productList)
            {
                //Delete all old buttons if they exists
                Button buttonToDel = (Button)FindName(IdUtils.IdToName(product[(int)prdct.id_product]));
                productPanel.Children.Remove(buttonToDel);
                if (buttonToDel != null)
                {
                    UnregisterName(buttonToDel.Name);
                }
            }
        }

        /*
        * This method creates list of buttons of products in MainProductWindow
        */
        public void SetProductButtons()
        {
            List<string[]> productList = DbQueries.GetAllProducts();
            foreach (string[] product in productList)
            {
                Button button = new();

                button.Height = 20;
                button.Content = product[(int)prdct.product_name] + "  -  "
                    + DbQueries.GetCategoryByID(product[(int)prdct.category_number])[0][(int)ctgry.category_name];

                button.Name = IdUtils.IdToName(product[(int)prdct.id_product]);

                productPanel.Children.Add(button);
                RegisterName(button.Name, button);

                button.Click += new RoutedEventHandler(OpenProductWindowClick);
            }

        }

        /*
        * This method opens window for managing product unit
        */
        private void OpenProductWindowClick(object sender, RoutedEventArgs e)
        {
            string productId = (sender as Button).Name.ToString(); //get id of button that is employee_id
            ManageProductWindow window = new(IdUtils.NameToId(productId));
            window.Owner = this;
            window.Show();
            Hide();
        }

        /*
        * This method opens window for managing product unit
        */
        private void OpenAddProductWindowClick(object sender, RoutedEventArgs e)
        {
            AddProductWindow window = new();
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}
