using supermarket.Utils;
using System;
using supermarket.Middlewares.Customer;
using supermarket.Models;
using System.Windows;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    internal class AddCustomerVM : ViewModel
    {
        private string _card_number;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phone_number;
        private string _city;
        private string _street;
        private string _zipcode;
        private string _percent;

        private RelayCommand<object> _addCustomerCommand;
        private RelayCommand<object> _closeCommand;

        public Action Close { get; set; }

        public string CardNumber { get => _card_number; set => _card_number = value; }

        public string Surname { get => _surname; set => _surname = value; }

        public string Name { get => _name; set => _name = value; }

        public string Patronymic { get => _patronymic; set => _patronymic = value; }

        public string Phone_number { get => _phone_number; set => _phone_number = value; }

        public string City { get => _city; set => _city = value; }

        public string Street { get => _street; set => _street = value; }

        public string Zipcode { get => _zipcode; set => _zipcode = value; }

        public string Percent { get => _percent; set => _percent = value; }


        public RelayCommand<object> AddCustomerCommand
        {
            get => _addCustomerCommand ??= new RelayCommand<object>(_ => AddCustomer(), CanExecute);
        }

        public RelayCommand<object> CloseCommand
        {
            get => _closeCommand ??= new RelayCommand<object>(_ => Close());
        }

        private void AddCustomer()
        {
            /*S
            * This method sign up new customer
            */

            ////Validates entered information
            string result = CustomerValidator.ValidateInsert(_card_number, _surname, _name, _patronymic,
                _phone_number, _city, _street, _zipcode, _percent);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Customer.AddCustomer(_card_number, _surname, _name, _patronymic,
                _phone_number, _city, _street, _zipcode, _percent);

            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(CardNumber)
                && !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(Phone_number)
                && !string.IsNullOrWhiteSpace(City)
                && !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(Zipcode)
                && !string.IsNullOrWhiteSpace(Percent);
        }
    }
}
