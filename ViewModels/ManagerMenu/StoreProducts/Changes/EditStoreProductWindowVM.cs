﻿using supermarket.Navigation.ViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts.Changes;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts.Changes
{
    /*
     * Controls EditStoreProduct Window in StoreProducts of ManagerMenu
     */
    internal class EditStoreProductWindowVM : WindowViewModel<EditStoreProductWindow>
    {
        private NonProm.EditStoreProductVM _nonProm_editStoreProductVM;
        private Prom.EditStoreProductVM _prom_editStoreProductVM;
        
        private NavigatableViewModel<EditStoreProductViewsTypes> _currentViewModel;

        public EditStoreProductWindowVM()
        {
            _nonProm_editStoreProductVM = new NonProm.EditStoreProductVM();
            _prom_editStoreProductVM = new Prom.EditStoreProductVM();
            
            _nonProm_editStoreProductVM.CloseWindow = Window.Close;
            _prom_editStoreProductVM.CloseWindow = Window.Close;

            ViewsNavigator = 
                new VMNavigator<EditStoreProductViewsTypes>((vm) => CurrentViewModel = vm);
            
            ViewsNavigator.SetWay(EditStoreProductViewsTypes.EditNonPromStoreProduct, _nonProm_editStoreProductVM);
            ViewsNavigator.SetWay(EditStoreProductViewsTypes.EditPromStoreProduct, _prom_editStoreProductVM);
            CurrentViewModel = _nonProm_editStoreProductVM;
        }
        
        public VMNavigator<EditStoreProductViewsTypes> ViewsNavigator { get; }
        
        public NavigatableViewModel<EditStoreProductViewsTypes> CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public void SetSelectedStoreProduct(string[] product)
        {
            if (product[StrProduct.promotional_product] == "False")
            {
                ViewsNavigator.Navigate(EditStoreProductViewsTypes.EditNonPromStoreProduct);
            } 
            else
            {
                ViewsNavigator.Navigate(EditStoreProductViewsTypes.EditPromStoreProduct);
            }
            
            CurrentViewModel.SetData(product);
        }
    }
}
