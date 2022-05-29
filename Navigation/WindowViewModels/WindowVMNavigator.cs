using System;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.Navigation.WindowViewModels
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

        public void SetWay(Type type, Window window)
        {
            if (_ways.ContainsKey(type))
            {
                throw new Exception("This way is already set");
            }

            _ways.Add(type, () => window.Show());
        }

        public void SetWay(Type type, Window window, Action handler)
        {
            if (_ways.ContainsKey(type))
            {
                throw new Exception("This way is already set");
            }
            else
            {
                Action action = handler + window.Show;
                _ways.Add(type, action);
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
