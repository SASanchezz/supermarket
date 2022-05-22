using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace supermarket.Navigation.ViewsNavigation
{
    internal abstract class ViewModelNavigator : IViewModelNavigator, INotifyPropertyChanged
    {
        private List<INavigatableViewModel> _viewModels = new();
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

            INavigatableViewModel viewModel = GetViewModel(type);

            if (viewModel == null)
                return;

            _viewModels.Add(viewModel);
            CurrentViewModel = viewModel;
        }

        private INavigatableViewModel GetViewModel(ViewTypes type)
        {
            INavigatableViewModel viewModel = _viewModels.FirstOrDefault(viewModel => viewModel.ViewType.Equals(type));

            if (viewModel != null)
                return viewModel;

            return CreateViewModel(type);
        }

        protected abstract INavigatableViewModel CreateViewModel(ViewTypes type);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
