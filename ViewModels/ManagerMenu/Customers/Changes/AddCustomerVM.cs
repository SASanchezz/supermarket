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
        public AddCustomerVM()
        {
            AddCustomerCommand = new RelayCommand<object>(_ => AddCustomer(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string CardNumber { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Zipcode { get; set; }

        public string Percent { get; set; }

        public RelayCommand<object> AddCustomerCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

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

            CloseWindow();
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
