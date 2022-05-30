using supermarket.Utils;
using System.Collections.Generic;
using System;
using supermarket.Data;
using Empl = supermarket.Models.Employee;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Employees
{
    /*
     * Controls ManagerEmployees View
     */
    internal class EmployeesVM : ViewModel, IWindowOpeningVM<ManagerEmployees>
    {
        private const string AllString = "Всі";

        private List<string[]> _employees;
        private string[] _selectedEmployee;

        private string _filteredSurname = "";

        private string[] _selectiveRoles;
        private string _selectedRole = AllString;

        private RelayCommand<object> _openAddEmployeeWindowCommand;
        private RelayCommand<object> _openEditEmployeeWindowCommand;
        private RelayCommand<object> _printCommand;
        private RelayCommand<object> _closeCommand;

        public EmployeesVM()
        {
            UpdateEmployees();
            SetSelectiveRoles();
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
                
                OnPropertyChanged();
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
                
                UpdateEmployees();
                OnPropertyChanged();
            }
        }

        public string[] SelectiveRoles => _selectiveRoles;

        public string SelectedRole
        {
            get
            {
                return _selectedRole;
            }
            set
            {
                if (value == null)
                {
                    _selectedRole = AllString;
                } 
                else
                {
                    _selectedRole = value;
                }
                OnPropertyChanged();
                UpdateEmployees();
            }
        }

        public RelayCommand<object> PrintCommand
        {
            get
            {
                return _printCommand ??= new RelayCommand<object>(_ =>
                {
                    Printer.PrintDataGrid(Employees, new string[]
                    {
                        "Колонка1", 
                        "Колонка2", 
                        "Колонка3", 
                        "Колонка4", 
                        "Колонка5", 
                        "Колонка6",
                        "Колонка7",
                        "Колонка8",
                        "Колонка9",
                        "Колонка10",
                        "Колонка11",
                        "Колонка12", 
                        "Колонка13"
                    });
                });
                
                
            }
        }


        public void UpdateEmployees()
        {
            Employees = Empl.GetAllEmployee(_selectedRole, _filteredSurname);
        }

        private void SetSelectiveRoles()
        {
            _selectiveRoles = new string[Data.Roles.roleNames.Length + 1];
            _selectiveRoles.SetValue(AllString, 0);
            for (int i = 0; i < Data.Roles.roleNames.Length; ++i)
            {
                _selectiveRoles.SetValue(Data.Roles.roleNames[i], i + 1);
            }
        }
    }
}
