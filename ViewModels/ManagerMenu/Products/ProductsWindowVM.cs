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
            SetWindowsNavigation();
            
            Window.Closed += (sender, e) =>
            {
                _addProductWindowVM.Window.Close();
                _editProductWindowVM.Window.Close();
            };

            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }

        private void SetWindowsNavigation()
        {
            var windowsNavigator = new WindowVMNavigator<ManagerProducts>(new IWindowOpeningVM<ManagerProducts>[] { ViewModel });

            windowsNavigator.SetWay(ManagerProducts.AddProduct, _addProductWindowVM.Window);
            windowsNavigator.SetWay(ManagerProducts.EditProduct, _editProductWindowVM.Window, OnOpeningEditProductHandler);
            
            SetVisibilitySystem(_addProductWindowVM);
            SetVisibilitySystem(_editProductWindowVM);
        }

        private void OnOpeningEditProductHandler()
        {
            if (ViewModel.SelectedProduct == null)
            {
                throw new Exception("No selected item");
            }
            
            _editProductWindowVM.ViewModel.SetData(ViewModel.SelectedProduct);
            ViewModel.SelectedProduct = null;
        }

        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return; // window is hiden
                // window is shown
                ViewModel.UpdateProducts();
            };
            SetUpdatingAfterHiden(_addProductWindowVM);
            SetUpdatingAfterHiden(_editProductWindowVM);
        }
        
        private void SetUpdatingAfterHiden<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return; // window is shown
                // window is hiden
                ViewModel.UpdateProducts();
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
