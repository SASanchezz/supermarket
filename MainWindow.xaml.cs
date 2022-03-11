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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace supermarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userLogin;
        private string userPassword;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SignIn_Button(object sender, RoutedEventArgs e)
        {
            userLogin = loginBox.Text;
            userPassword = passwordBox.Text;
            //TODO: sign in via database - 11.03.2022 
        }

        private void SignUpPage_Button(object sender, RoutedEventArgs e)
        {
            //TODO: open sign up page - 11.03.2022 
        }
    }
}
