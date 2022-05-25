using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu.Employees.Changes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls ManagerAddEmployee Window
     */
    internal class AddEmployeeWindowVM : INavigatableWindowVM, INotifyPropertyChanged
    {
        private readonly AddEmployeeVM _viewModel;
        private readonly Window _window;

        public AddEmployeeWindowVM()
        {
            _window = new AddEmployeeWindow()
            {
                DataContext = this
            };

            _viewModel = new AddEmployeeVM();
            _viewModel.Close = Window.Close;

            Window.Show();
        }

        public AddEmployeeVM ViewModel { get => _viewModel; }
        public WindowTypes WindowType => WindowTypes.ManagerAddEmployee;
        public Window Window => _window;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
