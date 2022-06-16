using System;
using System.Collections.Generic;
using supermarket.Models;
using supermarket.Printing;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Prod = supermarket.Models.Product;

namespace supermarket.ViewModels.ManagerMenu.Sales
{
    internal class SalesVM : ViewModel
    {
        private const string AllString = Constants.AllString;
        private string _filteredProduct = AllString;

        private DateTime _minPrintDate = DateTime.Now.AddYears(-3);
        private DateTime _maxPrintDate = DateTime.Now;

        public SalesVM()
        {
            CountNumberOfProductsCommand = new RelayCommand<object>(_ => OnPropertyChanged(nameof(NumberOfProducts)));
            PrintSalesCommand = new RelayCommand<object>(_ => PrintSales());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public RelayCommand<object> CountNumberOfProductsCommand { get; }
        
        public RelayCommand<object> PrintSalesCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

        public List<string[]> Sales 
        {
            get
            {
                List<string[]> sales = Sale.GetAllSales(FilteredProduct.Split(' ')[0], MinPrintDate, MaxPrintDate);

                if (sales == null)
                {
                    return new List<string[]>();
                }
            
                for (int i = 0; i < sales.Count; ++i)
                {
                    string[] oldSale = sales[i];
                    string[] newSale = new string[7];
                    for (int h = 0; h < 5; ++h)
                    {
                        newSale[h] = oldSale[h];
                    }
                
                    newSale[5] = (double.Parse(newSale[4]) * double.Parse(newSale[3])).ToString();
                    newSale[6] = oldSale[5];
                    sales[i] = newSale;
                }

                return sales;
            }
        }
        
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

        public string FilteredProduct
        {
            get => _filteredProduct;
            set
            {
                _filteredProduct = value;
                UpdateSales();
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectiveProducts));
            }
        }

        public List<string> SelectiveProducts
        {
            get
            {
                List<string[]> storeProducts = Product.GetProductBySubNameOrId(FilteredProduct);

                if (storeProducts == null)
                {
                    return new List<string>(0);
                }

                List<string> resultStoreProducts = new(storeProducts.Count + 1) { AllString };

                foreach (var storeProduct in storeProducts)
                {
                    resultStoreProducts.Add(storeProduct[Prod.id_product] + "  --  " + storeProduct[Prod.product_name]);
                }

                return resultStoreProducts;
            }
        }

        public string NumberOfProducts =>
            Sale.GetSumOfNumberOfProducts(FilteredProduct.Split(' ')[0], MinPrintDate, MaxPrintDate);

        public void UpdateSales()
        {
            OnPropertyChanged(nameof(Sales));
            OnPropertyChanged(nameof(SelectiveProducts));
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

            Printer.PrintDataGrid(printerSales, "Продажі станом на " + DateTime.UtcNow.ToString("dd.MM.yyyy"), new string[]
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