using supermarket.Navigation.WindowsNavigation;
using supermarket.Windows.ManagerMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu
{
    internal class ManagerClientsWindowViewModel : WindowViewModelNavigator, INavigatableWindowViewModel
    {
        private Window _window;
        private ManagerClientsViewModel _viewModel;

        public ManagerClientsWindowViewModel()
        {
            IsEnabled = true;

            _window = new ManagerClientsWindow
            {
                DataContext = this
            };
            
            _viewModel = new ManagerClientsViewModel();

            Window.Show();
        }

        public Window Window => _window;
        public WindowTypes WindowType => WindowTypes.ManagerClients;
        public ManagerClientsViewModel ViewModel { get => _viewModel; }

        protected override INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
