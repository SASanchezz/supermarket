using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using supermarket.Data;
using System;
using System.Collections.Generic;
using StrProduct = supermarket.Models.StoreProduct;
using supermarket.Printing;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsVM : ViewModel, IWindowOpeningVM<ManagerStoreProducts>
    {
        private const string AllString = "Всі";

        private List<string[]> _storeProducts;
        private string[] _selectedStoreProduct;
        private string _selectedProm = AllString;
        private string _subUPC = "";

        private RelayCommand<object> _open_nonProm_AddStoreProductWindowCommand;
        private RelayCommand<object> _open_Prom_AddStoreProductWindowCommand;

        private RelayCommand<object> _openEditStoreProductWindowCommand;

        private RelayCommand<object> _printStoreProductsCommand;

        private RelayCommand<object> _closeCommand;

        public StoreProductsVM()
        {
            UpdateStoreProducts();
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

        public RelayCommand<object> Open_nonProm_AddStoreProductWindowCommand
        {
            get => _open_nonProm_AddStoreProductWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerStoreProducts.AddNonPromStoreProduct));
        }

        public RelayCommand<object> Open_Prom_AddStoreProductWindowCommand
        {
            get => _open_Prom_AddStoreProductWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerStoreProducts.AddPromStoreProduct));
        }

        public RelayCommand<object> OpenEditStoreProductWindowCommand
        {
            get => _openEditStoreProductWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerStoreProducts.EditStoreProduct));
        }

        public RelayCommand<object> PrintStoreProductsCommand
        {
            get
            {
                return _printStoreProductsCommand ??= new RelayCommand<object>(_ => PrintStoreProducts());
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get => _closeCommand ??= new RelayCommand<object>(_ => CloseWindow());
        }

        public string[] SelectedStoreProduct
        {
            get => _selectedStoreProduct;
            set
            {
                _selectedStoreProduct = value;
                OnPropertyChanged();
            }
        }

        public string SelectedProm
        {
            get => _selectedProm;
            set
            {
                _selectedProm = value;
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

        public static string[] SelectiveProms { 
            get {
                string[] result = new string[3];
                result[0] = AllString;
                for(int i = 1; i < 3; i++)
                {
                    result[i] = Proms.promNames[i - 1];
                }

                return result;
            }   
                private set {} }

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

            Printer.PrintDataGrid(printerStoreProducts, new string[]
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
