using System;
using System.Collections.Generic;
using supermarket.Models;
using supermarket.Printing;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Sales
{
    internal class SalesVM : ViewModel
    {
        private DateTime _minPrintDate;
        private DateTime _maxPrintDate;
        private List<string[]> _sales;

        public SalesVM()
        {
            PrintSalesCommand = new RelayCommand<object>(_ => PrintSales());
        }

        public RelayCommand<object> PrintSalesCommand { get; }
        private void DeleteReceipt()
        {
            //Sale.DeleteReceiptByReceiptNumber(ReceiptNumber);
            CloseWindow();
        }

        public List<string[]> Sales { 
            get => _sales;
            set
            {
                _sales = value;
                OnPropertyChanged();
            } }
        
        public DateTime MinPrintDate
        {
            get => _minPrintDate;
            set
            {
                _minPrintDate = value;
                UpdateSales();
                OnPropertyChanged();
            }
        }
        public DateTime MaxPrintDate
        {
            get => _maxPrintDate;
            set
            {
                _maxPrintDate = value;
                UpdateSales();
                OnPropertyChanged();
            }
        }

        public void Reset()
        {
            MinPrintDate = DateTime.Now.AddYears(-3);
            MaxPrintDate = DateTime.Now;
            UpdateSales();
        }

        public void UpdateSales()
        {
            Sales = Sale.GetAllSales(MinPrintDate, MaxPrintDate);
            
            if (Sales == null) return;
            
            for (int i = 0; i < Sales.Count; ++i)
            {
                string[] oldSale = Sales[i];
                string[] newSale = new string[7];
                for (int h = 0; h < 5; ++h)
                {
                    newSale[h] = oldSale[h];
                }
                
                newSale[5] = (double.Parse(newSale[4]) * double.Parse(newSale[3])).ToString();
                newSale[6] = oldSale[5];
                Sales[i] = newSale;
            }
        }

        private void PrintSales()
        {
            var printerSales = new List<string[]>();

            for (int i = 0; i < Sales.Count; ++i)
            {
                printerSales.Add(new string[Sales[0].Length]);

                for (int h = 0; h < Sales[0].Length; ++h)
                {
                    printerSales[i].SetValue(Sales[i][h], h);
                }
            }

            Printer.PrintDataGrid(printerSales, new string[]
            {
                "UPC",
                "id товару",
                "Назва товару",
                "Ціна",
                "Кількість",
                "Сума",
                "Дата продажу"
            });
        }
    }
}