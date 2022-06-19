using supermarket.Utils;
using System;
using supermarket.Middlewares.StoreProduct;
using StrProduct = supermarket.Models.StoreProduct;
using System.Windows;
using System.Collections.Generic;
using supermarket.Navigation.ViewModels;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom
{
    /*
     * Controls ManagerEditEmployee View
     */
    internal class EditStoreProductVM : NavigatableViewModel<EditStoreProductViewsTypes>
    {
        private string _initUPCProm;
        private string _changedUPCProm;
        private string _UPCFather;
        private string _amount;

        int ERROR = -1;
        int GOOD = 1;
        string deleteErrorMessage = "Цей чек використовується";

        public EditStoreProductVM()
        {
            UpdateCommand = new RelayCommand<object>(_ => UpdateStoreProduct(), CanExecute);
            DeleteCommand = new RelayCommand<object>(_ => DeleteStoreProduct());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string ChangedUPCProm
        {
            get => _changedUPCProm;
            set
            {
                _changedUPCProm = value;
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

        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public List<string> SelectiveFatherUPC
        {
            get
            {
                List<string[]> UPCs = StrProduct.GetFatherStoreProductLikeSubUPC(_UPCFather);
                if (UPCs == null) return new List<string>(0);
                List<string> resultUPCs = new(UPCs.Count);
                for (int i = 0; i < UPCs.Count; i++)
                {
                    resultUPCs.Add(UPCs[i][StrProduct.UPC] + "  --  " + UPCs[i][StrProduct.product_name]);
                }

                return resultUPCs;
            }
        }

        public RelayCommand<object> UpdateCommand { get; }
        
        public RelayCommand<object> DeleteCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

        public override void SetData(string[] data)
        {
            _initUPCProm = data[StrProduct.UPC];
            ChangedUPCProm = data[StrProduct.UPC];

            UPCFather = StrProduct.GetStoreProductByPromUPC(data[StrProduct.UPC])[StrProduct.UPC];

            Amount = data[StrProduct.products_number];
        }

        private void UpdateStoreProduct()
        {
            string result = StoreProductEditValidator.ValidateProm(_initUPCProm, _changedUPCProm, _UPCFather.Split(' ')[0], Amount);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            StrProduct.UpdatePromStoreProduct(_initUPCProm, _changedUPCProm, _UPCFather.Split(' ')[0], Amount);

            CloseWindow();
        }

        private void DeleteStoreProduct()
        {
            int response = StrProduct.DeleteStoreProductByUPC(_initUPCProm);
            if (response == ERROR) MessageBox.Show(deleteErrorMessage);
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(UPCFather)
                && !string.IsNullOrWhiteSpace(ChangedUPCProm)
                && !string.IsNullOrWhiteSpace(Amount);
        }
    }
}
