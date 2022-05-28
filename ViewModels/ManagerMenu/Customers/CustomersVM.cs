using System;
using System.Collections.Generic;
using supermarket.Utils;
using supermarket.Navigation.WindowViewModels;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    /*
     * Controls ManagerCustomers View
     */
    //TODO
    internal class CustomersVM : ViewModel, IWindowOpeningVM<ManagerCustomers>
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

        public Action<ManagerCustomers> OpenWindowViewModel { get; set; }
        public Action Close { get; set; }

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
    }
}
