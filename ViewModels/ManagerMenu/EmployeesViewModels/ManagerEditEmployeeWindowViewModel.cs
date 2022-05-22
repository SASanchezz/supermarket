using supermarket.Navigation.WindowsNavigation;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using supermarket.Windows.ManagerMenu.EmployeeWindows;
using supermarket.Utils;

namespace supermarket.ViewModels.ManagerMenu.EmployeesViewModels
{
    /*
     * Controls ManagerEditEmployee Window
     */
    internal class ManagerEditEmployeeWindowViewModel : INavigatableWindowViewModel, INotifyPropertyChanged
    {
        private ManagerEditEmployeeViewModel _viewModel;
        private Window _window;

        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _closeCommand;

        public ManagerEditEmployeeWindowViewModel()
        {
            _window = new ManagerEditEmployeeWindow()
            {
                DataContext = this
            };
            _viewModel = new ManagerEditEmployeeViewModel();
            _viewModel.Close = Window.Close;
            Window.Show();
        }
        
        
        public WindowTypes WindowType { get => WindowTypes.ManagerEditEmployee; }
        public Window Window { get => _window; }
        public ManagerEditEmployeeViewModel ViewModel { get => _viewModel; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
