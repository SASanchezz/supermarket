using System.Collections.Generic;
using System.Windows;
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
            List<string[]> employeeList = DbQueries.GetAllEmployee();
            foreach (string[] employee in employeeList)
            {
                System.Windows.Controls.Button button = new();

                button.Height = 20;
                button.Content = employee[(int)empl.empl_surname] + " "
                    + employee[(int)empl.empl_name] + "  -  "
                    + employee[(int)empl.id_employee];
                
                button.Name = employee[(int) empl.id_employee];

                employeePanel.Children.Add(button);
            }
        }

        private void OpenRegisterWindow_Button(object sender, RoutedEventArgs e)
        {
            AddUserWindow window = new();
            window.Show();
            Close();
        }
    }
}
