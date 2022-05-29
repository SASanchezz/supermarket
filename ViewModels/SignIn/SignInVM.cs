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
    internal class SignInVM : NavigatableViewModel
    {
        private RelayCommand<object> _signInCommand;

        public string Login { get; set; }

        public string Password { get; set; }

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ??= new RelayCommand<object>(_ => SignIn(), CanExecute);
            }
        }

        private void SignIn()
        {
            try
            {
                //SignInValidator.Validate(Login, Password);
                SignInValidator.Validate("+380634412925", "admin");

                // вот тут хуйню с проверкой менеджер это или кассир надо сообразить
                ChangeViewModel(VMNavigationTypes.ManagerMenu);
                //_goToCashierMenu();
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
