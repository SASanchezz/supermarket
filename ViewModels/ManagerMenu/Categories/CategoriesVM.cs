using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
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
            UpdateCategories();
        }

        public Action<ManagerCategories> OpenWindowViewModel { get; set; }

        public List<string[]> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public string[] SelectedCustomer
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public void UpdateCategories()
        {
            Categories = DbQueries.GetAllCategories();
        }
    }
}
