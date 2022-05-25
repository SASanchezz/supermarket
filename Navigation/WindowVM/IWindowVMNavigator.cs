using System;

namespace supermarket.Navigation.WindowVM
{
    internal interface IWindowVMNavigator<Type> where Type : Enum
    {
        public bool IsEnabled { get; set; }
        void Navigate(Type type);
    }
}
