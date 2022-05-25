using supermarket.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using supermarket.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    /*
     * Controls ManagerCustomers View
     */
    internal class CustomersVM : IWindowOpeningVM, INotifyPropertyChanged
    {
        private List<string[]> _customers;
        private string[] _selectedCustomer;

        private RelayCommand<object> _openAddCustomerWindowCommand;
        private RelayCommand<object> _openEditCustomerWindowCommand;
        private RelayCommand<object> _closeCommand;

        public CustomersVM()
        {
            UpdateCustomers();
        }

        public Action<WindowTypes> OpenWindowViewModel { get; set; }
        public List<string[]> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }
        public string[] SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public void UpdateCustomers()
        {
            Customers = DbQueries.GetAllCustomers();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
