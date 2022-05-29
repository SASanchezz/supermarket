using supermarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.Navigation.WindowViewModels
{
    internal class WindowVMNavigator<Type> where Type : Enum
    {
        private Dictionary<Type, Window> _ways;

        public WindowVMNavigator(IWindowOpeningVM<Type>[] viewModels)
        {
            SetWindowOpeningVM(viewModels);
            _ways = new Dictionary<Type, Window>();
        }

        public void Navigate(Type type)
        {
            if (!_ways.ContainsKey(type))
            {
                throw new Exception("There is no way");
            }
            _ways[type].Show();
        }

        public void SetWay(Type type, Window windowViewModel)
        {
            if (_ways.ContainsKey(type))
            {
                _ways[type] = windowViewModel;
            }
            else
            {
                _ways.Add(type, windowViewModel);
            }
        }

        private void SetWindowOpeningVM(IWindowOpeningVM<Type>[] viewModels)
        {
            for (int i = 0; i < viewModels.Length; i++)
            {
                viewModels[i].OpenWindowViewModel = Navigate;
            }
        }
    }
}
