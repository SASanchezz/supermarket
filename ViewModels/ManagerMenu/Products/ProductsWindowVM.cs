using supermarket.Windows.ManagerMenu.Products;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsWindowVM : WindowViewModel<ProductsWindow, ProductsVM>
    {
        public ProductsWindowVM()
        {
            Window.Show();
        }
    }
}
