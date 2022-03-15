using System.Windows;
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

        private void OpenUsersWindow_Button(object sender, RoutedEventArgs e)
        {
            MainUserWindow window = new();
            window.Owner = this;
            Hide();
            window.Show();
        }

        private void OpenProductsWindow_Button(object sender, RoutedEventArgs e)
        {
            MainProductWindow window = new();
            window.Owner = this;
            Hide();
            window.Show();
        }
    }
}
