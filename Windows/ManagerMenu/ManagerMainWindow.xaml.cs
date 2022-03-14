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

        private void OpenRegisterWindow_Button(object sender, RoutedEventArgs e)
        {
            AddUserWindow window = new();
            window.Owner = this;
            Hide();
            window.Show();
        }
    }
}
