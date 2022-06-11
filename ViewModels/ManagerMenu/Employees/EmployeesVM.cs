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

        private string _filteredSurname = "";
        private string _selectedRole = AllString;

        public EmployeesVM()
        {
            OpenAddEmployeeWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(ManagerEmployees.AddEmployee));
            OpenEditEmployeeWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(ManagerEmployees.EditEmployee));
            PrintEmployeesCommand = new RelayCommand<object>(_ => PrintEmployees());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
                
            SetSelectiveRoles();
        }

        public Action<ManagerEmployees> OpenWindowViewModel { get; set; }

        public List<string[]> Employees
        {
            get
            {
                List<string[]> employees = Empl.GetAllEmployee(_selectedRole, _filteredSurname);

                if (employees == null)
                {
                    return new List<string[]>();
                }

                foreach (var employee in employees)
                {
                    // set word-roles
                    employee[Empl.role] = Roles.roleNames[int.Parse(employee[Empl.role])];
                    employee[Empl.date_of_birth] = DateTime.Parse(employee[6]).ToShortDateString();
                    employee[Empl.date_of_start] = DateTime.Parse(employee[7]).ToShortDateString();
                }

                return employees;
            }
        }

        public RelayCommand<object> OpenAddEmployeeWindowCommand { get; }

        public RelayCommand<object> OpenEditEmployeeWindowCommand { get; }

        public RelayCommand<object> PrintEmployeesCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public string[] SelectedEmployee { get; set; }
        
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
            OnPropertyChanged(nameof(Employees));
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
                    if (h == 9) { break; } 
                    printerEmployees[i].SetValue(Employees[i][h], h);
                }

                for (int h = 9; h < Employees[0].Length - 1; ++h)
                {
                    printerEmployees[i].SetValue(Employees[i][h + 1], h);
                }
            }

            Printer.PrintDataGrid(printerEmployees, "Працівники станом на " + DateTime.UtcNow.ToString("dd.MM.yyyy"), new string[]
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
