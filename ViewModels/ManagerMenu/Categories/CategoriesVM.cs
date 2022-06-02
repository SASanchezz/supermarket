﻿using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using supermarket.Printing;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesVM : ViewModel, IWindowOpeningVM<ManagerCategories>
    {
        private List<string[]> _categories;
        private string[] _selectedCategory;

        public CategoriesVM()
        {
            OpenAddCategoryWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.AddCategory));
            OpenEditCategoryWindowCommand= new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.EditCategory));
            PrintCategoriesCommand = new RelayCommand<object>(_ => PrintCategories());
            CloseCommand = new RelayCommand<object>(_ => Close());
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

        public RelayCommand<object> OpenAddCategoryWindowCommand { get; }

        public RelayCommand<object> OpenEditCategoryWindowCommand { get; }
        
        public RelayCommand<object> PrintCategoriesCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public void UpdateCategories()
        {
            Categories = DbQueries.GetAllCategories();
        }
        
        private void PrintCategories()
        {
            Printer.PrintDataGrid(Categories, new string[]
            {
                "Номер",
                "Назва"
            });
        }
    }
}
