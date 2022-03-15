﻿using System;
using System.Windows;
using supermarket.Utils;
using empl = supermarket.Data.DbMaps.EmployeeMap;
using supermarket.Middlewares.SignUp;
using supermarket.Data;

namespace supermarket.Windows.ManagerMenu.UserWindows
{
    /// <summary>
    /// Interaction logic for ManageUserWindow.xaml
    /// </summary>
    public partial class ManageUserWindow : Window
    {
        private const string s_format = "yyyy-MM-dd HH:mm:ss";
        private const string s_defaultPassword = "somePassword";
        private string _employeeId;

        public ManageUserWindow(string employeeId)
        {
            _employeeId = employeeId;
            InitializeComponent();
            FillBoxes();
        }

        private void FillBoxes()
        {
            string[] employee = DbQueries.GetEmployeeByID(_employeeId)[0];

            surnameBox.Text = employee[(int)empl.empl_surname];
            nameBox.Text = employee[(int)empl.empl_name];
            patronymicBox.Text = employee[(int)empl.empl_patronumic];
            roleList.Text = Roles.roleNames[int.Parse(employee[(int)empl.empl_role])];
            salaryBox.Text = employee[(int)empl.salary];
            birthDate.Text = employee[(int)empl.date_of_birth];
            startDate.Text = employee[(int)empl.date_of_start];
            phoneNumberBox.Text = employee[(int)empl.phone_number];
            cityBox.Text = employee[(int)empl.city];
            streetBox.Text = employee[(int)empl.street];
            zipcodeBox.Text = employee[(int)empl.zipcode];
        }

        public void Update_Button(object sender, RoutedEventArgs e)
        {
            string surname = surnameBox.Text;
            string name = nameBox.Text;
            string patronymic = patronymicBox.Text;
            string role = roleList.Text;
            string salary = salaryBox.Text;
            string dateBirth = birthDate.Text;
            string dateStart = startDate.Text;
            string phoneNumber = phoneNumberBox.Text;
            string password = (passwordBox.Text == "") ? s_defaultPassword : passwordBox.Text;
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

            string[] employee = DbQueries.GetEmployeeByID(_employeeId)[0];
            //If password is default, we remain old user's password, otherwise set new
            password = (password == s_defaultPassword) ? employee[(int)empl.password] : CryptUtils.HashPassword(password);

            string sql = string.Format("UPDATE Employee SET " +
                "empl_surname='{1}', empl_name='{2}', empl_patronymic='{3}', empl_role={4}, salary={5}," +
                "date_of_birth='{6}', date_of_start='{7}', phone_number='{8}', password='{9}', city='{10}', street='{11}', zip_code='{12}'" +
                "WHERE id_employee='{0}'",
                _employeeId, surname, name, patronymic, Roles.roleKeys[role], float.Parse(salary),
                Convert.ToDateTime(dateBirth).ToString(s_format), Convert.ToDateTime(dateStart).ToString(s_format),
                phoneNumber, password, city, street, zipcode);

            DbUtils.Execute(sql);

            MainUserWindow owner = (MainUserWindow)Owner; //So we can renew buttons 
            owner.SetEmployeeButtons();
            owner.Show();
            Close();
        }

        public void Return_Button(object sender, RoutedEventArgs e)
        {
            Owner.UpdateLayout();
            Owner.Show();
            Close();
        }

        public void Delete_Button(object sender, RoutedEventArgs e)
        {
            DbQueries.DeleteEmployeeByID(_employeeId);
            MainUserWindow owner = (MainUserWindow)Owner; //So we can renew buttons 
            owner.SetEmployeeButtons();
            owner.Show();
            Close();

        }
    }
}
