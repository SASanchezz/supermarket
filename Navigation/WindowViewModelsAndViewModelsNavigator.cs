using supermarket.Navigation.ViewsNavigation;
using supermarket.Navigation.WindowsNavigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation
{
    internal abstract class WindowViewModelsAndViewModelsNavigator : IViewModelNavigator, IWindowViewModelNavigator, INotifyPropertyChanged
    {
        private INavigatableViewModel _currentViewModel;

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

        protected void SetWindowOpeningViewModel(IWindowOpeningViewModel[] viewModels)
        {
            for (int i = 0; i < viewModels.Length; i++)
            {
                viewModels[i].OpenWindowViewModel = Navigate;
            }
        }

        public void Navigate(ViewTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewType.Equals(type))
                return;

            CurrentViewModel = CreateViewModel(type);
        }

        public void Navigate(WindowTypes type)
        {
            IsEnabled = false;
            CreateWindowViewModel(type);
        }
        protected abstract INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type);
        protected abstract INavigatableViewModel CreateViewModel(ViewTypes type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
