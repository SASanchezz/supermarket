using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class AddProductVM : ViewModel
    {
        private RelayCommand<object> _closeCommand;
        public Action Close { get; set; }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
    }
}
