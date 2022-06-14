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
        private string _card_number;
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
            }
        }

        public string CardNumber
        {
            get => _card_number;
            set
            {
                _card_number = value;
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

        private void AddReceipt()
        {
            try
            {
                //Validates entered information
                ReceiptValidator.Validate(CardNumber, Sum);
                
                //Query to insert new receipt
                Receipt.AddReceipt(_cashier_id, CardNumber, Sum);

                ResetFields();
                CloseWindow();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        private void ResetFields()
        {
            CardNumber = "";
            Sum = "";
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(CardNumber)
                && !string.IsNullOrWhiteSpace(Sum);
        }
    }
}
