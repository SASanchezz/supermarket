using supermarket.Middlewares.SignIn;
using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Navigation.ViewModels;

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
        
        public string Login { get; set; }

        public string Password { get; set; }

        public RelayCommand<object> SignInCommand { get; }

        private void SignIn()
        {
            try
            {
                //SignInValidator.Validate(Login, Password);
                //SignInValidator.Validate("+380634412925", "admin");

                // вот тут хуйню с проверкой менеджер это или кассир надо сообразить
                if (Login == "к") ChangeViewModel(MainViewsTypes.CashierMenu);
                else ChangeViewModel(MainViewsTypes.ManagerMenu);
                
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
