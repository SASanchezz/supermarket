using System;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.Navigation.ViewModels
{
    internal class VMNavigator<TEnum> where TEnum : Enum
    {
        private List<Way<TEnum>> _ways;
        private Action<NavigatableViewModel<TEnum>> _changeViewModel;

        public VMNavigator(Action<NavigatableViewModel<TEnum>> changeViewModel)
        {
            _ways = new List<Way<TEnum>>();
            _changeViewModel = changeViewModel;
        }

        public void Navigate(TEnum type)
        {
            try
            {
                foreach (var way in _ways)
                {
                    if (!way.Type.Equals(type)) continue;
                    
                    way.Handler?.Invoke();
                    _changeViewModel(way.ViewModel);
                    return;
                }
                
                throw new Exception("There is no way");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetWay(TEnum type, NavigatableViewModel<TEnum> viewModel)
        {
            foreach (var way in _ways)
            {
                if (way.Type.Equals(type))
                {
                    throw new Exception("This way is already set");
                }
            }

            viewModel.ChangeViewModel = Navigate;
            _ways.Add(new Way<TEnum>(type, viewModel));
        }

        public void SetWay(TEnum type, NavigatableViewModel<TEnum> viewModel, Action handler)
        {
            foreach (var way in _ways)
            {
                if (way.Type.Equals(type))
                {
                    throw new Exception("This way is already set");
                }
            }

            viewModel.ChangeViewModel = Navigate;
            _ways.Add(new Way<TEnum>(type, viewModel, handler));
        }

        private class Way<T> where T : Enum
        {
            public Way(T type, NavigatableViewModel<T> viewModel)
            {
                Type = type;
                ViewModel = viewModel;
            }

            public Way(T type, NavigatableViewModel<T> viewModel, Action handler) : this(type, viewModel)
            {
                Handler = handler;
            }

            public T Type { get; private set; }

            public NavigatableViewModel<T> ViewModel { get; private set; }

            public Action Handler { get; private set; }
        }
    }
}
