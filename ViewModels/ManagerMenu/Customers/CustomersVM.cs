using System;
using System.Collections.Generic;
using supermarket.Utils;
using supermarket.Navigation.WindowViewModels;
using Cust = supermarket.Models.Customer;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    /*
     * Controls ManagerCustomers View
     */
    internal class CustomersVM : ViewModel, IWindowOpeningVM<ManagerCustomers>
    {
        private List<string[]> _customers;
        private string[] _selectedCustomer;

        private double _sliderMax;
        private double _sliderMin;

        private RelayCommand<object> _openAddCustomerWindowCommand;
        private RelayCommand<object> _openEditCustomerWindowCommand;
        private RelayCommand<object> _closeCommand;

        public CustomersVM()
        {
            SliderMax = 100;
            SliderMin = 0;
            //UpdateCustomers();
        }

        public Action<ManagerCustomers> OpenWindowViewModel { get; set; }
        public Action Close { get; set; }

        public List<string[]> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> OpenAddCustomerWindowCommand
        {
            get => _openAddCustomerWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCustomers.AddCustomer));
        }

        public RelayCommand<object> OpenEditCustomerWindowCommand
        {
            get => _openEditCustomerWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCustomers.EditCustomer));
        }

        public RelayCommand<object> CloseCommand
        {
            get => _closeCommand ??= new RelayCommand<object>(_ => Close());
        }

        public string[] SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public double SliderMax
        {
            get => _sliderMax;
            set
            {
                _sliderMax = value > _sliderMin ? value : _sliderMin;
                _sliderMax = _sliderMax > 100 ? 100 : _sliderMax;
                OnPropertyChanged();
                UpdateCustomers();
            }
        }

        public double SliderMin
        {
            get => _sliderMin;
            set
            {
                _sliderMin = value < _sliderMax ? value : _sliderMax;
                _sliderMin = _sliderMin < 0 ? 0 : _sliderMin;
                OnPropertyChanged();
                UpdateCustomers();
            }
        }

        public void UpdateCustomers()
        {
            Customers = Cust.GetAllCustomers(_sliderMin, _sliderMax);
        }
    }
}
