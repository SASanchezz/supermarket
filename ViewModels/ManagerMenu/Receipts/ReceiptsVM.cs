using System;
using System.Collections.Generic;
using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;
using Empl = supermarket.Models.Employee;
using supermarket.Printing;

namespace supermarket.ViewModels.ManagerMenu.Receipts
{
    internal class ReceiptsVM : ViewModel, IWindowOpeningVM<ManagerReceipts>
    {
        private const string AllString = "Всі";
        private string _filteredIdCashier = AllString;
        
        private DateTime _minPrintDate;
        private DateTime _maxPrintDate;
        
        public ReceiptsVM()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
            
            OpenDetailsReceiptWindowCommand =
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerReceipts.DetailsReceipt));

            PrintReceiptsCommand = new RelayCommand<object>(_ => PrintReceipts());
            CountReceiptsSumCommand = new RelayCommand<object>(_ => OnPropertyChanged(nameof(ReceiptsSum)));
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public List<string[]> Receipts
        {
            get
            {
                if (FilteredIdCashier != AllString)
                {
                    try
                    {
                        int.Parse(FilteredIdCashier.Split(' ')[0]);
                    }
                    catch
                    {
                        return new List<string[]>();
                    }
                }
            
                List<string[]> receipts = Rec.GetAllReceipts(FilteredIdCashier.Split(' ')[0], MinPrintDate, MaxPrintDate);

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

        public Action<ManagerReceipts> OpenWindowViewModel { get; set; }
        
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
                OnPropertyChanged(nameof(SelectiveCashiers));
            }
        }

        public List<string> SelectiveCashiers
        {
            get
            {
                List<string[]> employees = Empl.GetCashierLikeIdOrSNP(FilteredIdCashier);

                if (employees == null)
                {
                    return new List<string>(0);
                }
                
                List<string> resultReceipt = new(employees.Count + 1) { AllString };
                
                foreach (var employee in employees)
                {
                    resultReceipt.Add(employee[Empl.id] + "  --  " + employee[Empl.surname]);
                }
                
                return resultReceipt;
            }
        }

        public void UpdateReceipts()
        {
            OnPropertyChanged(nameof(Receipts));
            //OnPropertyChanged(nameof(ReceiptsSum));
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