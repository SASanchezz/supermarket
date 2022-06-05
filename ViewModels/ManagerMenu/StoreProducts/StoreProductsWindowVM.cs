using System;
using System.Windows;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.ManagerMenu.StoreProducts;
using Prom = supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.Prom;
using NonProm = supermarket.ViewModels.ManagerMenu.StoreProducts.Changes.NonProm;
using StrProduct = supermarket.Models.StoreProduct;

namespace supermarket.ViewModels.ManagerMenu.StoreProducts
{
    internal class StoreProductsWindowVM : WindowViewModel<StoreProductsWindow, StoreProductsVM>
    {
        private NonProm.AddStoreProdcutWindowVM _nonProm_addStoreProdcutWindowVM;
        private NonProm.EditStoreProductWindowVM _nonProm_editStoreProdcutWindowVM;

        private Prom.AddStoreProdcutWindowVM _prom_addStoreProdcutWindowVM;
        private Prom.EditStoreProductWindowVM _prom_editStoreProdcutWindowVM;

        public StoreProductsWindowVM()
        {
            _nonProm_addStoreProdcutWindowVM = new NonProm.AddStoreProdcutWindowVM();
            _nonProm_editStoreProdcutWindowVM = new NonProm.EditStoreProductWindowVM();

            _prom_addStoreProdcutWindowVM = new Prom.AddStoreProdcutWindowVM();
            _prom_editStoreProdcutWindowVM = new Prom.EditStoreProductWindowVM();

            SetWindowsNavigation();

            Window.Closed += (sender, e) =>
            {
                _nonProm_addStoreProdcutWindowVM.Window.Close();
                _nonProm_editStoreProdcutWindowVM.Window.Close();
                _prom_addStoreProdcutWindowVM.Window.Close();
                _prom_editStoreProdcutWindowVM.Window.Close();
            };

            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = new WindowVMNavigator<ManagerStoreProducts>(new IWindowOpeningVM<ManagerStoreProducts>[] { ViewModel });

            windowsNavigator.SetWay(ManagerStoreProducts.AddNonPromStoreProduct,
                _nonProm_addStoreProdcutWindowVM.Window, GoToNonPromAddStoreProdcut);
            windowsNavigator.SetWay(ManagerStoreProducts.AddPromStoreProduct,
                _prom_addStoreProdcutWindowVM.Window, GoToPromAddStoreProdcut);

            windowsNavigator.SetWay(ManagerStoreProducts.EditStoreProduct,
                _nonProm_editStoreProdcutWindowVM.Window, GoToEditStoreProdcut);


            SetVisibilitySystem(_nonProm_addStoreProdcutWindowVM);
            SetVisibilitySystem(_nonProm_editStoreProdcutWindowVM);
            SetVisibilitySystem(_prom_addStoreProdcutWindowVM);
            SetVisibilitySystem(_prom_editStoreProdcutWindowVM);
        }

        private void GoToNonPromAddStoreProdcut()
        {
            SetDefaultClosedEventHandler(_nonProm_addStoreProdcutWindowVM);
        }
        private void GoToPromAddStoreProdcut()
        {
            SetDefaultClosedEventHandler(_prom_addStoreProdcutWindowVM);
        }

        private void GoToEditStoreProdcut()
        {
            if (ViewModel.SelectedStoreProduct == null)
            {
                throw new Exception("No selected item");
            }
            if (ViewModel.SelectedStoreProduct[StrProduct.promotional_product] == "False")
            {
                SetDefaultClosedEventHandler(_nonProm_editStoreProdcutWindowVM);
                _nonProm_editStoreProdcutWindowVM.ViewModel.SetData(ViewModel.SelectedStoreProduct);
            } else
            {
                SetDefaultClosedEventHandler(_prom_editStoreProdcutWindowVM);
                _prom_editStoreProdcutWindowVM.ViewModel.SetData(ViewModel.SelectedStoreProduct);
            }

            ViewModel.SelectedStoreProduct = null;
        }

        private void SetDefaultClosedEventHandler<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
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

    }
}
