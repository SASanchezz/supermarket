using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace supermarket.ViewModels.BaseClasses
{
    internal abstract class WindowViewModel<TWindow, TViewModel> : INotifyPropertyChanged
        where TWindow : Window, new()
        where TViewModel : ViewModel, new()
    {
        private bool _isEnabled = true;
        protected WindowViewModel()
        {
            Window = new TWindow
            {
                DataContext = this
            };

            Window.Closing += (sender, e) =>
            {
                e.Cancel = true;
                Window.Hide();
            };
            
            ViewModel = new TViewModel
            {
                CloseWindow = Window.Close
            };
        }

        public TWindow Window { get; protected set; }

        public TViewModel ViewModel { get; protected set; }

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
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal abstract class WindowViewModel<TWindow> : INotifyPropertyChanged
        where TWindow : Window, new()
    {
        private bool _isEnabled = true;

        protected WindowViewModel(TWindow window)
        {
            Window = window;
            Window.DataContext = this;
            Window.Closing += (sender, e) =>
            {
                e.Cancel = true;
                Window.Hide();
            };
        }
        
        protected WindowViewModel()
        {
            Window = new TWindow
            {
                DataContext = this
            };
            
            Window.Closing += (sender, e) =>
            {
                e.Cancel = true;
                Window.Hide();
            };
        }

        public TWindow Window { get; protected set; }

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
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}