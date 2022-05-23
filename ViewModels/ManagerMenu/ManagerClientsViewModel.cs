using supermarket.Navigation.WindowsNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu
{
    internal class ManagerClientsViewModel : IWindowOpeningViewModel
    {
        public Action<WindowTypes> OpenWindowViewModel { get; set; }
    }
}
