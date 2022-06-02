using System;
using System.Windows;
using supermarket.Middlewares.Category;
using supermarket.Models;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class AddCategoryVM : ViewModel
    {
        public AddCategoryVM()
        {
            AddCategoryCommand = new RelayCommand<object>(_ => AddCategory(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => Close());
        }
        
        public string Number { get; set; }

        public string Name { get; set; }
        
        public Action Close { get; set; }
        
        public RelayCommand<object> AddCategoryCommand { get; }

        public RelayCommand<object> CloseCommand { get; }
        
        private void AddCategory()
        {
            ////Validates entered information
            string result = CategoryValidator.ValidateInsert(Number, Name);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new category
            Category.AddCategory(Number, Name);
            
            // добавить обнуление свойств
            
            Close();
        }
        
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Number)
                   && !string.IsNullOrWhiteSpace(Name);
        }
    }
}