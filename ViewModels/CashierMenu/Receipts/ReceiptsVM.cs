using System;
using System.Collections.Generic;
using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;
using Empl = supermarket.Models.Employee;
using supermarket.Printing;

namespace supermarket.ViewModels.CashierMenu.Receipts
{
    internal class ReceiptsVM : ViewModel, IWindowOpeningVM<CashierReceipts>
    {
        //TODO
        private const string cashierId = "44"; //constant cashier id
        private string _filteredIdCashier = cashierId;
        private string _filteredReceiptId = "";
        
        private DateTime _minPrintDate;
        private DateTime _maxPrintDate;
        
        public ReceiptsVM()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
            
            OpenAddReceiptWindowCommand =
                new RelayCommand<object>(_ => OpenWindowViewModel(CashierReceipts.AddReceipt));

            PrintReceiptsCommand = new RelayCommand<object>(_ => PrintReceipts());
            CountReceiptsSumCommand = new RelayCommand<object>(_ => OnPropertyChanged(nameof(ReceiptsSum)));
            SetTodayDataCommand = new RelayCommand<object>(_ => SetTodayData());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public List<string[]> Receipts
        {
            get
            {
                List<string[]> receipts = Rec.GetAllReceipts(FilteredIdCashier.Split(' ')[0], MinPrintDate, MaxPrintDate, FilteredReceiptId);

                if (receipts == null)
                {
                    return new List<string[]>();
                }
        
                foreach (var receipt in receipts)
                {
                    receipt[Rec.name_employee] += " " + receipt[Rec.surname_employee]
                                                      + " " + receipt[Rec.patronymic_employee];
                }

                return receipts;
            }
        }

        public string[] SelectedReceipt { get; set; }

        public double ReceiptsSum
        {
            get => Models.Receipt.GetAllReceiptsSum(FilteredIdCashier.Split(' ')[0], MinPrintDate, MaxPrintDate);
        }

        public Action<CashierReceipts> OpenWindowViewModel { get; set; }
        
        public RelayCommand<object> OpenAddReceiptWindowCommand { get; }
        public RelayCommand<object> PrintReceiptsCommand { get; }
        public RelayCommand<object> CountReceiptsSumCommand { get; }
        public RelayCommand<object> SetTodayDataCommand { get; }
        
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
            }
        }

        public string FilteredReceiptId
        {
            get => _filteredReceiptId;
            set
            {
                _filteredReceiptId = value;
                UpdateReceipts();
            }
        }

        public void UpdateReceipts()
        {
            OnPropertyChanged(nameof(Receipts));
            //OnPropertyChanged(nameof(ReceiptsSum));
        }

        private void SetTodayData()
        {
            MinPrintDate = DateTime.Now;
            MaxPrintDate = DateTime.Now;
        }

        private void PrintReceipts()
        {
            var printerReceipts = new List<string[]>();

            for (int i = 0; i < Receipts.Count; ++i)
            {
                printerReceipts.Add(new string[Receipts[0].Length - 2]);

                for (int h = 0; ; ++h)
                {
                    if (h == 2)
                    {
                        printerReceipts[i].SetValue(Receipts[i][6], h);
                        break;
                    }
                    printerReceipts[i].SetValue(Receipts[i][h], h);
                }

                for (int h = 2; h < Receipts[0].Length - 3; ++h)
                {
                    printerReceipts[i].SetValue(Receipts[i][h], h + 1);
                }
            }

            Printer.PrintDataGrid(printerReceipts, "Чеки станом на " + DateTime.UtcNow.ToString("dd.MM.yyyy"), new string[]
            {
                "Номер",
                "id касира",
                "ПІБ касира",
                "Номер карти клієнта",
                "Дата",
                "Сума",
                "ПДВ",
            });
        }
    }
}