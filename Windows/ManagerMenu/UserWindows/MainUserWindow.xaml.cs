using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using supermarket.Utils;
using empl = supermarket.Data.DbMaps.EmployeeMap;

namespace supermarket.Windows.ManagerMenu.UserWindows
{
    /*
    * This class concerns MainUserWindow, that has buttons to add\ manage employee
    */
    public partial class MainUserWindow : Window
    {
        public MainUserWindow()
        {
            InitializeComponent();
            SetEmployeeButtons();
        }

        /*
        * This method deletes old buttons with employee
        */
        public void DeleteOldEmployeeButtons()
        {
            List<string[]> employeeList = DbQueries.GetAllEmployee();
            foreach (string[] employee in employeeList)
            {
                //Delete all old buttons if they exists
                Button buttonToDel = (Button)FindName(IdUtils.IdToName(employee[(int)empl.id_employee]));
                employeePanel.Children.Remove(buttonToDel);
                if (buttonToDel != null)
                {
                    UnregisterName(buttonToDel.Name);
                }
            }
        }

        /*
        * This method creates buttons with for employee in MainUserWindow
        */
        public void SetEmployeeButtons()
        {
            List<string[]> employeeList = DbQueries.GetAllEmployee();
            foreach (string[] employee in employeeList)
            {
                Button button = new();

                button.Height = 20;
                button.Content = employee[(int)empl.empl_surname] + " "
                    + employee[(int)empl.empl_name] + "  -  "
                    + employee[(int)empl.id_employee];
                
                button.Name = IdUtils.IdToName(employee[(int) empl.id_employee]);

                employeePanel.Children.Add(button);
                RegisterName(button.Name, button);

                button.Click += new RoutedEventHandler(OpenUserWindow_Button);
            }

        }

        /*
        * This method opens window for managing user
        */
        private void OpenUserWindow_Button(object sender, RoutedEventArgs e)
        {
            string employeeId = (sender as Button).Name.ToString(); //get id of button that is employee_id
            ManageUserWindow window = new(IdUtils.NameToId(employeeId));
            window.Owner = this;
            window.Show();
            Hide();
        }

        /*
        * This method opens window for registering new user
        */
        private void OpenRegisterWindow_Button(object sender, RoutedEventArgs e)
        {
            AddUserWindow window = new();
            window.Owner = this;
            window.Show();
            Hide();
        }

        private void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}
