using System;
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
            
            Window.Closed += (sender, e) =>
            {
                _addCategoryWindowVM.Window.Close();
                _editCategoryWindowVM.Window.Close();
            };
            
            // set Close() method to Action in ViewModel
            ViewModel.Close = Window.Close;
        }
        
        private void SetWindowsNavigation()
        {
            var windowsNavigator = new WindowVMNavigator<ManagerCategories>(new IWindowOpeningVM<ManagerCategories>[] { ViewModel });
            
            windowsNavigator.SetWay(ManagerCategories.AddCategory, _addCategoryWindowVM.Window);
            windowsNavigator.SetWay(ManagerCategories.EditCategory, _editCategoryWindowVM.Window, OnOpeningEditEmployeeHandler);
            
            SetVisibilitySystem(_addCategoryWindowVM);
            SetVisibilitySystem(_editCategoryWindowVM);
        }

        private void OnOpeningEditEmployeeHandler()
        {
            if (ViewModel.SelectedCategory == null)
            {
                throw new Exception("No selected item");
            }

            _editCategoryWindowVM.ViewModel.SetData(ViewModel.SelectedCategory);
            ViewModel.SelectedCategory = null;
        }
        private void SetUpdatingSystem()
        {
            Window.IsVisibleChanged += (sender, e) =>
            {
                if (!(bool)e.NewValue) return; // window is hiden
                // window is shown
                ViewModel.UpdateCategories();
            };
            SetUpdatingAfterHiden(_addCategoryWindowVM);
            SetUpdatingAfterHiden(_editCategoryWindowVM);
        }
        
        private void SetUpdatingAfterHiden<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM) 
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) =>
            {
                if ((bool)e.NewValue) return; // window is shown
                // window is hiden
                ViewModel.UpdateCategories();
            };
        }

        private void SetVisibilitySystem<TWindow, TViewModel>(WindowViewModel<TWindow, TViewModel> windowVM)
            where TWindow : Window, new()
            where TViewModel : ViewModel, new()
        {
            windowVM.Window.IsVisibleChanged += (sender, e) => IsEnabled = !IsEnabled;
        }
    }
}
