using supermarket.ViewModels.BaseClasses;
using System;

namespace supermarket.Navigation.ViewModels
{
    internal abstract class NavigatableViewModel<T> : ViewModel where T : Enum
    {
        public Action<T> ChangeViewModel { get; set; }

        public virtual void SetData(string[] data)
        {
            throw new NotImplementedException();
        }
    }
}
