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
using System.Data.Common;
using MySql.Data.MySqlClient;
using supermarket.Connections;
using supermarket.Middlewares.SignIn;

namespace supermarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            DBUtils.GetDBConnection(); //open connection to MySQL
            InitializeComponent();
        }

        private void SignIn_Button(object sender, RoutedEventArgs e)
        {
            string userLogin = loginBox.Text;
            string userPassword = passwordBox.Text;
            string result = SignInValidator.Validate(userLogin, userPassword);
            if (result != "")
            {
                MessageBox.Show(result);
                return;
            }

            ManagerMainWindow window = new();
            window.Show();
            Close();
        }

        private void ChechDb_Button(object sender, RoutedEventArgs e)
        {

            string sql = "SELECT * FROM Category";

            List<string> result = DBUtils.FindAll(sql);

            if (!result.Any())
            {
                MessageBox.Show("No data in Category table");
            }else
            {
                foreach (string row in result)
                {
                    MessageBox.Show(row);
                }
            }
        }
    }
}
