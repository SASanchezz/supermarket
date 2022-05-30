using supermarket.ViewModels.BaseClasses;
using System;

namespace supermarket.Navigation.ViewModels
{
    internal abstract class NavigatableViewModel : ViewModel
    {
        public Action<VMNavigationTypes> ChangeViewModel { get; set; }
    }
}
