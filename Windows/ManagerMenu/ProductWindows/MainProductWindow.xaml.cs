using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using supermarket.Utils;
using Category = supermarket.Models.Category;
using prdct = supermarket.Data.DbMaps.ProductMap;
using ctgry = supermarket.Data.DbMaps.CategoryMap;
using System;
/*
* Class concerns MainProductWindow that contains buttons to add\ manage product unit
*/
namespace supermarket.Windows.ManagerMenu.ProductWindows
{
    public partial class MainProductWindow : Window
    {
        private const string ASC = "ASC";
        private const string DESC = "DESC";
        private const string nothing = "";

        private const string NameUp = "Назва↑";
        private const string NameDown = "Назва↓";
        private const string CategoryUp = "Категорія↑";
        private const string CategoryDown = "Категорія↓";
        private const string NoCategory = "Категорія";

        /*
         *Sorting \ order variables
         */
        private string NameSort = ASC; // ASC as default value
        private string CategorySort = ASC; // ASC as default value
        private List<int> CategoryFilter = new(); // "" - default value as all categories
        public MainProductWindow()
        {
            InitializeComponent();
            SetFiltering();
            SetProductButtons(SetSortList());
        }

        /*
        * This method fill combobox for filtering products
        */
        private void SetFiltering()
        {
            List<string[]> allCategories = DbQueries.GetAllCategories();

            ComboBox comboBox = (ComboBox)FindName("categoryFilterComboBox");
            comboBox.Items.Add("Категорії");
            comboBox.SelectedIndex = 0;

            StackPanel categoryFilterPanel = new();
            comboBox.Items.Add(categoryFilterPanel);

            foreach (string[] category in allCategories)
            {
                CheckBox checkBox = new();
                checkBox.Name = IdUtils.IdToName(category[(int)ctgry.category_number]);
                checkBox.Content = category[(int)ctgry.category_name];
                checkBox.Click += new RoutedEventHandler(CategoryFilterClick);

                RegisterName(checkBox.Name, checkBox);
                categoryFilterPanel.Children.Add(checkBox);
            }
        }

        /*
         * This method add filtering to sql query
         */
        private void CategoryFilterClick(object sender, RoutedEventArgs e)
        {
            CheckBox thisCheckBox = (CheckBox)sender;
            int categoryNumber = int.Parse(IdUtils.NameToId(thisCheckBox.Name));

            if ((bool)thisCheckBox.IsChecked)
            {
                DeleteOldProductButtons();
                CategoryFilter.Add(categoryNumber);
                SetProductButtons(SetSortList());
            }
            else
            {
                DeleteOldProductButtons();
                CategoryFilter.Remove(categoryNumber);
                SetProductButtons(SetSortList());
            }
        }


        /*
         * This method creates string array to pass it to dbQuery and maintain sorting\filtering
         */
        public string[] SetSortList()
        {
            return new string[]
            {
                "category_number", string.Join(',', CategoryFilter),
                "category_name", CategorySort,
                "product_name", NameSort,
            };
        }

        /*
         * manages button for category asc\desc sorting
         */
        private void CategorySortClick(object sender, RoutedEventArgs e)
        {
            //Renew buttons
            DeleteOldProductButtons();
            if ((string)((Button)sender).Content == CategoryDown)
            {
                ((Button)sender).Content = CategoryUp;
                CategorySort = DESC;
            }
            else if ((string)((Button)sender).Content == CategoryUp)
            {
                ((Button)sender).Content = NoCategory;
                CategorySort = nothing;
            }
            else
            {
                ((Button)sender).Content = CategoryDown;
                CategorySort = ASC;
            }
            SetProductButtons(SetSortList());
        }

        /*
         * manages button for name asc\desc sorting
         */
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
