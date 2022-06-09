using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.Products.Changes;
using supermarket.Windows.ManagerMenu.Products;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsWindowVM : WindowViewModel<ProductsWindow, ProductsVM>
    {
        private AddProductWindowVM _addProductWindowVM;
        private EditProductWindowVM _editProductWindowVM;

        public ProductsWindowVM()
        {
            _addProductWindowVM = new AddProductWindowVM();
            _editProductWindowVM = new EditProductWindowVM();

            SetUpdatingSystem();
            SetResettingSystem();
            SetWindowsNavigation();
            
            Window.Closed += (sender, e) =>
            {
                _addProductWindowVM.Window.Close();
                _editProductWindowVM.Window.Close();
            };
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = 
                new WindowVMNavigator<ManagerProducts>(new IWindowOpeningVM<ManagerProducts>[] { ViewModel });

            windowsNavigator.SetWay(ManagerProducts.AddProduct, _addProductWindowVM.Window);
            windowsNavigator.SetWay(ManagerProducts.EditProduct, _editProductWindowVM.Window, OnOpeningEditProduct);
            
            SetChangingEnabilityByOpeningAnotherWindow(_addProductWindowVM);
            SetChangingEnabilityByOpeningAnotherWindow(_editProductWindowVM);
        }

        private void OnOpeningEditProduct()
        {
            if (ViewModel.SelectedProduct == null)
            {
                throw new Exception("No selected item");
            }
            
            _editProductWindowVM.ViewModel.SetData(ViewModel.SelectedProduct);
        }

        private void SetUpdatingSystem()
        {
            Window.IsEnabledChanged += (sender, e) =>
            { 
                // window is enabled
                if ((bool)e.NewValue)
                {
                    ViewModel.UpdateProducts();
                }
            };
        }
        
        private void SetResettingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) 
                {
                    return; 
                }
                // window is shown
                ViewModel.Reset();
            };
        }

        private void SetChangingEnabilityByOpeningAnotherWindow<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

    }
}
