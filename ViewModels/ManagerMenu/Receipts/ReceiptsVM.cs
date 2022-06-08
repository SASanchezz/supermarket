using System;
using System.Collections.Generic;
using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Rec = supermarket.Models.Receipt;
using Employee = supermarket.Models.Employee;

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
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
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

        public List<string[]> ReceiptsSum
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
                List<string[]> employees = Employee.GetEmployeeLikeIdOrSNP(FilteredIdCashier);
                if (employees == null) return new List<string>(0);
                List<string> resultProducts = new(employees.Count);
                foreach (var employee in employees)
                {
                    resultProducts.Add(employee[Employee.id] + "  --  " + employee[Employee.surname]);
                }

                return resultProducts;
            }
            set { }
        }

        public void UpdateReceipts()
        {
            if (FilteredIdCashier != "")
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
        }

        public void Reset()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
            FilteredIdCashier = "";
        }

        public Action<ManagerReceipts> OpenWindowViewModel { get; set; }
    }
}