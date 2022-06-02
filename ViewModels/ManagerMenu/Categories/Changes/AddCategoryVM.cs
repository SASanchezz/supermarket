using System;
using supermarket.Middlewares.Category;
using supermarket.Models;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class AddCategoryVM : ViewModel
    {
        private RelayCommand<object> _addCategoryCommand;
        private RelayCommand<object> _closeCommand;
        
        public string Id { get; set; }

        public string Name { get; set; }
        
        public Action Close { get; set; }
        
        public RelayCommand<object> AddCategoryCommand
        {
            get
            {
                return _addCategoryCommand ??= new RelayCommand<object>(_ => AddCategory(), CanExecute);
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        
        private void AddCategory()
        {
            ////Validates entered information
            string result = CategoryValidator.Validate(Id, Name);

            if (result.Length != 0)
            {
                //MessageBox.Show(result);
                return;
            }

            //Query to insert new category
            Category.AddCategory(Id, Name);
            
            // добавить обнуление свойств
            
            Close();
        }
        
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Id)
                   && !string.IsNullOrWhiteSpace(Name);
        }
    }
}