using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace supermarket.ViewModels
{
    internal class WindowViewModel<WindowType, ViewModelType> : INotifyPropertyChanged
        where WindowType : Window, new() 
        where ViewModelType : ViewModel, new()
    {
        private bool _isEnabled = true;
        protected WindowViewModel()
        {
            Window = new WindowType();
            ViewModel = new ViewModelType();

            Window.DataContext = this;
            Window.Closing += (sender, e) =>
            {
                e.Cancel = true;
                Window.Hide();
            };
        }

        public WindowType Window { get; protected set; }

        public ViewModelType ViewModel { get; protected set; }

        public bool IsEnabled
        {
            get => _isEnabled;
            protected set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}