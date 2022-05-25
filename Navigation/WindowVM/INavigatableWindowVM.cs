using System;
using System.Windows;

namespace supermarket.Navigation.WindowVM
{
    internal interface INavigatableWindowVM<Type> where Type : Enum
    {
        public Type WindowType { get; }
        public Window Window { get; }
    }
}
