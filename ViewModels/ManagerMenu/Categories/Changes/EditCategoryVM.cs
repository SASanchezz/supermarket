using System;
using System.Windows;
using supermarket.Middlewares.Category;
using supermarket.Middlewares.Employee;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Cat = supermarket.Models.Category;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class EditCategoryVM : ViewModel
    {
        private string _changedChangedId;
        private string _name;

        private string _initId;
        
        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _closeCommand;

        public string ChangedId
        {
            get => _changedChangedId;
            set
            {
                _changedChangedId = value;
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

        public Action Close { get; set; }
        
        public RelayCommand<object> UpdateCommand
        {
            get
            {
                return _updateCommand ??= new RelayCommand<object>(_ => UpdateCategory(), CanExecute);
            }
        }
        
        public RelayCommand<object> DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand<object>(_ => DeleteCategory(), CanExecute);
            }
        }

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ??= new RelayCommand<object>(_ => Close());
            }
        }
        public void SetData(string[] data)
        {
            _initId = data[Cat.number];
            ChangedId = _initId;
            Name = data[Cat.name];
        }
        
        private void UpdateCategory()
        {
            string result = CategoryValidator.Validate(ChangedId, Name);
            
            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }
            
            Cat.UpdateCategory(_initId, ChangedId, Name);
            
            Close();
        }
        
        private void DeleteCategory()
        {
            Cat.DeleteCategoryByID(_initId);
            Close();
        }
        
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedId)
                   && !string.IsNullOrWhiteSpace(Name);
        }
    }
}