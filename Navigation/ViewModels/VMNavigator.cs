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
                Way way = null;
                for (int i = 0; i < _ways.Count; i++)
                {
                    if (_ways[i].Type == type)
                    {
                        way = _ways[i];
                    }
                }
                
                if (way == null)
                {
                    throw new Exception("There is no way");
                }

                way.Handler?.Invoke();
                _changeViewModel(way.ViewModel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetWay(VMNavigationTypes type, NavigatableViewModel viewModel)
        {
            for (int i = 0; i < _ways.Count; i++)
            {
                if (_ways[i].Type == type)
                {
                    throw new Exception("This way is already set");
                }
            }

            viewModel.ChangeViewModel = Navigate;
            _ways.Add(new Way(type, viewModel));
        }

        public void SetWay(VMNavigationTypes type, NavigatableViewModel viewModel, Action handler)
        {
            for (int i = 0; i < _ways.Count; i++)
            {
                if (_ways[i].Type == type)
                {
                    throw new Exception("This way is already set");
                }
            }

            viewModel.ChangeViewModel = Navigate;
            _ways.Add(new Way(type, viewModel, handler));
        }

        class Way
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
