using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation.VM
{
    internal abstract class VMNavigator : IVMNavigator, INotifyPropertyChanged
    {
        private INavigatableVM _currentViewModel;

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

        protected abstract INavigatableVM CreateViewModel(VMNavigationTypes type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
