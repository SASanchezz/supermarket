using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public ViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}