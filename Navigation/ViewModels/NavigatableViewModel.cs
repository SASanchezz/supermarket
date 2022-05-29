using supermarket.ViewModels;
using System;

namespace supermarket.Navigation.ViewModels
{
    internal abstract class NavigatableViewModel : ViewModel
    {
        public Action<VMNavigationTypes> ChangeViewModel { get; set; }
    }
}
