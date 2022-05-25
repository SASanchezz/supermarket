using System.Windows;

namespace supermarket.Navigation.WindowsNavigation
{
    public interface INavigatableWindowVM
    {
        public Window Window { get; }
        public WindowTypes WindowType { get; }
    }
}
