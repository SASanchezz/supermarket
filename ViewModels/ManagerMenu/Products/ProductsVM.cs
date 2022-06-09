﻿using supermarket.Navigation.WindowViewModels;
using supermarket.Printing;
using supermarket.Utils;
using Categ = supermarket.Models.Category;
using Prod = supermarket.Models.Product;
using supermarket.ViewModels.BaseClasses;
using System;
using System.Collections.Generic;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Products
{
    internal class ProductsVM : ViewModel, IWindowOpeningVM<ManagerProducts>
    {
        private const string AllString = "Всі";

        private List<string[]> _products;

        private string _filteredName = "";
        private string _selectedCategory = AllString;
        private string[] _selectiveCategories;

        public ProductsVM()
        {
            OpenAddProductWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerProducts.AddProduct));
            OpenEditProductWindowCommand = 
                new RelayCommand<object>(_ => OpenWindowViewModel(ManagerProducts.EditProduct));
            PrintProductsCommand = new RelayCommand<object>(_ => PrintProducts());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public Action<ManagerProducts> OpenWindowViewModel { get; set; }
        
        public List<string[]> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public string[] SelectedProduct { get; set; }

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
            get => _selectiveCategories;
            set
            {
                _selectiveCategories = value;
                OnPropertyChanged();
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

        public RelayCommand<object> OpenAddProductWindowCommand { get; }

        public RelayCommand<object> OpenEditProductWindowCommand { get; }

        public RelayCommand<object> PrintProductsCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

        public void UpdateProducts()
        {
            Products = Prod.GetAllProducts(_selectedCategory, _filteredName);
        }

        public void Reset()
        {
            SelectedCategory = null;
            FilteredName = null;
            SelectiveCategories = GetSelectiveCategories();
        }

        private string[] GetSelectiveCategories() 
        {
            string[] categories = new string[Categ.GetAllCategories().Count + 1];
            categories.SetValue(AllString, 0);
            
            for (int i = 0; i < Categ.GetAllCategories().Count; ++i)
            {
                categories[i + 1] = Categ.GetAllCategoriesNames()[i];
            }

            MessageBox.Show("Get Categotires");
            return categories;
        }

        private void PrintProducts()
        {
            var printerProducts = new List<string[]>();

            for (int i = 0; i < Products.Count; ++i)
            {
                printerProducts.Add(new string[Products[0].Length]);

                for (int h = 0; ; ++h)
                { 
                    if (h == 2)
                    {
                        printerProducts[i].SetValue(Products[i][5], h);
                        break;
                    }
                    printerProducts[i].SetValue(Products[i][h], h);
                }

                for (int h = 2; h < Products[0].Length - 1; ++h)
                {
                    printerProducts[i].SetValue(Products[i][h], h + 1);
                }
            }

            Printer.PrintDataGrid(printerProducts, new string[]
            {
                "id",
                "Номер категорії",
                "Категорія",
                "Назва",
                "Опис",
                "Виробник"
            });
        }
    }
}

