using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu.Products.Changes
{
    internal class EditProductVM : ViewModel
    {
        private RelayCommand<object> _closeCommand;

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        public Action Close { get; set; }
    }
}
