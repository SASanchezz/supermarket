using System.Windows;
using supermarket.Windows.ManagerMenu.UserWindows;

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
    }
}
