using System;
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

        private void AddCategory()
        {
            try
<<<<<<< Updated upstream
=======
            {
                CategoryValidator.ValidateInsert(Number, Name);
            }
            catch(Exception e)
            {
                
            }
            ////Validates entered information
            string result = 

            if (result.Length != 0)
>>>>>>> Stashed changes
            {
                //Validates entered information
                CategoryValidator.ValidateInsert(Number, Name);
                
                //Query to insert new category
                Cat.AddCategory(Number, Name);
            
                ResetFields();
                CloseWindow();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        private void ResetFields()
        {
            Number = "";
            Name = "";
        }
        
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Number)
                   && !string.IsNullOrWhiteSpace(Name);
        }
    }
}