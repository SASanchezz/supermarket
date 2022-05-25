using supermarket.Data;
using supermarket.Utils;
using System;
using supermarket.Middlewares.SignUp;
using supermarket.Models;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    internal class AddEmployeeVM
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _role;
        private string _salary;
        private DateTime _date_of_birth;
        private DateTime _date_of_start;
        private string _phone_number;
        private string _password;
        private string _city;
        private string _street;
        private string _zipcode;

        private RelayCommand<object> _addEmployeeCommand;
        private RelayCommand<object> _closeCommand;

        public string Surname { get => _surname; set => _surname = value; }
        public string Name { get => _name; set => _name = value; }
        public string Patronymic { get => _patronymic; set => _patronymic = value; }
        public string Role { get => _role; set => _role = value; }
        public string Salary { get => _salary; set => _salary = value; }
        public DateTime Date_of_birth { get => _date_of_birth; set => _date_of_birth = value; }
        public DateTime Date_of_start { get => _date_of_start; set => _date_of_start = value; }
        public string Phone_number { get => _phone_number; set => _phone_number = value; }
        public string Password { get => _password; set => _password = value; }
        public string City { get => _city; set => _city = value; }
        public string Street { get => _street; set => _street = value; }
        public string Zipcode { get => _zipcode; set => _zipcode = value; }
        public static string[] Roles { get => Data.Roles.roleNames; }
        public RelayCommand<object> AddEmployeeCommand
        {
            get
            {
                return _addEmployeeCommand ??= new RelayCommand<object>(_ => AddEmployee(), CanExecute);
            }
        }
        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        public Action Close { get; set; }

        private void AddEmployee()
        {
            // сюда саша
            // Yes honey
            /*
            * This method sign up new user
            */

            ////Validates entered information
            string result = SignUpValidator.Validate(_surname, _name, _patronymic, _role,
                _salary, _date_of_birth, _date_of_start, _phone_number,
                _city, _street, _zipcode, _password);

            if (result.Length != 0)
            {
                //MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Employee.AddEmployee(_surname, _name, _patronymic, _role,
            _salary, Date_of_birth, Date_of_start, _phone_number,
            _password, _city, _street, _zipcode);


            Close();
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
    }
}
