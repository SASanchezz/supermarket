using System;
using System.Collections.Generic;
using System.Windows;
using supermarket.Models;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;

namespace supermarket.ViewModels.ManagerMenu.Receipts.Changes
{
    internal class DetailsReceiptVM : ViewModel
    {
        private string _receiptNumber;
        private List<string[]> _sales;

        int ERROR = -1;
        int GOOD = 1;
        string deleteErrorMessage = "��� ��� ���������������";

        public DetailsReceiptVM()
        {
            DeleteReceiptCommand = new RelayCommand<object>(_ => DeleteReceipt());
        }

        private void DeleteReceipt()
        {
            int response = Rec.DeleteReceiptByReceiptNumber(ReceiptNumber);
            if (response == ERROR) MessageBox.Show(deleteErrorMessage);
            CloseWindow();
        }

        public List<string[]> Sales
        {
            get
            {
                List<string[]> sales = Sale.GetAllSalesByCheckNumber(ReceiptNumber);

                if (sales == null)
                {
                    return new List<string[]>();
                }
            
                for (int i = 0; i < sales.Count; ++i)
                {
                    string[] oldSale = sales[i];
                    string[] newSale = new string[6];
                    for (int h = 0; h < oldSale.Length; ++h)
                    {
                        newSale[h] = oldSale[h];
                    }

                    newSale[4] = Math.Round((double.Parse(newSale[4]) / double.Parse(newSale[3])), 4).ToString();
                    newSale[5] = oldSale[4];

                    sales[i] = newSale;
                }

                return sales;
            }
        }

        public string ReceiptNumber
        {
            get => _receiptNumber;
            set
            {
                _receiptNumber = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ReceiptSumWithoutDiscount));
                OnPropertyChanged(nameof(CustomerPercentDiscount));
                UpdateSales();
            }
        }

        public string ReceiptSumWithoutDiscount
        {
            get
            {
                return Rec.getRealReceiptSum(_receiptNumber).ToString();
            }
        }


        public string CustomerPercentDiscount
        {
            get
            {
                return Rec.GetReceiptCustomerDiscount(_receiptNumber).ToString();
            }
        }

        public RelayCommand<object> DeleteReceiptCommand { get; }

        public void SetData(string[] receiptData)
        {
            ReceiptNumber = receiptData[Rec.receipt_number];
        }

        private void UpdateSales()
        {
            OnPropertyChanged(nameof(Sales));
        }
    }
}