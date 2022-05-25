using supermarket.Utils;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using supermarket.Data;
using Empl = supermarket.Models.Employee;
using supermarket.Navigation.WindowVM;

namespace supermarket.ViewModels.ManagerMenu.Employees
{
    /*
     * Controls ManagerEmployees View
     */
    internal class EmployeesVM : IWindowOpeningVM<ManagerEmployees>, INotifyPropertyChanged
    {
        private List<string[]> _employees;
        private string[] _selectedEmployee;
        private string _filteredSurname;
        private int _selectedRole;

        private RelayCommand<object> _openAddEmployeeWindowCommand;
        private RelayCommand<object> _openEditEmployeeWindowCommand;
        private RelayCommand<object> _closeCommand;

        public EmployeesVM()
        {
            UpdateEmployees();
            //SelectedRole = 0;
        }

        public Action<ManagerEmployees> OpenWindowViewModel { get; set; }

        public Action Close { get; set; }

        public List<string[]> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                // set word-roles
                if (_employees != null)
                {
                    foreach (string[] employee in _employees)
                    {
                        employee[Empl.role] = Roles.roleNames[int.Parse(employee[Empl.role])];
                    }
                }
                
                OnPropertyChanged(nameof(Employees));
            }
        }

        public RelayCommand<object> OpenAddEmployeeWindowCommand
        {
            get
            {
                return _openAddEmployeeWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerEmployees.AddEmployee));
            }
        }

        public RelayCommand<object> OpenEditEmployeeWindowCommand
        {
            get
            {
                return _openEditEmployeeWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerEmployees.EditEmployee));
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }

        public string[] SelectedEmployee
        {
            get
            {
                return _selectedEmployee;
            }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public string FilteredSurname
        {
            get
            {
                return _filteredSurname;
            }
            set
            {
                _filteredSurname = value;
                
                {
                    Employees = Empl.GetEmployeesLikeSurname(_filteredSurname);
                }
                OnPropertyChanged();
            }
        }

        public int Surname
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                _selectedRole = value;
                if (_selectedRole == 0)
                {
                    UpdateEmployees();
                }
                else
                {
                    Employees = DbQueries.GetEmployeesByRole(_selectedRole);
                }
                OnPropertyChanged();
            }
        }

        public void UpdateEmployees()
        {
            Employees = DbQueries.GetAllEmployee();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
