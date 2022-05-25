using System;
using System.Windows;

namespace supermarket.Navigation.WindowVM
{
    public interface INavigatableWindowVM<Type> where Type : Enum
    {
        public Type WindowType { get; }
        public Window Window { get; }
    }
}
