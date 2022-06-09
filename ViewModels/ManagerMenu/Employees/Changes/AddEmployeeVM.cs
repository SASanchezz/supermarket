using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Middlewares.SignUp;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    internal class AddEmployeeVM : ViewModel
    {
        private string _id;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _role;
        private string _salary;
        private DateTime _dateOfBirth;
        private DateTime _dateOfStart;
        private string _phoneNumber;
        private string _password;
        private string _city;
        private string _street;
        private string _zipcode;
        
        public AddEmployeeVM()
        {
            AddEmployeeCommand = new RelayCommand<object>(_ => AddEmployee(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }

        public string Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfStart
        {
            get => _dateOfStart;
            set
            {
                _dateOfStart = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }

        public string Zipcode
        {
            get => _zipcode;
            set
            {
                _zipcode = value;
                OnPropertyChanged();
            }
        }
        
        public static string[] Roles => Data.Roles.roleNames;

        public RelayCommand<object> AddEmployeeCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public void Reset()
        {
            Id = null;
            Surname = null;
            Name = null;
            Patronymic = null;
            Role = null;
            Salary = null;
            DateOfBirth = DateTime.Now;
            DateOfStart = DateTime.Now;
            PhoneNumber = null;
            Password = null;
            City = null;
            Street = null;
            Zipcode = null;
        }

        private void AddEmployee()
        {
            ////Validates entered information
            string result = SignUpValidator.Validate(Surname, Name, Patronymic, Role,
                Salary, DateOfBirth, DateOfStart, PhoneNumber,
                City, Street, Zipcode, Password);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Employee.AddEmployee(Surname, Name, Patronymic, Role,
                Salary, DateOfBirth, DateOfStart, PhoneNumber,
                City, Street, Zipcode, Password);

            CloseWindow();
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
                && !string.IsNullOrWhiteSpace(Zipcode)
                && (Role != null);
        }
    }
}
