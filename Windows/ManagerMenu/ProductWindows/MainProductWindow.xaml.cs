using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using supermarket.Utils;
using prdct = supermarket.Data.DbMaps.ProductMap;

namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    /// <summary>
    /// Interaction logic for MainProductWindow.xaml
    /// </summary>
    public partial class MainProductWindow : Window
    {
        public MainProductWindow()
        {
            InitializeComponent();
            SetProductButtons();
        }

        public void SetProductButtons()
        {
            List<string[]> productList = DbQueries.GetAllProducts();
            foreach (string[] product in productList)
            {
                //Delete all old buttons if they exists
                Button buttonToDel = (Button)FindName(product[(int)prdct.id_product]);
                productPanel.Children.Remove(buttonToDel);
                if (buttonToDel != null)
                {
                    UnregisterName(buttonToDel.Name);
                }


                Button button = new();

                button.Height = 20;
                button.Content = product[(int)prdct.product_name] + "  -  "
                    + product[(int)prdct.category_number];

                button.Name = product[(int)prdct.id_product];

                productPanel.Children.Add(button);
                RegisterName(button.Name, button);

                button.Click += new RoutedEventHandler(OpenProductWindow_Button);
            }

        }

        private void OpenProductWindow_Button(object sender, RoutedEventArgs e)
        {
            string productId = (sender as Button).Name.ToString(); //get id of button that is employee_id
            ManageProductWindow window = new(productId);
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void OpenAddProductWindow_Button(object sender, RoutedEventArgs e)
        {
            AddProductWindow window = new();
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}
