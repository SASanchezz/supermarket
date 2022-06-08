using System;
using System.Collections.Generic;
using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;
using Employee = supermarket.Models.Employee;
using supermarket.Data;

namespace supermarket.ViewModels.ManagerMenu.Receipts
{
    internal class ReceiptsVM : ViewModel, IWindowOpeningVM<ManagerReceipts>
    {
        private const string AllString = "Âñ³";
        private List<string[]> _receipts;
        private string[] _selectedReceipt;
        private string _filteredIdCashier = AllString;
        private DateTime _minPrintDate;
        private DateTime _maxPrintDate;
        
        public ReceiptsVM()
        {
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
            OpenDetailsReceiptWindowCommand =
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerReceipts.DetailsReceipt));

            CountReceiptsSumCommand = new RelayCommand<object>(_ => OnPropertyChanged(nameof(ReceiptsSum)));
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

        public double ReceiptsSum
        {
            get => Models.Receipt.GetAllReceiptsSum(FilteredIdCashier.Split(' ')[0], MinPrintDate, MaxPrintDate);
            set { }
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
                List<string[]> employees = Employee.GetCashierLikeIdOrSNP(FilteredIdCashier);
                if (employees == null) return new List<string>(0);
                List<string> resultReceipt = new(employees.Count+1);
                resultReceipt.Add(AllString);
                foreach (var employee in employees)
                {
                    resultReceipt.Add(employee[Employee.id] + "  --  " + employee[Employee.surname]);
                }

                return resultReceipt;
            }
            set { }
        }

        public void UpdateReceipts()
        {
            if (FilteredIdCashier != AllString)
            {
                try
                {
                    int.Parse(FilteredIdCashier.Split(' ')[0]);
                }
                catch
                {
                    return;
                }
            }
            
            Receipts = Rec.GetAllReceipts(FilteredIdCashier.Split(' ')[0], MinPrintDate, MaxPrintDate);

            if (Receipts == null) return;
        
            foreach (var receipt in Receipts)
            {
                receipt[Rec.name_employee] += " " + receipt[Rec.surname_employee]
                                                  + " " + receipt[Rec.patronymic_employee];
            }
            OnPropertyChanged(nameof(ReceiptsSum));
        }

        public void Reset()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
            FilteredIdCashier = AllString;
        }

        public Action<ManagerReceipts> OpenWindowViewModel { get; set; }
    }
}