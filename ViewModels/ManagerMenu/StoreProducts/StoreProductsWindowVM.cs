using System;
using System.Windows;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.StoreProducts.Changes;
using supermarket.Windows.ManagerMenu.StoreProducts;
using Prom = supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom;
using NonProm = supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsWindowVM : WindowViewModel<StoreProductsWindow, StoreProductsVM>
    {
        private NonProm.AddStoreProdcutWindowVM _nonProm_addStoreProductWindowVM;
        private Prom.AddStoreProdcutWindowVM _prom_addStoreProductWindowVM;
        
        private EditStoreProductWindowVM _editStoreProductWindowVM;

        public StoreProductsWindowVM()
        {
            _nonProm_addStoreProductWindowVM = new NonProm.AddStoreProdcutWindowVM();
            _prom_addStoreProductWindowVM = new Prom.AddStoreProdcutWindowVM();
            
            _editStoreProductWindowVM = new EditStoreProductWindowVM();

            SetUpdatingSystem();
            SetWindowsNavigation();

            Window.Closed += (sender, e) =>
            {
                _nonProm_addStoreProductWindowVM.Window.Close();
                _prom_addStoreProductWindowVM.Window.Close();
                _editStoreProductWindowVM.Window.Close();
            };
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = 
                new WindowVMNavigator<ManagerStoreProducts>(new IWindowOpeningVM<ManagerStoreProducts>[] { ViewModel });

            windowsNavigator.SetWay(ManagerStoreProducts.AddNonPromStoreProduct,
                _nonProm_addStoreProductWindowVM.Window);
            windowsNavigator.SetWay(ManagerStoreProducts.AddPromStoreProduct,
                _prom_addStoreProductWindowVM.Window);

            windowsNavigator.SetWay(ManagerStoreProducts.EditStoreProduct,
                _editStoreProductWindowVM.Window, OnOpeningEditStoreProduct);
            
            SetChangingEnabilityByOpeningAnotherWindow(_nonProm_addStoreProductWindowVM);
            SetChangingEnabilityByOpeningAnotherWindow(_prom_addStoreProductWindowVM);
            SetChangingEnabilityByOpeningAnotherWindow(_editStoreProductWindowVM);
        }

        private void OnOpeningEditStoreProduct()
        {
            if (ViewModel.SelectedStoreProduct == null)
            {
                throw new Exception("No selected item");
            }

            _editStoreProductWindowVM.SetSelectedStoreProduct(ViewModel.SelectedStoreProduct);
        }
        
        private void SetUpdatingSystem()
        {
            Window.IsEnabledChanged += (sender, e) =>
            { 
                // window is enabled
                if ((bool)e.NewValue)
                {
                    ViewModel.UpdateStoreProducts();
                }
            };
            
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) 
                {
                    return; 
                }
                // window is shown
                ViewModel.UpdateStoreProducts();
            };
        }

        private void SetChangingEnabilityByOpeningAnotherWindow<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
        
        private void SetChangingEnabilityByOpeningAnotherWindow<TWindow>(WindowViewModel<TWindow> windowVM)
            where TWindow : Window, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

    }
}
