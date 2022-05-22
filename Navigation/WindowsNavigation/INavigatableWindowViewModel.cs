using System.Windows;

namespace supermarket.Navigation.WindowsNavigation
{
    public interface INavigatableWindowViewModel
    {
        public Window Window { get; }
        public WindowTypes WindowType { get; }
    }
}
