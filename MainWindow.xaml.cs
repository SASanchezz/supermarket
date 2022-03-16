using System.Collections.Generic;
using System.Linq;
using System.Windows;
using supermarket.Connections;
using supermarket.Middlewares.SignIn;
using supermarket.Windows.ManagerMenu;
using supermarket.Utils;
/*
* Main window with Signing in
*/
namespace supermarket
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            DbUtils.SetDbConnection(); //open connection to MySQL
            InitializeComponent();
        }

        private void SignInClick(object sender, RoutedEventArgs e)
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
        /*
         * Button to check your connection to db
         * if it creates MessageBox with at least something, it's OK
         */
        private void ChechDbClick(object sender, RoutedEventArgs e)
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
