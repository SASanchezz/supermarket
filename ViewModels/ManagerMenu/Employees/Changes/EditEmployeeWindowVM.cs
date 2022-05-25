using supermarket.Navigation.WindowsNavigation;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using supermarket.Windows.ManagerMenu.Employees.Changes;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls ManagerEditEmployee Window
     */
    internal class EditEmployeeWindowVM : INavigatableWindowVM, INotifyPropertyChanged
    {
        private EditEmployeeVM _viewModel;
        private Window _window;

        public EditEmployeeWindowVM()
        {
            _window = new EditEmployeeWindow()
            {
                DataContext = this
            };
            _viewModel = new EditEmployeeVM();
            _viewModel.Close = Window.Close;
            Window.Show();
        }

        public WindowTypes WindowType { get => WindowTypes.ManagerEditEmployee; }
        public Window Window { get => _window; }
        public EditEmployeeVM ViewModel { get => _viewModel; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
