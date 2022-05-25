using supermarket.Middlewares.SignIn;
using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Navigation.VM;

namespace supermarket.ViewModels.SignIn
{
    /*
     * Controls Sign In View
     */
    internal class SignInVM : INavigatableVM
    {
        private RelayCommand<object> _signInCommand;

        private Action _goToManageMenu;
        private Action _goToCashierMenu;

        public SignInVM(Action goToManageMenu, Action goToCashierMenu)
        {
            _goToManageMenu = goToManageMenu;
            _goToCashierMenu = goToCashierMenu;
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ??= new RelayCommand<object>(_ => SignIn(), CanExecute);
            }
        }

        public VMNavigationTypes ViewType => VMNavigationTypes.SignIn;

        private void SignIn()
        {
            //string result = SignInValidator.Validate(Login, Password);
            string result = SignInValidator.Validate("+380634412925", "admin");

            if (result != "")
            {
                MessageBox.Show(result);
                return;
            }
            // вот тут хуйню с проверкой менеджер это или кассир надо сообразить
            _goToManageMenu?.Invoke();
            //_goToCashierMenu?.Invoke();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Login)
               && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
