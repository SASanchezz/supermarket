using System;

namespace supermarket.Navigation.WindowsNavigation
{
    internal interface IWindowOpeningVM
    {
        Action<WindowTypes> OpenWindowViewModel { get; set; }
    }
}
