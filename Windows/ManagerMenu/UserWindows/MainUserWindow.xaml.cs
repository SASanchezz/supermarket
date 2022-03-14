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
using supermarket.Utils;
using empl = supermarket.Data.DbMaps.EmployeeMap;

namespace supermarket.Windows.ManagerMenu.UserWindows
{
    /// <summary>
    /// Interaction logic for MainUserWindow.xaml
    /// </summary>
    public partial class MainUserWindow : Window
    {
        public MainUserWindow()
        {
            InitializeComponent();
            SetEmployeeButtons();
        }

        private void SetEmployeeButtons()
        {
            List<string> employeeList = DbQueries.GetAllEmployee();
            foreach (string employee in employeeList)
            {
                System.Windows.Controls.Button button = new();

                button.Height = 20;
                button.Content = employee.Split(',')[(int)empl.empl_surname] + " "
                    + employee.Split(',')[(int)empl.empl_name] + "  -  "
                    + employee.Split(',')[(int)empl.id_employee];
                
                button.Name = employee.Split(',')[(int) empl.id_employee];

                employeePanel.Children.Add(button);
            }
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
