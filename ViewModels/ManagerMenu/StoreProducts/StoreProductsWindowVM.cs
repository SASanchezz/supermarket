using System;
using System.Windows;
using supermarket.Navigation.ViewModels;
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
        private NonProm.AddStoreProdcutWindowVM _nonProm_addStoreProdcutWindowVM;
        private Prom.AddStoreProdcutWindowVM _prom_addStoreProdcutWindowVM;
        
        private EditStoreProductWindowVM _editStoreProdcutWindowVM;

        public StoreProductsWindowVM()
        {
            _nonProm_addStoreProdcutWindowVM = new NonProm.AddStoreProdcutWindowVM();
            _prom_addStoreProdcutWindowVM = new Prom.AddStoreProdcutWindowVM();
            
            _editStoreProdcutWindowVM = new EditStoreProductWindowVM();

            SetWindowsNavigation();

            Window.Closed += (sender, e) =>
            {
                _nonProm_addStoreProdcutWindowVM.Window.Close();
                _prom_addStoreProdcutWindowVM.Window.Close();
                
                _editStoreProdcutWindowVM.Window.Close();
            };
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = 
                new WindowVMNavigator<ManagerStoreProducts>(new IWindowOpeningVM<ManagerStoreProducts>[] { ViewModel });

            windowsNavigator.SetWay(ManagerStoreProducts.AddNonPromStoreProduct,
                _nonProm_addStoreProdcutWindowVM.Window, OnOpeningNonPromAddStoreProduct);
            windowsNavigator.SetWay(ManagerStoreProducts.AddPromStoreProduct,
                _prom_addStoreProdcutWindowVM.Window, OnOpeningPromAddStoreProduct);

            windowsNavigator.SetWay(ManagerStoreProducts.EditStoreProduct,
                _editStoreProdcutWindowVM.Window, OnOpeningEditStoreProduct);


            SetVisibilitySystem(_nonProm_addStoreProdcutWindowVM);
            SetVisibilitySystem(_prom_addStoreProdcutWindowVM);
            
            SetVisibilitySystem(_editStoreProdcutWindowVM);
        }

        private void OnOpeningNonPromAddStoreProduct()
        {
            SetUpdatingSystem(_nonProm_addStoreProdcutWindowVM);
        }
        
        private void OnOpeningPromAddStoreProduct()
        {
            SetUpdatingSystem(_prom_addStoreProdcutWindowVM);
        }

        private void OnOpeningEditStoreProduct()
        {
            if (ViewModel.SelectedStoreProduct == null)
            {
                throw new Exception("No selected item");
            }
            
            if (ViewModel.SelectedStoreProduct[StrProduct.promotional_product] == "False")
            {
                SetUpdatingSystem(_editStoreProdcutWindowVM);
                _editStoreProdcutWindowVM.ViewsNavigator.Navigate(EditStoreProductViewsTypes.EditNonPromStoreProduct);
                _editStoreProdcutWindowVM.CurrentViewModel.SetData(ViewModel.SelectedStoreProduct);
            } 
            else
            {
                SetUpdatingSystem(_editStoreProdcutWindowVM);
                _editStoreProdcutWindowVM.ViewsNavigator.Navigate(EditStoreProductViewsTypes.EditPromStoreProduct);
                _editStoreProdcutWindowVM.CurrentViewModel.SetData(ViewModel.SelectedStoreProduct);
            }

            ViewModel.SelectedStoreProduct = null;
        }

        private void SetUpdatingSystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
        
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return;
        
                ViewModel.UpdateStoreProducts();
            };
        }
        
        private void SetUpdatingSystem<TWindow>(WindowViewModel<TWindow> windowVM)
            where TWindow : Window, new()
        {
        
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return;
        
                ViewModel.UpdateStoreProducts();
            };
        }

        private void SetVisibilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
        
        private void SetVisibilitySystem<TWindow>(WindowViewModel<TWindow> windowVM)
            where TWindow : Window, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

    }
}
