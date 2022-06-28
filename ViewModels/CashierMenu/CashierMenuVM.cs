using supermarket.Navigation.ViewModels;
using supermarket.Navigation.WindowViewModels;
using System;
using System.Reflection.Metadata;
using System.Windows;
using Microsoft.VisualBasic;
using supermarket.Utils;
using supermarket.Models;
using Constants = supermarket.Models.Constants;

namespace supermarket.ViewModels.CashierMenu
{
    /*
     * Controls Cashier Menu View
     */
    internal class CashierMenuVM : NavigatableViewModel<MainViewsTypes>, IWindowOpeningVM<Main>
    {
        public CashierMenuVM()
        {
            OpenInfoWindowCommand = new RelayCommand<object>(_ => OpenInfoWindow());
            //
            OpenProductsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.CashierProducts));
            //
            OpenStoreProductsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.CashierStoreProducts));
            //
            OpenReceiptsWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.CashierChecks));
            //
            OpenCustomersWindowCommand = new RelayCommand<object>(_ => OpenWindowViewModel(Main.CashierCustomers));
            //
            GoToSignInCommand = new RelayCommand<object>(_ => ChangeViewModel(MainViewsTypes.SignIn));
        }

        public string[] Employee { get; set; }

        public string EmployeeFullName { get { return Employee[1] + " " + Employee[2]; } }

        public Action<Main> OpenWindowViewModel { get; set; }
        
        public RelayCommand<object> OpenInfoWindowCommand { get; }
        public RelayCommand<object> OpenProductsWindowCommand { get; }
        public RelayCommand<object> OpenStoreProductsWindowCommand { get; }
        public RelayCommand<object> OpenCustomersWindowCommand { get; }
        public RelayCommand<object> OpenReceiptsWindowCommand { get; }
        public RelayCommand<object> GoToSignInCommand { get; }
        
        private void OpenInfoWindow()
        {
            MessageBox.Show("Id:\t\t" + Employee[0] + "\n"
                            + "Прізвище:\t" + Employee[1] + "\n"
                            + "Ім'я:\t\t" + Employee[2] + "\n"
                            + "По батькові:\t" + Employee[3] + "\n" 
                            + "Зарплата:\t\t" + Employee[5] + "\n" 
                            + "Дата народження:\t" 
                            + DateTime.Parse(Employee[6]).ToString(Constants.DateStringFormat) + "\n" 
                            + "Дата початку роботи: " 
                            + DateTime.Parse(Employee[7]).ToString(Constants.DateStringFormat) + "\n" 
                            + "Номер телефону:\t" + Employee[8] + "\n"
                            + "Адреса:\t\t" + Employee[10] + ", " + Employee[11] + "\n" 
                            + "Поштовий індекс:\t" + Employee[12]);
        }
    }
}
