using System.Collections.Generic;
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

        public DetailsReceiptVM()
        {
            DeleteReceiptCommand = new RelayCommand<object>(_ => DeleteReceipt());
        }

        private void DeleteReceipt()
        {
            Rec.DeleteReceiptByReceiptNumber(ReceiptNumber);
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
                
                    newSale[5] = (double.Parse(newSale[3]) * double.Parse(newSale[4])).ToString();
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
            OnPropertyChanged(nameof(Sales));
        }
    }
}