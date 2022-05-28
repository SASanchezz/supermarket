using supermarket.Windows.ManagerMenu.Products.Changes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    class AddProductWindowVM : WindowViewModel<AddProductWindow, AddProductVM>
    {
        public AddProductWindowVM()
        {
            ViewModel.Close = Window.Close;
        }
    }
}
