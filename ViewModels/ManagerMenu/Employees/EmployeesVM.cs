using supermarket.Utils;
using System.Collections.Generic;
using System;
using supermarket.Data;
using Empl = supermarket.Models.Employee;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.Printing;

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
            get => _employees;
            set
            {
                _employees = value;
                // set word-roles
                if (_employees != null)
                {
                    foreach (var employee in _employees)
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

        public RelayCommand<object> PrintCommand
        {
            get
            {
                return _printCommand ??= new RelayCommand<object>(_ => PrintEmployees());
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
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }
        public string FilteredSurname
        {
            get => _filteredSurname;
            set
            {
                _filteredSurname = value;
                
                UpdateEmployees();
                OnPropertyChanged();
            }
        }

        public string[] SelectiveRoles { get; private set; }

        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value ?? AllString;
                OnPropertyChanged();
                UpdateEmployees();
            }
        }

        public void UpdateEmployees()
        {
            Employees = Empl.GetAllEmployee(_selectedRole, _filteredSurname);

            if (Employees == null) return;

            foreach (var employee in Employees)
            {
                employee[6] = DateTime.Parse(employee[6]).ToShortDateString();
                employee[7] = DateTime.Parse(employee[7]).ToShortDateString();
            }
        }

        private void SetSelectiveRoles()
        {
            SelectiveRoles = new string[Data.Roles.roleNames.Length + 1];
            SelectiveRoles.SetValue(AllString, 0);
            for (int i = 0; i < Data.Roles.roleNames.Length; ++i)
            {
                SelectiveRoles.SetValue(Data.Roles.roleNames[i], i + 1);
            }
        }

        private void PrintEmployees()
        {
            var printerEmployees = new List<string[]>();

            for (int i = 0; i < Employees.Count; ++i)
            {
                printerEmployees.Add(new string[Employees[0].Length - 1]);

                for (int h = 0; ; ++h)
                {
                    if (h == 9) break; 
                    printerEmployees[i].SetValue(Employees[i][h], h);
                }

                for (int h = 9; h < Employees[0].Length - 1; ++h)
                {
                    printerEmployees[i].SetValue(Employees[i][h + 1], h);
                }
            }

            Printer.PrintDataGrid(printerEmployees, new string[]
            {
                "id",
                "Прізвище",
                "Ім'я",
                "По батькові",
                "Посада",
                "Зарплата",
                "Дата народження",
                "Дата початку роботи",
                "Номер телефону",
                "Місто",
                "Вулиця",
                "Поштовий індекс"
            });
        }
    }
}
