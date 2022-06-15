using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Middlewares.Receipts;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using Receipt = supermarket.Models.Receipt;
using System.Collections.Generic;

namespace supermarket.ViewModels.CashierMenu.Receipts.Changes
{
    internal class AddReceiptVM : ViewModel
    {
        private List<string[]> _selectedProducts = new(0);


        private const string cashierId = "44"; //constant cashier id

        private string _cashier_id = cashierId;
        private string _client_card = "";
        private string _sum;
        
        public AddReceiptVM()
        {
            //Temp
            string[] one = { "Milk", "23" };
            _selectedProducts.Add(one);
            //Temp
            AddReceiptCommand = new RelayCommand<object>(_ => AddReceipt(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public List<string> SelectiveClients
        {
            get
            {
                List<string[]> customers = Models.Customer.GetCustomersLikeSNPOrCardOrPhone(_client_card);

                if (customers == null)
                {
                    return new List<string>(0);
                }

                List<string> resultCustomers = new(customers.Count);

                foreach (var customer in customers)
                {
                    resultCustomers.Add(customer[Customer.card_number] + "  --  " +
                        customer[Customer.phone_number] + "  --  " +
                        customer[Customer.surname]);
                }

                return resultCustomers;
            }
        }

        public List<string[]> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value;
            }
        }

        public string CashierId
        {
            get => _cashier_id;
            set
            {
                _cashier_id = value;
                OnPropertyChanged();
            }
        }

        public string ClientCard
        {
            get => _client_card.Split(' ')[0];
            set
            {
                _client_card = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectiveClients));
            }
        }

        public string Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> AddReceiptCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        private void ResetFields()
        {
            ClientCard = "";
            Sum = "";
        }

        private void AddReceipt()
        {
            ////Validates entered information
            string result = ReceiptValidator.Validate(ClientCard, Sum);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Receipt.AddReceipt(_cashier_id, ClientCard, Sum);

            ResetFields();
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Sum);
        }
    }
}
