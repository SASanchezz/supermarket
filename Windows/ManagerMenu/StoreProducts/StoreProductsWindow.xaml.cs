using System.Windows;

namespace supermarket.Windows.ManagerMenu.StoreProducts
{
    public partial class StoreProductsWindow : Window
    {
        //private const string ASC = "ASC";
        //private const string DESC = "DESC";
        //private const string nothing = "";

        //private const string NameUp = "Назва↑";
        //private const string NameDown = "Назва↓";
        //private const string NoName = "Назва";
        //private const string NumberUp = "Кількість↑";
        //private const string NumberDown = "Кількість↓";
        //private const string NoNumber = "Кількість";

        ///*
        // *Sorting \ order variables
        // */
        //private string NameSort = ASC; // ASC as default value
        //private string NumberSort = ASC; // ASC as default value
        //private List<int> CategoryFilter = new List<int> { 1, 0};
        public StoreProductsWindow()
        {
            InitializeComponent();
            //SetFiltering();
            //SetButtons();
        }

        ///*
        //* This method fill combobox for filtering products
        //*/
        //private void SetFiltering()
        //{
        //    ComboBox comboBox = (ComboBox)FindName("promFilterComboBox");
        //    comboBox.Items.Add("Категорії");
        //    comboBox.SelectedIndex = 0;

        //    StackPanel promFilterPanel = new();
        //    comboBox.Items.Add(promFilterPanel);

        //    CheckBox promoted = new();
        //    promoted.Name = IdUtils.IdToName("1");
        //    promoted.Content = "Акційний";
        //    promoted.Click += new RoutedEventHandler(CategoryFilterClick);
        //    promoted.IsChecked = true;

        //    CheckBox notPromoted = new();
        //    notPromoted.Name = IdUtils.IdToName("0");
        //    notPromoted.Content = "Неакційний";
        //    notPromoted.Click += new RoutedEventHandler(CategoryFilterClick);
        //    notPromoted.IsChecked = true;

        //    RegisterName(promoted.Name, promoted);
        //    RegisterName(notPromoted.Name, notPromoted);
        //    promFilterPanel.Children.Add(promoted);
        //    promFilterPanel.Children.Add(notPromoted);
        //}

        ///*
        // * This method add filtering to sql query
        // */
        //private void CategoryFilterClick(object sender, RoutedEventArgs e)
        //{
        //    CheckBox thisCheckBox = (CheckBox)sender;
        //    int categoryNumber = int.Parse(IdUtils.NameToId(thisCheckBox.Name));

        //    if ((bool)thisCheckBox.IsChecked)
        //    {
        //        DeleteOldButtons();
        //        CategoryFilter.Add(categoryNumber);
        //        SetButtons();
        //    }
        //    else
        //    {
        //        DeleteOldButtons();
        //        CategoryFilter.Remove(categoryNumber);
        //        SetButtons();
        //    }
        //}


        //public string[][] SetOrderList()
        //{
        //    string[][] orders = new string[2][];
        //    orders[0] = (NumberSort == "") ? new string[] {"",""} : new string[] { "Store_Product.products_number", NumberSort };
        //    orders[1] = (NameSort == "") ? new string[] {"",""} : new string[] { "Product.product_name", NameSort };

        //    return orders;
        //}
        //public string[][] SetFilterList()
        //{
        //    string[][] filters = new string[1][];
        //    string concat = string.Join(",", CategoryFilter);
        //    if (concat.Length == 0)
        //    {
        //        filters[0] = new string[] { "" };
        //        return filters;
        //    }
        //    filters[0] = new string[] { "Store_Product.promotional_product IN (", string.Join(",", CategoryFilter) + ")" };

        //    return filters;
        //}

        ///*
        // * manages button for number asc\desc sorting
        // */
        //private void NumberSortClick(object sender, RoutedEventArgs e)
        //{
        //    //Renew buttons
        //    DeleteOldButtons();
        //    if ((string)((Button)sender).Content == NumberDown)
        //    {
        //        ((Button)sender).Content = NumberUp;
        //        NumberSort = DESC;
        //    }
        //    else if ((string)((Button)sender).Content == NumberUp)
        //    {
        //        ((Button)sender).Content = NoNumber;
        //        NumberSort = nothing;
        //    } else
        //    {
        //        ((Button)sender).Content = NumberDown;
        //        NumberSort = ASC;
        //    }
        //    SetButtons();
        //}

        ///*
        // * manages button for name asc\desc sorting
        // */
        //private void NameSortClick(object sender, RoutedEventArgs e)
        //{
        //    //Renew buttons
        //    DeleteOldButtons();
        //    if ((string)((Button)sender).Content == NameDown)
        //    {
        //        ((Button)sender).Content = NameUp;
        //        NameSort = DESC;
        //    }
        //    else if ((string)((Button)sender).Content == NameUp)
        //    {
        //        ((Button)sender).Content = NoName;
        //        NameSort = nothing;
        //    }
        //    else
        //    {
        //        ((Button)sender).Content = NameDown;
        //        NameSort = ASC;
        //    }
        //    SetButtons();
        //}

        ///*
        //* This method creates list of buttons of products in MainProductWindow
        //*/
        //public void SetButtons()
        //{
        //    string filters = DbUtils.StringifyFilter(SetFilterList());
        //    string orders = DbUtils.StringifyOrder(SetOrderList());

        //    List<string[]> storeProductList = DbQueries.GetAllStoreProducts(filters, orders);
        //    foreach (string[] storeProduct in storeProductList)
        //    {
        //        Button button = new();

        //        button.Height = 20;
        //        button.Content = storeProduct[(int)strPrdct.product_name] + "  -  "
        //            + storeProduct[(int)strPrdct.products_number] + "  -  " +
        //            storeProduct[(int)strPrdct.UPC];

        //        button.Name = IdUtils.IdToName(storeProduct[(int)strPrdct.UPC]);

        //        productPanel.Children.Add(button);
        //        RegisterName(button.Name, button);

        //        button.Click += new RoutedEventHandler(OpenStoreProductWindowClick);
        //    }
        //}

        ///*
        //* This method deletes all buttons of products in MainProductWindow
        //*/
        //public void DeleteOldButtons()
        //{
        //    List<string[]> storeProductList = DbQueries.GetAllStoreProducts();
        //    foreach (string[] storeProduct in storeProductList)
        //    {
        //        //Delete all old buttons if they exists
        //        Button buttonToDel = (Button)FindName(IdUtils.IdToName(storeProduct[(int)strPrdct.UPC]));
        //        productPanel.Children.Remove(buttonToDel);
        //        if (buttonToDel != null)
        //        {
        //            UnregisterName(buttonToDel.Name);
        //        }
        //    }
        //}

        //private void OpenAddProductWindowClick(object sender, RoutedEventArgs e)
        //{
        //    AddStoreProductWindow window = new();
        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        //private void OpenAddPromProductWindowClick(object sender, RoutedEventArgs e)
        //{
        //    AddPromStoreProductWindow window = new();
        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        ///*
        //* This method opens window for managing product unit
        //*/
        //private void OpenStoreProductWindowClick(object sender, RoutedEventArgs e)
        //{
        //    string upc = (sender as Button).Name.ToString();

        //    string[] storeProduct = DbQueries.GetStoreProductByID(IdUtils.NameToId(upc))[0];
        //    bool isProm = storeProduct[(int)strPrdct.promotional_product] == "True";

        //    Window window = isProm ? new ManagePromStoreProductWindow(IdUtils.NameToId(upc))
        //        : new ManageStoreProductWindow(IdUtils.NameToId(upc));

        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        //private void ReturnClick(object sender, RoutedEventArgs e)
        //{
        //    Owner.Show();
        //    Close();
        //}
    }
}
