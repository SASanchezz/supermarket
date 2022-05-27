using System;
using System.Collections.Generic;

namespace supermarket.Navigation.WindowVM
{
    internal class WindowVMNavigator<Type> where Type : Enum 
    {
        private Dictionary<Type, Action> _ways;

        public WindowVMNavigator(IWindowOpeningVM<Type>[] viewModels)
        {
            SetWindowOpeningVM(viewModels);
            _ways = new Dictionary<Type, Action>();
        }

        public void Navigate(Type type)
        {
            if (!_ways.ContainsKey(type))
            {
                throw new Exception("There is no way");
            }
            _ways[type]();
        }

        public void SetWay(Type type, Action goToWindowViewModel)
        {
            if (_ways.ContainsKey(type))
            {
                _ways[type] = goToWindowViewModel;
            }
            else
            {
                _ways.Add(type, goToWindowViewModel);
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
