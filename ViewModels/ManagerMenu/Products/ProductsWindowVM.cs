﻿using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.Products.Changes;
using supermarket.Windows.ManagerMenu.Products;
using System;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsWindowVM : WindowViewModel<ProductsWindow, ProductsVM>
    {
        private WindowVMNavigator<ManagerProducts> _windowsNavigator;

        private AddProductWindowVM _addProductWindowVM;
        private EditProductWindowVM _editProductWindowVM;

        public ProductsWindowVM()
        {
            _addProductWindowVM = new AddProductWindowVM();
            _editProductWindowVM = new EditProductWindowVM();

            SetVisibilitySystem(_addProductWindowVM);
            SetVisibilitySystem(_editProductWindowVM);

            _windowsNavigator = new WindowVMNavigator<ManagerProducts>(new IWindowOpeningVM<ManagerProducts>[] { ViewModel });

            _windowsNavigator.SetWay(ManagerProducts.AddProduct, _addProductWindowVM.Window);
            _windowsNavigator.SetWay(ManagerProducts.EditProduct, _editProductWindowVM.Window);

            Window.Closed += (sender, e) =>
            {
                _addProductWindowVM.Window.Close();
                _editProductWindowVM.Window.Close();
            };

            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
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

        private void SetDefaultClosedEventHandler<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM)
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            string filteredName = ViewModel.FilteredName;
            string selectedCategory = ViewModel.SelectedCategory;

            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue.Equals(false))
                {
                    ViewModel.FilteredName = filteredName;
                    ViewModel.SelectedCategory = selectedCategory;
                    ViewModel.UpdateProducts();
                    //MessageBox.Show("Data updated");
                }
            };
        }

        private void SetVisibilitySystem<WindowType, VMType>(WindowViewModel<WindowType, VMType> windowVM)
            where WindowType : Window, new()
            where VMType : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }

    }
}
