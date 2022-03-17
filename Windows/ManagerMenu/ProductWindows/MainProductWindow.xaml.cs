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
        private const string ASC = "ASC";
        private const string DESC = "DESC";
        private const string NameUp = "Назва↑";
        private const string NameDown = "Назва↓";

        /*
         *Sorting \ order variables
         */
        private string NameSort = ASC; // ASC as default value
        private string CategorySort = ASC; // ASC as default value
        private string CategoryFilter = ""; // "" - default value as all categories
        public MainProductWindow()
        {
            InitializeComponent();
            SetSorting();
            SetProductButtons(SetSortList());
        }

        /*
        * This method creates button and field for sorting and filtering product buttons
        */
        private void SetSorting()
        {
            Button button = new();

            Grid.SetRow(button, 0);
            button.Height = 24;
            button.Width = 64;
            button.FontSize = 12;
            button.Margin = new Thickness(0, 0, 200, 0);
            button.HorizontalAlignment = HorizontalAlignment.Center;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.Content = NameDown;

            rootGrid.Children.Add(button);

            button.Click += new RoutedEventHandler(NameSortClick);
        }

        public string[] SetSortList()
        {
            return new string[]
            {
                "category_number", CategoryFilter,
                "category_number", CategorySort,
                "product_name", NameSort,
            };
        }

        private void NameSortClick(object sender, RoutedEventArgs e)
        {
            //Renew buttons
            DeleteOldProductButtons();
            if ((string) ((Button)sender).Content == NameDown)
            {
                ((Button)sender).Content = NameUp;
                NameSort = DESC;
            }
            else
            {
                ((Button)sender).Content = NameDown;
                NameSort = ASC;
            }
            SetProductButtons(SetSortList());
        }

        /*
        * This method creates list of buttons of products in MainProductWindow
        */
        public void SetProductButtons(params string[] list)
        {

            List<string[]> productList = DbQueries.GetAllProducts(list);
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
        * This method deletes all buttons of products in MainProductWindow
        */
        public void DeleteOldProductButtons()
        {
            List<string[]> productList = DbQueries.GetAllProducts(SetSortList());
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
