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
using cstmr = supermarket.Data.DbMaps.CustomerMap;

namespace supermarket.Windows.ManagerMenu
{
    public partial class ManagerClientsWindow : Window
    {
        //private const string ASC = "ASC";
        //private const string DESC = "DESC";

        //private const string SurnameUp = "Прізвище↑";
        //private const string SurnameDown = "Прізвище↓";

        ///*
        // *Sorting \ order variables
        // */
        //private string SurnameSort = ASC; // ASC as default value
        //private double percentMax = 100.0;
        //private double percentMin = 0.0;
        public ManagerClientsWindow()
        {
            InitializeComponent();
            //SetButtons(SetSortList());
        }

        /*
         * manages min slider
         */
        //private void sliderMin_ValueChanged(object sender, RoutedEventArgs e)
        //{
        //    DeleteOldButtons();
        //    percentMin = ((Slider)sender).Value;
        //    SetButtons(SetSortList());
        //}

        ///*
        // * manages max slider
        // */
        //private void sliderMax_ValueChanged(object sender, RoutedEventArgs e)
        //{
        //    DeleteOldButtons();
        //    percentMax = ((Slider)sender).Value;
        //    SetButtons(SetSortList());
        //}

        ///*
        // * manages button for name asc\desc sorting
        // */
        //private void SurnameSortClick(object sender, RoutedEventArgs e)
        //{
        //    //Renew buttons
        //    DeleteOldButtons();
        //    if ((string)((Button)sender).Content == SurnameDown)
        //    {
        //        ((Button)sender).Content = SurnameUp;
        //        SurnameSort = DESC;
        //    }
        //    else
        //    {
        //        ((Button)sender).Content = SurnameDown;
        //        SurnameSort = ASC;
        //    }
        //    SetButtons(SetSortList());
        //}

        ///*
        // * This method creates string array to pass it to dbQuery and maintain sorting\filtering
        // */
        //public string[] SetSortList()
        //{
        //    return new string[]
        //    {
        //        "percent <", percentMax.ToString(),
        //        "percent >", percentMin.ToString(),
        //        "cust_surname", SurnameSort
        //    };
        //}

        ///*
        //* This method creates list of buttons of products in MainProductWindow
        //*/
        //public void SetButtons(params string[] list)
        //{

        //    List<string[]> customerList = DbQueries.GetAllCustomers(list);
        //    foreach (string[] customer in customerList)
        //    {
        //        Button button = new();

        //        button.Height = 20;
        //        button.Content = IdUtils.Compound(customer[(int)cstmr.cust_surname],
        //            customer[(int)cstmr.phone_number]);

        //        button.Name = IdUtils.IdToName(customer[(int)cstmr.card_number]);

        //        panel.Children.Add(button);
        //        RegisterName(button.Name, button);

        //        button.Click += new RoutedEventHandler(OpenCategoryWindowClick);
        //    }
        //}

        ///*
        //* This method opens window for managing product unit
        //*/
        //private void OpenCategoryWindowClick(object sender, RoutedEventArgs e)
        //{
        //    string customerId = (sender as Button).Name.ToString();
        //    ManageClientWindow window = new(IdUtils.NameToId(customerId));
        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        ///*
        //* This method deletes all buttons of products in MainProductWindow
        //*/
        //public void DeleteOldButtons()
        //{
        //    List<string[]> customerList = DbQueries.GetAllCustomers(SetSortList());
        //    foreach (string[] customer in customerList)
        //    {
        //        //Delete all old buttons if they exists
        //        Button buttonToDel = (Button)FindName(IdUtils.IdToName(customer[(int)cstmr.card_number]));
        //        panel.Children.Remove(buttonToDel);
        //        if (buttonToDel != null)
        //        {
        //            UnregisterName(buttonToDel.Name);
        //        }
        //    }
        //}

        ///*
        //* This method opens window for managing product unit
        //*/
        //private void OpenProductWindowClick(object sender, RoutedEventArgs e)
        //{
        //    string clientId = (sender as Button).Name.ToString();
        //    ManageClientWindow window = new(IdUtils.NameToId(clientId));
        //    window.Owner = this;
        //    window.Show();
        //    Hide();
        //}

        ///*
        //* This method opens window for managing product unit
        //*/
        //private void OpenAddCategoryWindowClick(object sender, RoutedEventArgs e)
        //{
        //    AddClientWindow window = new();
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
