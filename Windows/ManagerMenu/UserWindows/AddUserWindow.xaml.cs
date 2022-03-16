using System;
using System.Windows;
using supermarket.Middlewares.SignUp;
using supermarket.Data;
using supermarket.Utils;
using supermarket.Connections;

namespace supermarket.Windows.ManagerMenu.UserWindows
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        const string s_format = "yyyy-MM-dd HH:mm:ss";
        public AddUserWindow()
        {
            InitializeComponent();
        }

        public void SignUp_Button(object sender, RoutedEventArgs e)
        {
            string surname = surnameBox.Text;
            string name = nameBox.Text;
            string patronymic = patronymicBox.Text;
            string role = roleList.Text;
            string salary = salaryBox.Text;
            string dateBirth = birthDate.Text;
            string dateStart = startDate.Text;
            string phoneNumber = phoneNumberBox.Text;
            string password = passwordBox.Text;
            string city = cityBox.Text;
            string street = streetBox.Text;
            string zipcode = zipcodeBox.Text;
            string result = SignUpValidator.Validate(surname, name, patronymic, role,
                salary, dateBirth, dateStart, phoneNumber,
                city, street, zipcode, password);

            if (result.Length != 8) //if returned not uniqueId (hope any other string is not 8 length string)
            {
                MessageBox.Show(result);
                return;
            }

            string sql = String.Format("INSERT INTO Employee " +
                "(id_employee, empl_surname, empl_name, empl_patronymic, empl_role, salary, " +
                "date_of_birth, date_of_start, phone_number, password, city, street, zip_code) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', {4}, {5}, " +
                "'{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
                result, surname, name, patronymic, Roles.roleKeys[role], float.Parse(salary),
                Convert.ToDateTime(dateBirth).ToString(s_format), Convert.ToDateTime(dateStart).ToString(s_format),
                phoneNumber, CryptUtils.HashPassword(password), city, street, zipcode);

            DbUtils.Execute(sql);

            MainUserWindow owner = (MainUserWindow)Owner; //So we can renew buttons 
            owner.DeleteOldEmployeeButtons();
            owner.SetEmployeeButtons();
            owner.Show();
            Close();
        }

        public void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}
