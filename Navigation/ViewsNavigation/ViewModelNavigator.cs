using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation.ViewsNavigation
{
    internal abstract class ViewModelNavigator : IViewModelNavigator, INotifyPropertyChanged
    {
        private INavigatableViewModel _currentViewModel;

        public INavigatableViewModel CurrentViewModel
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

        public void Navigate(ViewTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewType.Equals(type))
                return;
            
            CurrentViewModel = CreateViewModel(type);
        }

        protected abstract INavigatableViewModel CreateViewModel(ViewTypes type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
