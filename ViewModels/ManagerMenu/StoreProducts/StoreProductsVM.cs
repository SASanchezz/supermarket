using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using supermarket.Data;
using System;
using System.Collections.Generic;
using supermarket.Models;
using StrProduct = supermarket.Models.StoreProduct;
using supermarket.Printing;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsVM : ViewModel, IWindowOpeningVM<ManagerStoreProducts>
    {
        private const string AllString = Constants.AllString;

        private List<string[]> _storeProducts;
        private string _selectedProm = AllString;
        private string _subUPC = "";

        public StoreProductsVM()
        {
            Open_nonProm_AddStoreProductWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerStoreProducts.AddNonPromStoreProduct));
            Open_Prom_AddStoreProductWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerStoreProducts.AddPromStoreProduct));
            OpenEditStoreProductWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerStoreProducts.EditStoreProduct));

            GetProductsOfAllReceiptsCommand = new RelayCommand<object>(_ => GetProductsOfAllReceipts());
            PrintStoreProductsCommand = new RelayCommand<object>(_ => PrintStoreProducts());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public Action<ManagerStoreProducts> OpenWindowViewModel { get; set; }

        public List<string[]> StoreProducts
        {
            get => _storeProducts;
            set
            {
                _storeProducts = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> Open_nonProm_AddStoreProductWindowCommand { get; }

        public RelayCommand<object> Open_Prom_AddStoreProductWindowCommand { get; }

        public RelayCommand<object> OpenEditStoreProductWindowCommand { get; }

        public RelayCommand<object> PrintStoreProductsCommand { get; }
        public RelayCommand<object> GetProductsOfAllReceiptsCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public string[] SelectedStoreProduct { get; set; }

        public string SelectedProm
        {
            get => _selectedProm;
            set
            {
                _selectedProm = value ?? SelectiveProms[0];
                UpdateStoreProducts();
                OnPropertyChanged();
            }
        }

        public string SubUPC
        {
            get => _subUPC;
            set
            {
                _subUPC = value;
                UpdateStoreProducts();
                OnPropertyChanged();
            }
        }

        public static string[] SelectiveProms 
        { 
            get 
            {
                var result = new string[3];
                result[0] = AllString;
                
                for(int i = 1; i < 3; i++)
                {
                    result[i] = Proms.promNames[i - 1];
                }

                return result;
            }
        }

        public void GetProductsOfAllReceipts()
        {
            StoreProducts = StrProduct.GetProductsOfAllReceipts(DateTime.Today);
        }

        public void UpdateStoreProducts()
        {
            StoreProducts = StrProduct.GetAllStoreProducts(_selectedProm , _subUPC);
        }

        private void PrintStoreProducts()
        {
            var printerStoreProducts = new List<string[]>();

            for (int i = 0; i < StoreProducts.Count; ++i)
            {
                printerStoreProducts.Add(new string[StoreProducts[0].Length - 1]);

                for (int h = 0; ; ++h)
                {
                    if (h == 2)
                    {
                        printerStoreProducts[i].SetValue(StoreProducts[i][6], h);
                        break;
                    }
                    printerStoreProducts[i].SetValue(StoreProducts[i][h], h);
                }

                for (int h = 3; h < StoreProducts[0].Length - 1; ++h)
                {
                    printerStoreProducts[i].SetValue(StoreProducts[i][h], h);
                }
            }

            new Printer().PrintDataGrid(printerStoreProducts, "Товари в магазині станом на " + DateTime.UtcNow.ToString("dd.MM.yyyy"), new string[]
            {
                "UPC",
                "UPC Акції",
                "Назва продукту",
                "Ціна",
                "Кількість",
                "Акційний продукт"
            });
        }
    }
}
