using System;
using System.Collections.Generic;
using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;

namespace supermarket.ViewModels.ManagerMenu.Receipts
{
    internal class ReceiptsVM : ViewModel, IWindowOpeningVM<ManagerReceipts>
    {
        private List<string[]> _receipts;
        private string[] _selectedReceipt;
        private string _filteredIdCashier = "";
        
        private DateTime _minPrintDate;
        private DateTime _maxPrintDate;
        
        public ReceiptsVM()
        {
            CloseCommand = new RelayCommand<object>(_ => Close());
            OpenDetailsReceiptWindowCommand =
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerReceipts.DetailsReceipt));
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

        public string[] SelectedReceipt
        {
            get => _selectedReceipt;
            set
            {
                _selectedReceipt = value;
                OnPropertyChanged();
            }
        }
        
        public Action Close { get; set; }
        
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
                UpdateReceipts();
                OnPropertyChanged();
            }
        }
        public DateTime MaxPrintDate
        {
            get => _maxPrintDate;
            set
            {
                _maxPrintDate = value;
                UpdateReceipts();
                OnPropertyChanged();
            }
        }

        public string FilteredIdCashier
        {
            get => _filteredIdCashier;
            set
            {
                _filteredIdCashier = value;
                UpdateReceipts();
                OnPropertyChanged();
            }
        }
        
        public void UpdateReceipts()
        {
            if (FilteredIdCashier != "")
            {
                try
                {
                    int.Parse(FilteredIdCashier);
                }
                catch
                {
                    return;
                }
            }
            
            Receipts = Rec.GetAllReceipts(FilteredIdCashier, MinPrintDate, MaxPrintDate);

            if (Receipts == null) return;
        
            foreach (var receipt in Receipts)
            {
                receipt[Rec.name_employee] += " " + receipt[Rec.surname_employee]
                                                  + " " + receipt[Rec.patronymic_employee];
            }
        }

        public void SetDefaultPrintDates()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
        }

        public Action<ManagerReceipts> OpenWindowViewModel { get; set; }
    }
}