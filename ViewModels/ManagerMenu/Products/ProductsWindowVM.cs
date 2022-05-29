using supermarket.Navigation.WindowViewModels;
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

            _windowsNavigator = new WindowVMNavigator<ManagerProducts>(new IWindowOpeningVM<ManagerProducts>[] { ViewModel });

            _windowsNavigator.SetWay(ManagerProducts.AddProduct, _addProductWindowVM.Window);
            _windowsNavigator.SetWay(ManagerProducts.EditProduct, _editProductWindowVM.Window);

            Window.Closed += (object sender, EventArgs e) =>
            {
                _addProductWindowVM.Window.Close();
                _editProductWindowVM.Window.Close();
            };

            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }

        private void GoToAddProduct()
        {
            //SetDefaultClosedEventHandler(_addProductWindowVM);

            _addProductWindowVM.Window.Show();
        }

        private void GoToEditProduct()
        {
            try
            {
                if (ViewModel.SelectedProduct == null)
                {
                    throw new Exception("No selected item");
                }
                //SetDefaultClosedEventHandler(_addProductWindowVM);

                //_editProductWindowVM.ViewModel.SetData(ViewModel.SelectedProduct);
                ViewModel.SelectedProduct = null;
                _editProductWindowVM.Window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
