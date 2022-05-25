using supermarket.Utils;
using System;
using System.ComponentModel;
using supermarket.Data;
using supermarket.Models;
using supermarket.Middlewares.Employee;
using System.Runtime.CompilerServices;
using Empl = supermarket.Models.Employee;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls ManagerEditEmployee View
     */
    internal class EditEmployeeVM : INotifyPropertyChanged
    {
        private string _id;
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

        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _closeCommand;

        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public string Salary
        {
            get => _salary;
            set
            {
                _salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        public DateTime Date_of_birth
        {
            get => _date_of_birth;
            set
            {
                _date_of_birth = value;
                OnPropertyChanged(nameof(Date_of_birth));
            }
        }

        public DateTime Date_of_start
        {
            get => _date_of_start;
            set
            {
                _date_of_start = value;
                OnPropertyChanged(nameof(Date_of_start));
            }
        }

        public string Phone_number
        {
            get => _phone_number;
            set
            {
                _phone_number = value;
                OnPropertyChanged(nameof(Phone_number));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged(nameof(Street));
            }
        }

        public string Zipcode
        {
            get => _zipcode;
            set
            {
                _zipcode = value;
                OnPropertyChanged(nameof(Zipcode));
            }
        }

        public static string[] EmployeeRoles { get => Roles.roleNames; }

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
            Date_of_birth = DateTime.Parse(data[Empl.date_of_birth]);
            Date_of_start = DateTime.Parse(data[Empl.date_of_start]);
            Phone_number = data[Empl.phone_number];
            Password = ""; //data[9];
            City = data[Empl.city];
            Street = data[Empl.street];
            Zipcode = data[Empl.zipcode];
        }

        private void UpdateEmployee()
        {
            string result = UpdateDataValidator.Validate(Id, Surname, Name, Patronymic, Role,
                Salary, Date_of_birth, Date_of_start, Phone_number,
                City, Street, Zipcode, Password);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            string[] employee = DbQueries.GetEmployeeByID(Id)[0];

            Password = Password == "" ? employee[Empl.password] : CryptUtils.HashPassword(Password);

            Empl.UpdateEmployee(Id, Surname, Name, Patronymic, Role,
            Salary, Date_of_birth, Date_of_start, Phone_number,
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
                && !string.IsNullOrWhiteSpace(Phone_number)
                //&& !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(City)
                && !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(Zipcode);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
