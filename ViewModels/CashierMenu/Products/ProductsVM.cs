using System.Collections.Generic;
using supermarket.Utils;
using supermarket.ViewModels.BaseClasses;
using Categ = supermarket.Models.Category;
using Prod = supermarket.Models.Product;

namespace supermarket.ViewModels.CashierMenu.Products
{
    internal class ProductsVM : ViewModel
    {
        private const string AllString = "Всі";
        private string _filteredName = "";
        private string _selectedCategory = AllString;

        public ProductsVM()
        {
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public List<string[]> Products => Prod.GetAllProducts(_selectedCategory, _filteredName);

        public string FilteredName
        {
            get => _filteredName;
            set
            {
                _filteredName = value;

                UpdateProducts();
                OnPropertyChanged();
            }
        }

        public string[] SelectiveCategories
        {
            get
            {
                string[] categories = new string[Categ.GetAllCategories().Count + 1];
                categories.SetValue(AllString, 0);
            
                for (int i = 0; i < Categ.GetAllCategories().Count; ++i)
                {
                    categories[i + 1] = Categ.GetAllCategoriesNames()[i];
                }
            
                return categories;
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value ?? AllString;
                OnPropertyChanged();
                UpdateProducts();
            }
        }

        public RelayCommand<object> CloseCommand { get; }

        public void UpdateProducts()
        {
            OnPropertyChanged(nameof(Products));
        }

        public void UpdateSelectiveCategories()
        {
            OnPropertyChanged(nameof(SelectiveCategories));
        }

    }
}