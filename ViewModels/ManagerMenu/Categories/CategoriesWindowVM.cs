﻿using System;
using System.Windows;
using supermarket.Navigation.WindowViewModels;
using supermarket.ViewModels.BaseClasses;
using supermarket.ViewModels.ManagerMenu.Categories.Changes;
using supermarket.Windows.ManagerMenu.Categories;

namespace supermarket.ViewModels.ManagerMenu.Categories
{
    internal class CategoriesWindowVM : WindowViewModel<CategoriesWindow, CategoriesVM>
    {
        private AddCategoryWindowVM _addCategoryWindowVM;
        private EditCategoryWindowVM _editCategoryWindowVM;
        
        public CategoriesWindowVM()
        {
            _addCategoryWindowVM = new AddCategoryWindowVM();
            _editCategoryWindowVM = new EditCategoryWindowVM();
            
            SetUpdatingSystem();
            SetWindowsNavigation();
            
            Window.Closing += (sender, e) =>
            {
                _addCategoryWindowVM.Window.Close();
                _editCategoryWindowVM.Window.Close();
            };
        }
        
        private void SetWindowsNavigation()
        {
            var windowsNavigator = 
                new WindowVMNavigator<ManagerCategories>(new IWindowOpeningVM<ManagerCategories>[] { ViewModel });
            
            windowsNavigator.SetWay(ManagerCategories.AddCategory, _addCategoryWindowVM.Window);
            windowsNavigator.SetWay(ManagerCategories.EditCategory, _editCategoryWindowVM.Window, 
                OnOpeningEditCategory);
            
            SetChangingEnabilityByOpeningAnotherWindow(_addCategoryWindowVM);
            SetChangingEnabilityByOpeningAnotherWindow(_editCategoryWindowVM);
        }

        private void OnOpeningEditCategory()
        {
            if (ViewModel.SelectedCategory == null)
            {
                throw new Exception("No selected item");
            }

            _editCategoryWindowVM.ViewModel.SetData(ViewModel.SelectedCategory);
        }

        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                // window is hiden
                if (!(bool)e.NewValue) return;
            
                // window is shown
                ViewModel.UpdateCategories();
            };
            
            Window.IsEnabledChanged += (sender, e) =>
            { 
                // window is enabled
                if ((bool)e.NewValue)
                {
                    ViewModel.UpdateCategories();
                }
            };
        }

        private void SetChangingEnabilityByOpeningAnotherWindow<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                IsEnabled = !IsEnabled;
            };
        }
    }
}
