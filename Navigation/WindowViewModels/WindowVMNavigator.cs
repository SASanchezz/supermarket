using System;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.Navigation.WindowViewModels
{
    internal class WindowVMNavigator<TEnum> where TEnum : Enum
    {
        private Dictionary<TEnum, Action> _ways;

        public WindowVMNavigator(IWindowOpeningVM<TEnum>[] viewModels)
        {
            SetWindowOpeningVM(viewModels);
            _ways = new Dictionary<TEnum, Action>();
        }

        public void Navigate(TEnum type)
        {
            try
            {
                if (!_ways.ContainsKey(type))
                {
                    throw new Exception("There is no way");
                }
                _ways[type]();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetWay(TEnum type, Window window)
        {
            if (_ways.ContainsKey(type))
            {
                throw new Exception("This way is already set");
            }

            _ways.Add(type, window.Show);
        }

        public void SetWay(TEnum type, Window window, Action handler)
        {
            if (_ways.ContainsKey(type))
            {
                throw new Exception("This way is already set");
            }

            Action action = handler + window.Show;
            _ways.Add(type, action);
        }

        private void SetWindowOpeningVM(IWindowOpeningVM<TEnum>[] viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                viewModel.OpenWindowViewModel = Navigate;
            }
        }
    }
}
