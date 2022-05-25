namespace supermarket.Navigation.WindowsNavigation
{
    internal interface IWindowVMNavigator
    {
        public bool IsEnabled { get; set; }
        void Navigate(WindowTypes type);
    }
}
