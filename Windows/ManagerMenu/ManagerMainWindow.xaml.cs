﻿using System.Windows;
using supermarket.Windows.ManagerMenu.UserWindows;
using supermarket.Windows.ManagerMenu.ProductWindows;

namespace supermarket.Windows.ManagerMenu
{
    /// <summary>
    /// Interaction logic for ManagerMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
        }

        private void OpenUsersWindowClick(object sender, RoutedEventArgs e)
        {
            MainUserWindow window = new();
            window.Owner = this;
            Hide();
            window.Show();
        }

        private void OpenProductsWindowClick(object sender, RoutedEventArgs e)
        {
            MainProductWindow window = new();
            window.Owner = this;
            Hide();
            window.Show();
        }
    }
}
