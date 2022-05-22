using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation.WindowsNavigation
{
    internal abstract class WindowViewModelNavigator : IWindowViewModelNavigator, INotifyPropertyChanged
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

        protected void SetWindowOpeningViewModel(IWindowOpeningViewModel[] viewModels)
        {
            for (int i = 0; i < viewModels.Length; i++)
            {
                viewModels[i].OpenWindowViewModel = Navigate;
            }
        }

        public void Navigate(WindowTypes type)
        {
            IsEnabled = false;
            CreateWindowViewModel(type);
        }

        protected abstract INavigatableWindowViewModel CreateWindowViewModel(WindowTypes type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
