using supermarket.Navigation.WindowsNavigation;
using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.ViewModels.ManagerMenu
{
    class ManagerCategoriesViewModel : IWindowOpeningViewModel, INotifyPropertyChanged
    {

        private List<string[]> _categories;
        private string[] _selectedCategory;

        private RelayCommand<object> _openAddCategoryWindowCommand;
        private RelayCommand<object> _openEditCategoryWindowCommand;
        private RelayCommand<object> _closeCommand;

        public ManagerCategoriesViewModel()
        {
            UpdateCustomers();
        }

        public Action<WindowTypes> OpenWindowViewModel { get; set; }
        public List<string[]> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public string[] SelectedCustomer
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public void UpdateCustomers()
        {
            Categories = DbQueries.GetAllCategories();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
