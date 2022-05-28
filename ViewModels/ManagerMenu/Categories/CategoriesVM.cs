using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using System;
using System.Collections.Generic;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesVM : ViewModel, IWindowOpeningVM<ManagerCategories>
    {
        private List<string[]> _categories;
        private string[] _selectedCategory;

        private RelayCommand<object> _openAddCategoryWindowCommand;
        private RelayCommand<object> _openEditCategoryWindowCommand;
        private RelayCommand<object> _closeCommand;

        public CategoriesVM()
        {
            UpdateCustomers();
        }

        public Action<ManagerCategories> OpenWindowViewModel { get; set; }

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
    }
}
