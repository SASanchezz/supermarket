using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.Employees.Changes;
using supermarket.Windows.ManagerMenu.Employees;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Employees
{
    /* 
     * Controls ManagerEmployees Window
     * Set navigating Windows as ManagerAddEmployee Window, ManagerEditEmployee Window
     */
    internal class EmployeesWindowVM : WindowViewModel<EmployeesWindow, EmployeesVM>
    {
        private AddEmployeeWindowVM _addEmployeeWindowVM;
        private EditEmployeeWindowVM _editEmployeeWindowVM;

        public EmployeesWindowVM()
        {
            _addEmployeeWindowVM = new AddEmployeeWindowVM();
            _editEmployeeWindowVM = new EditEmployeeWindowVM();
            
            SetUpdatingSystem();
            SetWindowsNavigation();

            Window.Closed += (sender, e) =>
            {
                _addEmployeeWindowVM.Window.Close();
                _editEmployeeWindowVM.Window.Close();
            };
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator =
                new WindowVMNavigator<ManagerEmployees>(new IWindowOpeningVM<ManagerEmployees>[] { ViewModel });
            
            windowsNavigator.SetWay(ManagerEmployees.AddEmployee, _addEmployeeWindowVM.Window);
            windowsNavigator.SetWay(ManagerEmployees.EditEmployee, _editEmployeeWindowVM.Window, 
                OnOpeningEditEmployee);
            
            SetEnabilitySystem(_addEmployeeWindowVM);
            SetEnabilitySystem(_editEmployeeWindowVM);
        }

        private void OnOpeningEditEmployee()
        {
            if (ViewModel.SelectedEmployee == null)
            {
                throw new Exception("No selected item");
            }

            _editEmployeeWindowVM.ViewModel.SetData(ViewModel.SelectedEmployee);
            ViewModel.SelectedEmployee = null;
        }

        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return; // window is hiden
                // window is shown
                ViewModel.UpdateEmployees();
            };
            SetUpdatingAfterHiden(_addEmployeeWindowVM);
            SetUpdatingAfterHiden(_editEmployeeWindowVM);
        }
        
        private void SetUpdatingAfterHiden<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM) 
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return; // window is shown
                // window is hiden
                ViewModel.UpdateEmployees();
            };
        }

        private void SetEnabilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
