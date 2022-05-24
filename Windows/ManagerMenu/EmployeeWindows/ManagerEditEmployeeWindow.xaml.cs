using System.Windows;

namespace supermarket.Windows.ManagerMenu.EmployeeWindows
{
    /*
    * This class concerns ManageUserWindow for managing employee
    */
    public partial class ManagerEditEmployeeWindow : Window
    {
        //private const string s_format = "yyyy-MM-dd HH:mm:ss";
        //private const string s_defaultPassword = "somePassword";
        //private string _employeeId;

        public ManagerEditEmployeeWindow()
        {
            //_employeeId = employeeId;
            InitializeComponent();
            //FillBoxes();
        }

        /*
        * This method fills boxes with information from database
        */
        //private void FillBoxes()
        //{
        //    string[] employee = DbQueries.GetEmployeeByID(_employeeId)[0];

        //    surnameBox.Text = employee[(int)empl.surname];
        //    nameBox.Text = employee[(int)empl.name];
        //    patronymicBox.Text = employee[(int)empl.patronymic];
        //    roleList.Text = Roles.roleNames[int.Parse(employee[(int)empl.role])];
        //    salaryBox.Text = employee[(int)empl.salary];
        //    birthDate.Text = employee[(int)empl.date_of_birth];
        //    startDate.Text = employee[(int)empl.date_of_start];
        //    phoneNumberBox.Text = employee[(int)empl.phone_number];
        //    cityBox.Text = employee[(int)empl.city];
        //    streetBox.Text = employee[(int)empl.street];
        //    zipcodeBox.Text = employee[(int)empl.zipcode];
        //}

        ///*
        //* This method updates information in database from boxes
        //*/


        //public void ReturnClick(object sender, RoutedEventArgs e)
        //{
        //    Owner.UpdateLayout();
        //    Owner.Show();
        //    Close();
        //}

        //public void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    EmployeesWindow owner = (EmployeesWindow)Owner; //So we can renew buttons 
        //    owner.DeleteOldEmployeeButtons();
        //    DbQueries.DeleteEmployeeByID(_employeeId);
        //    owner.SetEmployeeButtons();
        //    owner.Show();
        //    Close();

        //}
    }
}
