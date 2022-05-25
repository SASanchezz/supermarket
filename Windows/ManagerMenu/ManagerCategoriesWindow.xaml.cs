using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ctgry = supermarket.Data.DbMaps.CategoryMap;

namespace supermarket.Windows.ManagerMenu
{
    
    public partial class ManagerCategoriesWindow : Window
    {
        //private const string ASC = "ASC";
        //private const string DESC = "DESC";

        //private const string NameUp = "Назва↑";
        //private const string NameDown = "Назва↓";

        /*
         *Sorting \ order variables
         */
        // private string NameSort = ASC; // ASC as default value
        public ManagerCategoriesWindow()
        {
            InitializeComponent();
           // SetButtons();
        }


        /*
         * manages button for name asc\desc sorting
         */
        //private void NameSortClick(object sender, RoutedEventArgs e)
        //{
        //    //Renew buttons
        //    DeleteOldButtons();
        //    if ((string)((Button)sender).Content == NameDown)
        //    {
        //        ((Button)sender).Content = NameUp;
        //        NameSort = DESC;
        //    }
        //    else
        //    {
        //        ((Button)sender).Content = NameDown;
        //        NameSort = ASC;
        //    }
        //    SetButtons(SetSortList());
        //}

        /*
         * This method creates string array to pass it to dbQuery and maintain sorting\filtering
         */
        //public string[] SetSortList()
        //{
        //    return new string[]
        //    {
        //        "category_name", NameSort
        //    };
        //}

        /*
        * This method creates list of buttons of products in MainProductWindow
        */
        //public void SetButtons(params string[] list)
        //{

        //    List<string[]> categoryList = DbQueries.GetAllCategories(list);
        //    foreach (string[] category in categoryList)
        //    {
        //        Button button = new();

        //        button.Height = 20;
        //        button.Content = category[(int)ctgry.category_name] + "  -  "
        //            + category[(int)ctgry.category_number];

        //        button.Name = IdUtils.IdToName(category[(int)ctgry.category_number]);

        //        panel.Children.Add(button);
        //        RegisterName(button.Name, button);

        //        button.Click += new RoutedEventHandler(OpenCategoryWindowClick);
        //    }
        //}

        /*
        * This method opens window for managing product unit
        */
        //private void OpenCategoryWindowClick(object sender, RoutedEventArgs e)
        //{
        //    string categoryId = (sender as Button).Name.ToString();
        //    ManageCategoryWindow window = new(IdUtils.NameToId(categoryId));
        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        /*
        * This method deletes all buttons of products in MainProductWindow
        */
        //public void DeleteOldButtons()
        //{
        //    List<string[]> categoryList = DbQueries.GetAllCategories(SetSortList());
        //    foreach (string[] category in categoryList)
        //    {
        //        //Delete all old buttons if they exists
        //        Button buttonToDel = (Button)FindName(IdUtils.IdToName(category[(int)ctgry.category_number]));
        //        panel.Children.Remove(buttonToDel);
        //        if (buttonToDel != null)
        //        {
        //            UnregisterName(buttonToDel.Name);
        //        }
        //    }
        //}

        /*
        * This method opens window for managing product unit
        */
        //private void OpenProductWindowClick(object sender, RoutedEventArgs e)
        //{
        //    string categoryId = (sender as Button).Name.ToString();
        //    ManageCategoryWindow window = new(IdUtils.NameToId(categoryId));
        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        /*
        * This method opens window for managing product unit
        */
        //private void OpenAddCategoryWindowClick(object sender, RoutedEventArgs e)
        //{
        //    AddCategoryWindow window = new();
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
