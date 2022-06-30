using supermarket.Navigation.WindowViewModels;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using supermarket.Printing;
using Cat = supermarket.Models.Category;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesVM : ViewModel, IWindowOpeningVM<ManagerCategories>
    {
        public CategoriesVM()
        {
            OpenAddCategoryWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.AddCategory));
            
            OpenEditCategoryWindowCommand= 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerCategories.EditCategory));
            
            PrintCategoriesCommand = new RelayCommand<object>(_ => PrintCategories());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public Action<ManagerCategories> OpenWindowViewModel { get; set; }

        public List<string[]> Categories => Cat.GetAllCategories();

        public string[] SelectedCategory { get; set; }

        public RelayCommand<object> OpenAddCategoryWindowCommand { get; }

        public RelayCommand<object> OpenEditCategoryWindowCommand { get; }
        
        public RelayCommand<object> PrintCategoriesCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

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
