using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using supermarket.Printing;
using Cat = supermarket.Models.Category;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesVM : ViewModel, IWindowOpeningVM<ManagerCategories>
    {
        private string _filteredSum = "";
        public CategoriesVM()
        {
            OpenAddCategoryWindowCommand =
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.AddCategory));

            OpenEditCategoryWindowCommand =
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.EditCategory));

            PrintCategoriesCommand = new RelayCommand<object>(_ => PrintCategories());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public Action<ManagerCategories> OpenWindowViewModel { get; set; }

        public List<string[]> Categories {
            get
            {
                if (string.IsNullOrEmpty(_filteredSum)) { return Cat.GetAllCategories(); }
                else { return Cat.GetAllCategories(_filteredSum); }
            }
            set 
            {
                if (string.IsNullOrEmpty(_filteredSum)) { Categories = Cat.GetAllCategories(); }
                else { Categories = Cat.GetAllCategories(_filteredSum); }
            }
        }

        public string[] SelectedCategory { get; set; }

        public RelayCommand<object> OpenAddCategoryWindowCommand { get; }

        public RelayCommand<object> OpenEditCategoryWindowCommand { get; }
        
        public RelayCommand<object> PrintCategoriesCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        public string FilteredSum
        {
            get => _filteredSum;
            set
            {
                if (double.Parse(value) < 0) { 
                    _filteredSum = ""; 
                    MessageBox.Show("Введіть натуральне число"); 
                }
                else { 
                    _filteredSum = value; 
                }

                OnPropertyChanged();
                UpdateCategories();
            }
        }

        public void UpdateCategories()
        {
            OnPropertyChanged(nameof(Categories));
        }

        private void PrintCategories()
        {
            var printerCategories = new List<string[]>();

            for (int i = 0; i < Categories.Count; ++i)
            {
                printerCategories.Add(new string[Categories[0].Length]);

                for (int h = 0; h < Categories[0].Length; ++h)
                {
                    printerCategories[i].SetValue(Categories[i][h], h);
                }
            }

            new Printer().PrintDataGrid(Categories, "Категорії станом на " + DateTime.UtcNow.ToString("dd.MM.yyyy"), new string[]
            {
                "Номер",
                "Назва"
            });
        }
    }
}
