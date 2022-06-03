using supermarket.Data;
using supermarket.Utils;
using System;
using supermarket.Middlewares.SignUp;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm
{
    internal class AddStoreProductVM : ViewModel
    {
        private RelayCommand<object> _addEmployeeCommand;
        private RelayCommand<object> _closeCommand;

        public Action Close { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Role { get; set; }

        public string Salary { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfStart { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Zipcode { get; set; }

        public static string[] Roles => Data.Roles.roleNames;

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

        private void AddEmployee()
        {
            // сюда саша
            // Yes honey
            /*
            * This method sign up new user
            */

            ////Validates entered information
            string result = SignUpValidator.Validate(Surname, Name, Patronymic, Role,
                Salary, DateOfBirth, DateOfStart, PhoneNumber,
                City, Street, Zipcode, Password);

            if (result.Length != 0)
            {
                //MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Employee.AddEmployee(Surname, Name, Patronymic, Role,
                Salary, DateOfBirth, DateOfStart, PhoneNumber,
                City, Street, Zipcode, Password);

            // добавить обнуление свойств

            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(Patronymic)
                && !string.IsNullOrWhiteSpace(Salary)
                && !string.IsNullOrWhiteSpace(PhoneNumber)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(City)
                && !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(Zipcode);
        }
    }
}
