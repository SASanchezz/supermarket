using supermarket.Utils;
using System;
using supermarket.Middlewares.Customer;
using supermarket.Models;
using System.Windows;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    internal class AddCustomerVM : ViewModel
    {
        private RelayCommand<object> _addCustomerCommand;
        private RelayCommand<object> _closeCommand;

        public Action Close { get; set; }

        public string CardNumber { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Zipcode { get; set; }

        public string Percent { get; set; }

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
            string result = CustomerValidator.ValidateInsert(CardNumber, Surname, Name, Patronymic,
                PhoneNumber, City, Street, Zipcode, Percent);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Customer.AddCustomer(CardNumber, Surname, Name, Patronymic,
                PhoneNumber, City, Street, Zipcode, Percent);

            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(CardNumber)
                && !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(PhoneNumber)
                && !string.IsNullOrWhiteSpace(City)
                && !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(Zipcode)
                && !string.IsNullOrWhiteSpace(Percent);
        }
    }
}
