using System.Windows;
using supermarket.Middlewares.Category;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Cat = supermarket.Models.Category;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class AddCategoryVM : ViewModel
    {
        private string _number;
        private string _name;
        
        public AddCategoryVM()
        {
            AddCategoryCommand = new RelayCommand<object>(_ => AddCategory(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public string Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> AddCategoryCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        private void ResetFields()
        {
            Number = "";
            Name = "";
        }
        
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
            Cat.AddCategory(Number, Name);
            
            ResetFields();
            CloseWindow();
        }
        
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Number)
                   && !string.IsNullOrWhiteSpace(Name);
        }
    }
}