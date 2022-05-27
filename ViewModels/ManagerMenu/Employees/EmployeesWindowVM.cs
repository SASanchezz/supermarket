using supermarket.Navigation.WindowVM;
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
    internal class EmployeesWindowVM : WindowVMNavigator<ManagerEmployees>, INavigatableWindowVM<Main>
    {
        private EmployeesVM _viewModel;
        private Window _window;

        public EmployeesWindowVM()
        {
            IsEnabled = true;

            _window = new EmployeesWindow
            {
                DataContext = this
            };

            _viewModel = new EmployeesVM();
            // there is a button "Back"
            _viewModel.Close = Window.Close;

            SetWindowOpeningVM(new IWindowOpeningVM<ManagerEmployees>[] { _viewModel });

            Window.Show();
        }

        public Main WindowType => Main.ManagerEmployees; 

        public Window Window => _window; 

        public EmployeesVM ViewModel => _viewModel; 

        protected override INavigatableWindowVM<ManagerEmployees> CreateWindowViewModel(ManagerEmployees type)
        {
            try
            {
                switch (type)
                {
                    case ManagerEmployees.AddEmployee:
                    {
                        var addWindowVM = new AddEmployeeWindowVM();
                        SetDefaultClosedEventHandler(addWindowVM);
                        return addWindowVM;
                    }
                    case ManagerEmployees.EditEmployee:
                    {
                        if (_viewModel.SelectedEmployee == null)
                        {
                            IsEnabled = true;
                            throw new Exception("No selected item");
                        }

                        var editWindowVM = new EditEmployeeWindowVM();
                        SetDefaultClosedEventHandler(editWindowVM);
                        editWindowVM.ViewModel.SetData(ViewModel.SelectedEmployee);
                        ViewModel.SelectedEmployee = null;
                        return editWindowVM;
                    }
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        private void SetDefaultClosedEventHandler(INavigatableWindowVM<ManagerEmployees> windowVM)
        {
            string filteredSurname = ViewModel.FilteredSurname;
            windowVM.Window.Closed += (sender, e) =>
            {
                IsEnabled = true;
                ViewModel.UpdateEmployees();
                ViewModel.FilteredSurname = filteredSurname;
            };
        }
    }
}
