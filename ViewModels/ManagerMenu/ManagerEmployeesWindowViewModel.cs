using supermarket.Navigation.WindowsNavigation;
using supermarket.ViewModels.ManagerMenu.EmployeesViewModels;
using supermarket.Windows.ManagerMenu;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu
{
    /* 
     * Controls ManagerEmployees Window
     * Set navigating Windows as ManagerAddEmployee Window, ManagerEditEmployee Window
     */
    internal class ManagerEmployeesWindowViewModel : WindowViewModelNavigator, INavigatableWindowViewModel
    {
        private ManagerEmployeesViewModel _viewModel;
        private Window _window;

        public ManagerEmployeesWindowViewModel()
        {
            IsEnabled = true;

            _window = new ManagerEmployeesWindow
            {
                DataContext = this
            };

            _viewModel = new ManagerEmployeesViewModel();
                // there is a button "Back"
            _viewModel.Close = Window.Close;

            SetWindowOpeningViewModel(new IWindowOpeningViewModel[] { _viewModel });

            Window.Show();
        }

        public WindowTypes WindowType { get => WindowTypes.ManagerEmployees; }
        public Window Window { get => _window; }
        public ManagerEmployeesViewModel ViewModel { get => _viewModel; }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            INavigatableWindowViewModel windowViewModel;
            try
            {
                switch (type)
                {
                    case WindowTypes.ManagerAddEmployee:
                        windowViewModel = new ManagerAddEmployeeWindowViewModel();
                        windowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                        return windowViewModel;

                    case WindowTypes.ManagerEditEmployee:
                        if (_viewModel.SelectedEmployee == null)
                        {
                            IsEnabled = true;
                            throw new Exception("No selected item");
                        }
                        var editWindowViewModel = new ManagerEditEmployeeWindowViewModel();
                        editWindowViewModel.Window.Closed += (object sender, EventArgs e) => IsEnabled = true;
                        editWindowViewModel.ViewModel.Employee = ViewModel.SelectedEmployee;
                        ViewModel.SelectedEmployee = null;
                        return editWindowViewModel;

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
    }
}
