using supermarket.Middlewares.SignIn;
using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Navigation.ViewModels;
using Empl = supermarket.Models.Employee;

namespace supermarket.ViewModels.SignIn
{
    /*
     * Controls Sign In View
     */
    internal class SignInVM : NavigatableViewModel<MainViewsTypes>
    {
        public SignInVM()
        {
            SignInCommand = new RelayCommand<object>(_ => SignIn(), CanExecute);
        }
        
        public static string[] Employee { get; private set; }
        
        public string Login { get; set; }

        public string Password { get; set; }

        public RelayCommand<object> SignInCommand { get; }

        private void SignIn()
        {
            try
            {
                SignInValidator.Validate(Login, Password);

                Employee = Empl.GetEmployeeByPhone(Login);

                ChangeViewModel(Employee[Empl.role] == "0" ? MainViewsTypes.ManagerMenu : MainViewsTypes.CashierMenu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Login)
               && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
