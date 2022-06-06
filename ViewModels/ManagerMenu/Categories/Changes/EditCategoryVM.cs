using System.Windows;
using supermarket.Middlewares.Category;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Cat = supermarket.Models.Category;

namespace supermarket.ViewModels.ManagerMenu.Categories.Changes
{
    internal class EditCategoryVM : ViewModel
    {
        private string _initNumber;
        private string _initName;
        private string _changedNumber;
        private string _changedName;

        public EditCategoryVM()
        {
            UpdateCategoryCommand = new RelayCommand<object>(_ => UpdateCategory(), CanExecute);
            DeleteCategoryCommand = new RelayCommand<object>(_ => DeleteCategory());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public string ChangedNumber
        {
            get => _changedNumber;
            set
            {
                _changedNumber = value;
                OnPropertyChanged();
            }
        }

        public string ChangedName
        {
            get => _changedName;
            set
            {
                _changedName = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> UpdateCategoryCommand { get; }
        
        public RelayCommand<object> DeleteCategoryCommand { get; }

        public RelayCommand<object> CloseCommand { get; }
        
        public void SetData(string[] data)
        {
            _initNumber = data[Cat.number];
            _initName = data[Cat.name];
            ChangedNumber = _initNumber;
            ChangedName = data[Cat.name];
        }
        
        private void UpdateCategory()
        {
            string result = CategoryValidator.ValidateUpdate(_initNumber, _initName,ChangedNumber, ChangedName);
            
            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }
            
            Cat.UpdateCategory(_initNumber, ChangedNumber, ChangedName);
            
            CloseWindow();
        }
        
        private void DeleteCategory()
        {
            Cat.DeleteCategoryByNumber(_initNumber);
            CloseWindow();
        }
        
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedNumber)
                   && !string.IsNullOrWhiteSpace(ChangedName);
        }
    }
}