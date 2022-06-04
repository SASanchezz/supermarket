using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;

namespace supermarket.ViewModels.ManagerMenu.Receipts
{
    internal class ReceiptsVM : ViewModel
    {
        private List<string[]> _receipts;
        
        private DateTime _minPrintDate;
        private DateTime _maxPrintDate;
        
        public ReceiptsVM()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
            UpdateReceipts();
        }
        
        public List<string[]> Receipts
        {
            get => _receipts;
            set
            {
                _receipts = value;
                OnPropertyChanged();
            }
        }
        
        public RelayCommand<object> OpenDetailsReceiptWindowCommand { get; }
        
        public RelayCommand<object> PrintReceiptsCommand { get; }
        
        public RelayCommand<object> CountReceiptsSumCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }
        
        public DateTime MinPrintDate
        {
            get => _minPrintDate;
            set
            {
                _minPrintDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime MaxPrintDate
        {
            get => _maxPrintDate;
            set
            {
                _maxPrintDate = value;
                OnPropertyChanged();
            }
        }
        
        public void UpdateReceipts()
        {
            _receipts = Rec.GetAllReceipts(MinPrintDate, MaxPrintDate);

            if (Receipts == null)
            {
                MessageBox.Show("no receipts");
                return;
            };
        
            foreach (var receipt in Receipts)
            {
                receipt[Rec.name_employee] += " " + receipt[Rec.surname_employee]
                                                  + " " + receipt[Rec.patronymic_employee];
            }

        }
        
        //public void DefaultMinPrintDate()
    }
}