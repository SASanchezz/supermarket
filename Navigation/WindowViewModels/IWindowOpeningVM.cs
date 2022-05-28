using System;

namespace supermarket.Navigation.WindowViewModels
{
    internal interface IWindowOpeningVM<Type> where Type : Enum
    {
        public Action<Type> OpenWindowViewModel { get; set; }
    }
}
