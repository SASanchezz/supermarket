using supermarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.Navigation.ViewModels
{
    internal class VMNavigator
    {
        private List<Way> _ways;
        private Action<NavigatableViewModel> _changeViewModel;

        public VMNavigator(Action<NavigatableViewModel> changeViewModel)
        {
            _ways = new List<Way>();
            _changeViewModel = changeViewModel;
        }

        public void Navigate(VMNavigationTypes type)
        {
            try
            {
                foreach (var way in _ways)
                {
                    if (way.Type != type) continue;
                    
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

        public void SetWay(VMNavigationTypes type, NavigatableViewModel viewModel)
        {
            foreach (var way in _ways)
            {
                if (way.Type == type)
                {
                    throw new Exception("This way is already set");
                }
            }

            viewModel.ChangeViewModel = Navigate;
            _ways.Add(new Way(type, viewModel));
        }

        public void SetWay(VMNavigationTypes type, NavigatableViewModel viewModel, Action handler)
        {
            foreach (var way in _ways)
            {
                if (way.Type == type)
                {
                    throw new Exception("This way is already set");
                }
            }

            viewModel.ChangeViewModel = Navigate;
            _ways.Add(new Way(type, viewModel, handler));
        }

        private class Way
        {
            public Way(VMNavigationTypes type, NavigatableViewModel viewModel)
            {
                Type = type;
                ViewModel = viewModel;
            }

            public Way(VMNavigationTypes type, NavigatableViewModel viewModel, Action handler) : this(type, viewModel)
            {
                Handler = handler;
            }

            public VMNavigationTypes Type { get; private set; }

            public NavigatableViewModel ViewModel { get; private set; }

            public Action Handler { get; private set; }
        }
    }
}
