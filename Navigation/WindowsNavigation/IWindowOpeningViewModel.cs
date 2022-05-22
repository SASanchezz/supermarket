using System;

namespace supermarket.Navigation.WindowsNavigation
{
    internal interface IWindowOpeningViewModel
    {
        Action<WindowTypes> OpenWindowViewModel { get; set; }
    }
}
