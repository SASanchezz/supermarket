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
            windowsNavigator.SetWay(ManagerProducts.EditProduct, _editProductWindowVM.Window);
            
            SetVisibilitySystem(_addProductWindowVM);
            SetVisibilitySystem(_editProductWindowVM);
        }

        private void GoToAddProduct()
        {
            SetDefaultClosedEventHandler(_addProductWindowVM);
        }

        private void GoToEditProduct()
        {
            if (ViewModel.SelectedProduct == null)
            {
                throw new Exception("No selected item");
            }

            SetDefaultClosedEventHandler(_editProductWindowVM);

          //  _editProductWindowVM.ViewModel.SetData(ViewModel.SelectedProduct);
            ViewModel.SelectedProduct = null;
        }

        private void SetDefaultClosedEventHandler<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            string filteredName = ViewModel.FilteredName;
            string selectedCategory = ViewModel.SelectedCategory;

            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return;
                
                ViewModel.FilteredName = filteredName;
                ViewModel.SelectedCategory = selectedCategory;
                ViewModel.UpdateProducts();
                //MessageBox.Show("Data updated");
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
