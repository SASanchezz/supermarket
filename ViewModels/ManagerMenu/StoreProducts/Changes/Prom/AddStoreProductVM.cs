using supermarket.Utils;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using supermarket.Middlewares.StoreProduct;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom
{
    internal class AddStoreProductVM : ViewModel
    {
        private string _UPCProm = "";
        private string _UPCFather = "";

        public AddStoreProductVM()
        {
            AddStoreProductCommand = new RelayCommand<object>(_ => AddStoreProduct(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public string UPCProm
        {
            get => _UPCProm;
            set
            {
                _UPCProm = value;
                OnPropertyChanged();
            }
        }

        public string UPCFather
        {
            get => _UPCFather;
            set
            {
                _UPCFather = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectiveFatherUPC));
            }
        }

        public List<string> SelectiveFatherUPC
        {
            get
            {
                List<string[]> UPCs = StoreProduct.GetFatherStoreProductLikeSubUPC(_UPCFather);
                
                if (UPCs == null)
                {
                    return new List<string>(0);
                }
                
                List<string> resultUPCs = new(UPCs.Count);
                for (int i = 0; i < UPCs.Count; i++)
                {
                    resultUPCs.Add(UPCs[i][StoreProduct.UPC] + "  --  " + UPCs[i][StoreProduct.product_name]);
                }

                return resultUPCs;
            }
        }

        public RelayCommand<object> AddStoreProductCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        private void AddStoreProduct()
        {
            ////Validates entered information
            string result = StoreProductAddValidator.ValidateProm(UPCFather.Split(' ')[0], UPCProm);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            StoreProduct.AddPromStoreProduct(UPCFather.Split(' ')[0], UPCProm);
            
            ResetFields();
            CloseWindow();
        }

        private void ResetFields()
        {
            UPCFather = "";
            UPCProm = "";  
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(UPCFather)
                && !string.IsNullOrWhiteSpace(UPCProm);
        }
    }
}
