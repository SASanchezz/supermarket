using System;
using supermarket.Utils;
using supermarket.Middlewares.Customer;
using supermarket.Models;
using System.Windows;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    internal class AddCustomerVM : ViewModel
    {
        private string _cardNumber;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phoneNumber;
        private string _city;
        private string _street;
        private string _zipcode;
        private string _percent;

        public AddCustomerVM()
        {
            AddCustomerCommand = new RelayCommand<object>(_ => AddCustomer(), CanExecute);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                _cardNumber = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged();
            }
        }
        
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                OnPropertyChanged();
            }
        }

        public string Zipcode
        {
            get => _zipcode;
            set
            {
                _zipcode = value;
                OnPropertyChanged();
            }
        }

        public string Percent
        {
            get => _percent;
            set
            {
                _percent = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> AddCustomerCommand { get; }

        public RelayCommand<object> CloseCommand { get; }

        private void AddCustomer()
        {
            try
            {
                //Validates entered information
                CustomerValidator.ValidateInsert(CardNumber, Surname, Name, Patronymic,
                    PhoneNumber, City, Street, Zipcode, Percent);
                
                //Query to insert new customer
                Customer.AddCustomer(CardNumber, Surname, Name, Patronymic,
                    PhoneNumber, City, Street, Zipcode, Percent);

                ResetFields();
                CloseWindow();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        private void ResetFields()
        {
            CardNumber = "";
            Surname = "";
            Name = "";
            Patronymic = "";
            PhoneNumber = "";
            City = "";
            Street = "";
            Zipcode = "";
            Percent = "";
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(CardNumber)
                && !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(PhoneNumber)
                && !string.IsNullOrWhiteSpace(Percent);
        }
    }
}
