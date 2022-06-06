﻿using supermarket.Utils;
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
        private string _initUPCProm = "";
        private string _changedUPCProm = "";
        private string _UPCFather = "";

        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _closeCommand;

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
            set { }
        }

        public RelayCommand<object> UpdateCommand
        {
            get => _updateCommand ??= new RelayCommand<object>(_ => UpdateStoreProduct(), CanExecute);
        }
        public RelayCommand<object> DeleteCommand
        {
            get => _deleteCommand ??= new RelayCommand<object>(_ => DeleteStoreProduct());
        }
        public RelayCommand<object> CloseCommand
        {
            get => _closeCommand ??= new RelayCommand<object>(_ => CloseWindow());
        }

        public override void SetData(string[] data)
        {
            _initUPCProm = data[StrProduct.UPC_prom];
            ChangedUPCProm = data[StrProduct.UPC_prom];
            UPCFather = data[StrProduct.UPC];
        }

        private void UpdateStoreProduct()
        {
            string result = StoreProductEditValidator.ValidateProm(_initUPCProm, _changedUPCProm, _UPCFather);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            StrProduct.UpdatePromStoreProduct(_initUPCProm, _changedUPCProm, _UPCFather);

            CloseWindow();
        }

        private void DeleteStoreProduct()
        {
            StrProduct.DeleteStoreProductByUPC(_initUPCProm);
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(UPCFather)
                && !string.IsNullOrWhiteSpace(ChangedUPCProm);
        }
    }
}
