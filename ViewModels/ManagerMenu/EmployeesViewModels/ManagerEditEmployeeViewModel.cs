using supermarket.Utils;
using System;
using System.ComponentModel;
using supermarket.Data;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels.ManagerMenu.EmployeesViewModels
{
    /*
     * Controls ManagerEditEmployee View
     */
    internal class ManagerEditEmployeeViewModel : INotifyPropertyChanged
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

        private string[] _employee;

        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _closeCommand;
        public string[] Employee 
        { 
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;

                Name = _employee[1];
                Surname = _employee[2];
                Patronymic = _employee[3];
                Role = Roles.roleKeys[_employee[4]];
                Salary = _employee[5];
                Date_of_birth = DateTime.Parse(_employee[6]);
                Date_of_start = DateTime.Parse(_employee[7]);
                Phone_number = _employee[8];
                Password = _employee[9];
                City = _employee[10];
                Street = _employee[11];
                Zipcode = _employee[12];
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
        public string Surname 
        { 
            get => _surname; 
            set 
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
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
        public int Role 
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
        public Action Close { get; set; }
        //public RelayCommand<object> UpdateCommand 
        //{ 
        //    get
        //    {
        //        return _updateCommand ??= new RelayCommand<object>(_ => MessageBox.Show(Employee[0]));
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
