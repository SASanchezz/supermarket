using supermarket.Data;
using supermarket.Utils;
using System;

namespace supermarket.ViewModels.ManagerMenu.EmployeesViewModels
{
    internal class ManagerAddEmployeeViewModel
    {
        private string _name;
        private string _surname;
        private string _patronymic;
        private int _role;
        private string _salary;
        private DateTime _date_of_birth;
        private DateTime _date_of_start;
        private string _phone_number;
        private string _password;
        private string _city;
        private string _street;
        private string _zipcode;

        private RelayCommand<object> _addEmployeeCommand;

        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public string Patronymic { get => _patronymic; set => _patronymic = value; }
        public int Role { get => _role; set => _role = value; }
        public string Salary { get => _salary; set => _salary = value; }
        public DateTime Date_of_birth { get => _date_of_birth; set => _date_of_birth = value; }
        public DateTime Date_of_start { get => _date_of_start; set => _date_of_start = value; }
        public string Phone_number { get => _phone_number; set => _phone_number = value; }
        public string Password { get => _password; set => _password = value; }
        public string City { get => _city; set => _city = value; }
        public string Street { get => _street; set => _street = value; }
        public string Zipcode { get => _zipcode; set => _zipcode = value; }
        public RelayCommand<object> AddEmployeeCommand
        {
            get
            {
                return _addEmployeeCommand ??= new RelayCommand<object>(_ => AddEmployee(), CanExecute);
            }
        }
        public Action Close { get; set; }

        private void AddEmployee()
        {
            // сюда саша
            /*
        * This method sign up new user
        */
            //public void SignUpClick(object sender, RoutedEventArgs e)
            //{
            //    string surname = surnameBox.Text;
            //    string name = nameBox.Text;
            //    string patronymic = patronymicBox.Text;
            //    string role = roleList.Text;
            //    string salary = salaryBox.Text;
            //    string dateBirth = birthDate.Text;
            //    string dateStart = startDate.Text;
            //    string phoneNumber = phoneNumberBox.Text;
            //    string password = passwordBox.Text;
            //    string city = cityBox.Text;
            //    string street = streetBox.Text;
            //    string zipcode = zipcodeBox.Text;

            //    //Validates entered information
            //    string result = SignUpValidator.Validate(surname, name, patronymic, role,
            //        salary, dateBirth, dateStart, phoneNumber,
            //        city, street, zipcode, password);

            //    if (result.Length != 8) //if returned not uniqueId (hope any other string is not 8 length string)
            //    {
            //        MessageBox.Show(result);
            //        return;
            //    }

            //    //Query to insert new employee
            //    string sql = String.Format("INSERT INTO Employee " +
            //        "(id_employee, empl_surname, empl_name, empl_patronymic, empl_role, salary, " +
            //        "date_of_birth, date_of_start, phone_number, password, city, street, zip_code) " +
            //        "VALUES ('{0}', '{1}', '{2}', '{3}', {4}, {5}, " +
            //        "'{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')",
            //        result, surname, name, patronymic, Roles.roleKeys[role], float.Parse(salary),
            //        Convert.ToDateTime(dateBirth).ToString(s_format), Convert.ToDateTime(dateStart).ToString(s_format),
            //        phoneNumber, CryptUtils.HashPassword(password), city, street, zipcode);

            //    DbUtils.Execute(sql);

            //    EmployeesWindow owner = (EmployeesWindow)Owner;
            //    //Renew buttons in MainUserWindow
            //      owner.DeleteOldEmployeeButtons();
            //      owner.SetEmployeeButtons();
            //    owner.Show();
            //    Close();
            //}
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(Patronymic)
                && !string.IsNullOrWhiteSpace(Salary)
                && !string.IsNullOrWhiteSpace(Phone_number)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(City)
                && !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(Zipcode);
        }

        private void UpdateData()
        {

        }
    }
}
