using supermarket.Middlewares.SignIn;
using supermarket.Utils;
using supermarket.Windows.ManagerMenu;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace supermarket.ViewModels
{
    public class MainWindowVM : ViewModel
    {
        private string _login;
        private string _password;
        private RelayCommand<object> _signInCommand;

        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }
        public RelayCommand<object> SignInCommand
        {
            get
            {
                return _signInCommand ??= new RelayCommand<object>(_ => SignIn(), CanExecute);
            }
        }

        private void SignIn()
        {
            //OnPropertyChanged(Login);
            //OnPropertyChanged(Password);
            //string result = SignInValidator.Validate(Login, Password);

            string result = SignInValidator.Validate("+380634412925", "admin");
            if (result != "")
            {
                MessageBox.Show(result);
                return;
            }

            ManagerMainWindow window = new();
            window.Show();
            OnClosingRequest();
        }

        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(Login)
               && !String.IsNullOrWhiteSpace(Password);
        }  
    }
}