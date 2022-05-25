using System;

namespace supermarket.Navigation.WindowVM
{
    internal interface IWindowOpeningVM<Type> where Type : Enum
    {
        public Action<Type> OpenWindowViewModel { get; set; }
    }
}
