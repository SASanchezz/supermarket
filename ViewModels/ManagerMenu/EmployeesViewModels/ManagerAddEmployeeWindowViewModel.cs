using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu.EmployeeWindows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.EmployeesViewModels
{
    /*
     * Controls ManagerAddEmployee Window
     */
    internal class ManagerAddEmployeeWindowViewModel : INavigatableWindowViewModel, INotifyPropertyChanged
    {
        private readonly ManagerAddEmployeeViewModel _viewModel;
        private readonly Window _window;

        public ManagerAddEmployeeWindowViewModel()
        {
            _window = new ManagerAddEmployeeWindow()
            {
                DataContext = this
            };

            _viewModel = new ManagerAddEmployeeViewModel();
            _viewModel.Close = Window.Close;

            Window.Show();
        }

        public ManagerAddEmployeeViewModel ViewModel { get => _viewModel; }
        public WindowTypes WindowType => WindowTypes.ManagerAddEmployee;
        public Window Window => _window;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
