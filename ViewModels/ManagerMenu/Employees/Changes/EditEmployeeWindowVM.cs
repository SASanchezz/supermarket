using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using supermarket.Windows.ManagerMenu.Employees.Changes;
using supermarket.Navigation.WindowVM;

namespace supermarket.ViewModels.ManagerMenu.Employees.Changes
{
    /*
     * Controls ManagerEditEmployee Window
     */
    internal class EditEmployeeWindowVM : INavigatableWindowVM<ManagerEmployees>, INotifyPropertyChanged
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

        public ManagerEmployees WindowType => ManagerEmployees.EditEmployee; 

        public Window Window => _window; 

        public EditEmployeeVM ViewModel => _viewModel; 

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
