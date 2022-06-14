using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Middlewares.Receipts;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using Receipt = supermarket.Models.Receipt;

namespace supermarket.ViewModels.CashierMenu.Receipts.Changes
{
    internal class AddReceiptVM : ViewModel
    {
        private const string cashierId = "44"; //constant cashier id

        private string _cashier_id = cashierId;
        private string _client_card;
        private string _sum;
        
        public AddReceiptVM()
        {
            AddReceiptCommand = new RelayCommand<object>(_ => AddReceipt(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
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
            get => _client_card;
            set
            {
                _client_card = value;
                OnPropertyChanged();
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
            return !string.IsNullOrWhiteSpace(ClientCard)
                && !string.IsNullOrWhiteSpace(Sum);
        }
    }
}
