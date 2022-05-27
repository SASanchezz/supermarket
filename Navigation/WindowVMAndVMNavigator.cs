using supermarket.Navigation.VM;
using supermarket.Navigation.WindowVM;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation
{
    internal abstract class WindowVMAndVMNavigator<Type> : IVMNavigator, IWindowVMNavigator<Type>, INotifyPropertyChanged where Type : Enum
    {
        private bool _isEnabled;
        private INavigatableVM _currentViewModel;

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

        public INavigatableVM CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            private set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public void Navigate(VMNavigationTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewType.Equals(type))
                return;

            CurrentViewModel = CreateViewModel(type);
        }

        public void Navigate(Type type)
        {
            IsEnabled = false;
            CreateWindowViewModel(type);
        }

        protected void SetWindowOpeningVM(IWindowOpeningVM<Type>[] viewModels)
        {
            for (int i = 0; i < viewModels.Length; i++)
            {
                viewModels[i].OpenWindowViewModel = Navigate;
            }
        }

        protected abstract void CreateWindowViewModel(Type type);

        protected abstract INavigatableVM CreateViewModel(VMNavigationTypes type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
