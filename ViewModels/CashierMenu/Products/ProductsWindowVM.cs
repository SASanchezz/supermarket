using supermarket.ViewModels.BaseClasses;
using supermarket.Windows.CashierMenu.Products;

namespace supermarket.ViewModels.CashierMenu.Products
{
    internal class ProductsWindowVM : WindowViewModel<ProductsWindow, ProductsVM>
    {
        public ProductsWindowVM()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) 
                {
                    return; 
                }
                // window is shown
                ViewModel.UpdateProducts();
                ViewModel.UpdateSelectiveCategories();
            };
        }
    }
}