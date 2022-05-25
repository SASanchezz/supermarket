using supermarket.Utils;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using supermarket.Navigation.WindowsNavigation;
using System.Windows;
using supermarket.Data;
using Empl = supermarket.Models.Employee;

namespace supermarket.ViewModels.ManagerMenu
{
    /*
     * Controls ManagerEmployees View
     */
    public class ManagerEmployeesViewModel : IWindowOpeningViewModel, INotifyPropertyChanged
    {
        private const string _allString = "Всі";
        private List<string[]> _employees;
        private string[] _selectedEmployee;
        private string _selectedRole;
        private string[] _allRoles = Roles.roleNames;
        

        private RelayCommand<object> _openAddEmployeeWindowCommand;
        private RelayCommand<object> _openEditEmployeeWindowCommand;
        private RelayCommand<object> _closeCommand;

        public ManagerEmployeesViewModel()
        {
            UpdateEmployees();
            SelectedRole = _allString;

            Array.Resize(ref _allRoles, _allRoles.Length + 1);
            _allRoles[_allRoles.GetUpperBound(0)] = _allString;
        }

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
                        employee[Empl.role] = Roles.roleNames[(int.Parse(employee[Empl.role]))];
                    }
                }
                OnPropertyChanged(nameof(Employees));
            }
        }
        public RelayCommand<object> OpenAddEmployeeWindowCommand
        {
            get
            {
                return _openAddEmployeeWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(WindowTypes.ManagerAddEmployee));
            }
        }
        public RelayCommand<object> OpenEditEmployeeWindowCommand
        {
            get
            {
                return _openEditEmployeeWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(WindowTypes.ManagerEditEmployee));
            }
        }
        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        public Action<WindowTypes> OpenWindowViewModel { get; set; }
        public Action Close { get; set; }
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
        public string SelectedRole
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                _selectedRole = value;
                if (_selectedRole == _allString)
                {
                    UpdateEmployees();
                }
                else
                {
                    Employees = Empl.GetEmployeesByRole(Roles.roleKeys[_selectedRole]);
                }
                OnPropertyChanged();
            }
        }

        public string[] AllRoles { get => _allRoles; }

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
