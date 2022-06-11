using System;
using System.Collections.Generic;
using supermarket.Utils;
using supermarket.Navigation.WindowViewModels;
using Cust = supermarket.Models.Customer;
using supermarket.ViewModels.BaseClasses;
using supermarket.Printing;

namespace supermarket.ViewModels.ManagerMenu.Customers
{
    /*
     * Controls ManagerCustomers View
     */
    internal class CustomersVM : ViewModel, IWindowOpeningVM<ManagerCustomers>
    {
        private double _sliderMax;
        private double _sliderMin;

        public CustomersVM()
        {
            OpenAddCustomerWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCustomers.AddCustomer));
            
            OpenEditCustomerWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCustomers.EditCustomer));

            PrintCustomersCommand = new RelayCommand<object>(_ => PrintCustomers());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
            
            SliderMax = 100;
            SliderMin = 0;
        }

        public Action<ManagerCustomers> OpenWindowViewModel { get; set; }

        public List<string[]> Customers => Cust.GetAllCustomers(_sliderMin, _sliderMax);

        public RelayCommand<object> OpenAddCustomerWindowCommand { get; }

        public RelayCommand<object> OpenEditCustomerWindowCommand { get; }

        public RelayCommand<object> PrintCustomersCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public string[] SelectedCustomer { get; set; }

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
            OnPropertyChanged(nameof(Customers));
        }

        private void PrintCustomers()
        {
            var printerCustomers = new List<string[]>();

            for (int i = 0; i < Customers.Count; ++i)
            {
                printerCustomers.Add(new string[Customers[0].Length]);

                for (int h = 0; h < Customers[0].Length; ++h)
                {
                    printerCustomers[i].SetValue(Customers[i][h], h);
                }
            }

            Printer.PrintDataGrid(Customers, "Клієнти станом на " + DateTime.UtcNow.ToString("dd.MM.yyyy"), new string[]
            {
                "Номер карти",
                "Прізвище",
                "Ім'я",
                "По батькові",
                "Номер телефону",
                "Місто",
                "Вулиця",
                "Поштовий індекс",
                "Відсоток"
            });
        }
    }
}
