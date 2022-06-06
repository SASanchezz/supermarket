using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using supermarket.Models;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;

namespace supermarket.ViewModels.ManagerMenu.Receipts.Changes
{
    internal class DetailsReceiptVM : ViewModel
    {
        private string _receiptNumber;

        public DetailsReceiptVM()
        {
            DeleteReceiptCommand = new RelayCommand<object>(_ => DeleteReceipt());
        }

        private void DeleteReceipt()
        {
            Rec.DeleteReceiptByReceiptNumber(ReceiptNumber);
            CloseWindow();
        }

        public List<string[]> Sales { get; private set; }

        public string ReceiptNumber
        {
            get => _receiptNumber;
            set
            {
                _receiptNumber = value;
                OnPropertyChanged();
                UpdateSales();
            }
        }

        public RelayCommand<object> DeleteReceiptCommand { get; }

        public void SetData(string[] receiptData)
        {
            ReceiptNumber = receiptData[Rec.receipt_number];
        }

        private void UpdateSales()
        {
            Sales = Sale.GetAllSalesByCheckNumber(ReceiptNumber);
            
            if (Sales == null) return;
            
            for (int i = 0; i < Sales.Count; ++i)
            {
                string[] oldSale = Sales[i];
                string[] newSale = new string[6];
                for (int h = 0; h < oldSale.Length; ++h)
                {
                    newSale[h] = oldSale[h];
                }
                
                newSale[5] = (double.Parse(newSale[4]) / double.Parse(newSale[3])).ToString();
                Sales[i] = newSale;
            }
        }
    }
}