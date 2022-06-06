using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace supermarket.ViewModels.BaseClasses
{
    internal abstract class ViewModel : INotifyPropertyChanged
    {
        public Action CloseWindow { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}