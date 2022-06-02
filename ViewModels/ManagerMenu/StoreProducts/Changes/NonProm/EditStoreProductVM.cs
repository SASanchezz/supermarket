using supermarket.Utils;
using System;
using supermarket.Data;
using supermarket.Middlewares.Employee;
using Empl = supermarket.Models.Employee;
using System.Windows;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm
{
    /*
     * Controls ManagerEditEmployee View
     */
    internal class EditStoreProductVM : ViewModel
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

        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _closeCommand;

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

        public static string[] EmployeeRoles => Roles.roleNames;

        public RelayCommand<object> UpdateCommand
        {
            get
            {
                return _updateCommand ??= new RelayCommand<object>(_ => UpdateEmployee(), CanExecute);
            }
        }
        public RelayCommand<object> DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand<object>(_ => DeleteEmployee());
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

        public void SetData(string[] data)
        {
            Id = data[Empl.id];
            Name = data[Empl.name];
            Surname = data[Empl.surname];
            Patronymic = data[Empl.patronymic];
            Role = data[Empl.role];
            Salary = data[Empl.salary];
            DateOfBirth = DateTime.Parse(data[Empl.date_of_birth]);
            DateOfStart = DateTime.Parse(data[Empl.date_of_start]);
            PhoneNumber = data[Empl.phone_number];
            Password = ""; //data[9];
            City = data[Empl.city];
            Street = data[Empl.street];
            Zipcode = data[Empl.zipcode];
        }

        private void UpdateEmployee()
        {
            string result = UpdateDataValidator.Validate(Id, Surname, Name, Patronymic, Role,
                Salary, DateOfBirth, DateOfStart, PhoneNumber,
                City, Street, Zipcode, Password);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            string[] employee = DbQueries.GetEmployeeByID(Id)[0];

            Password = Password == "" ? employee[Empl.password] : CryptUtils.HashPassword(Password);

            Empl.UpdateEmployee(Id, Surname, Name, Patronymic, Role,
            Salary, DateOfBirth, DateOfStart, PhoneNumber,
            Password, City, Street, Zipcode);

            Close();
        }

        private void DeleteEmployee()
        {
            Empl.DeleteEmployeeByID(Id);
            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(Patronymic)
                && !string.IsNullOrWhiteSpace(Salary)
                && !string.IsNullOrWhiteSpace(PhoneNumber)
                && !string.IsNullOrWhiteSpace(City)
                && !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(Zipcode);
        }
    }
}
