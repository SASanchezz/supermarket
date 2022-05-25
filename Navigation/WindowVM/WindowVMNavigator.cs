using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation.WindowVM
{
    internal abstract class WindowVMNavigator<Type> : IWindowVMNavigator<Type>, INotifyPropertyChanged where Type : Enum
    {
        private bool _isEnabled;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        protected void SetWindowOpeningVM(IWindowOpeningVM<Type>[] viewModels)
        {
            for (int i = 0; i < viewModels.Length; i++)
            {
                viewModels[i].OpenWindowViewModel = Navigate;
            }
        }

        public void Navigate(Type type)
        {
            IsEnabled = false;
            CreateWindowViewModel(type);
        }

        protected abstract INavigatableWindowVM<Type> CreateWindowViewModel(Type type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
