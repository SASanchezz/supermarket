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
        private List<string[]> _employees;
        private string[] _selectedEmployee;
        private int _selectedRole;

        private RelayCommand<object> _openAddEmployeeWindowCommand;
        private RelayCommand<object> _openEditEmployeeWindowCommand;
        private RelayCommand<object> _closeCommand;

        public ManagerEmployeesViewModel()
        {
            UpdateEmployees();
            SelectedRole = 0;
            //int i = 0;
            //foreach (var employee in _employees)
            //{
            //    employee[4] = Roles.roleNames[int.Parse(employee[4])];
            //    ++i;
            //}
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
                foreach (string[] employee in _employees)
                {
                    employee[Empl.role] = Roles.roleNames[(int.Parse(employee[Empl.role]) - 1)];
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
        public int SelectedRole
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
