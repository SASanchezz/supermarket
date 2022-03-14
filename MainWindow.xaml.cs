using System.Collections.Generic;
using System.Linq;
using System.Windows;
using supermarket.Connections;
using supermarket.Middlewares.SignIn;
using supermarket.Windows.ManagerMenu;
using supermarket.Utils;

namespace supermarket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            DbUtils.SetDbConnection(); //open connection to MySQL
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

            string sql = "SELECT * FROM Category LIMIT 1;";

            List<string[]> result = DbUtils.FindAll(sql);

            if (!result.Any())
            {
                MessageBox.Show("No data in Category table");
            }else
            {
                foreach (string[] row in result)
                {
                    MessageBox.Show(row[0]);
                }
            }
        }
    }
}
