using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using System.Windows;

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

        }
        
        public Action<ManagerCategories> OpenWindowViewModel { get; set; }
        
        public Action Close { get; set; }

        public List<string[]> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public string[] SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> AddCategoryCommand 
        {
            get
            {
                return _openAddCategoryWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.AddCategory));
            }
        }
        
        public RelayCommand<object> EditCategoryCommand
        {
            get
            {
                return _openEditCategoryWindowCommand ??= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.EditCategory));
            }
        }
        
        public void UpdateCategories()
        {
            Categories = DbQueries.GetAllCategories();
        }
    }
}
