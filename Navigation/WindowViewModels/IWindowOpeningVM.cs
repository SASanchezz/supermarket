using System;

namespace supermarket.Navigation.WindowViewModels
{
    internal interface IWindowOpeningVM<TEnum> where TEnum : Enum
    {
        public Action<TEnum> OpenWindowViewModel { get; set; }
    }
}
