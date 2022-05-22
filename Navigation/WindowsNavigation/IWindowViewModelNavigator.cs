namespace supermarket.Navigation.WindowsNavigation
{
    internal interface IWindowViewModelNavigator
    {
        public bool IsEnabled { get; set; }
        void Navigate(WindowTypes type);
    }
}
