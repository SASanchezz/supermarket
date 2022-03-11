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
using supermarket.connections;

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

        private void ChechDb_Button(object sender, RoutedEventArgs e)
        {
            MySqlConnection db = DBUtils.Db();

            string sql = "SELECT category_number FROM Category";

            // Создать объект Command.
            MySqlCommand cmd = new();

            // Сочетать Command с Connection.
            cmd.Connection = db;
            cmd.CommandText = sql;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Индекс (index) столбца Emp_ID в команде SQL.
                        int categoryNumberIndex = reader.GetOrdinal("category_number"); // 0

                        int categoryNumber = reader.GetInt32(categoryNumberIndex);
                        MessageBox.Show(categoryNumber.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No data in Category table");
                }
            }

        }
    }
}
