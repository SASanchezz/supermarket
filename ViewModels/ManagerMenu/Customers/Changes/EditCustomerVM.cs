﻿using supermarket.Utils;
using System;
using supermarket.Middlewares.Customer;
using Cust = supermarket.Models.Customer;
using System.Windows;
using supermarket.ViewModels.BaseClasses;

namespace supermarket.ViewModels.ManagerMenu.Customers.Changes
{
    /*
     * Controls ManagerEditEmployee View
     */
    internal class EditCustomerVM : ViewModel
    {
        private string _init_card_number;
        private string _changed_card_number;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phone_number;
        private string _city;
        private string _street;
        private string _zipcode;
        private string _percent;

        private RelayCommand<object> _updateCommand;
        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _closeCommand;


        public string InitCardNumber
        {
            get => _init_card_number;
            set
            {
                _init_card_number = value;
                OnPropertyChanged();
            }
        }
        public string ChangedCardNumber
        {
            get => _changed_card_number;
            set
            {
                _changed_card_number = value;
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

        public string Phone_number
        {
            get => _phone_number;
            set
            {
                _phone_number = value;
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


        public RelayCommand<object> UpdateCommand
        {
            get => _updateCommand ??= new RelayCommand<object>(_ => UpdateCustomer(), CanExecute);
        }
        public RelayCommand<object> DeleteCommand
        {
            get => _deleteCommand ??= new RelayCommand<object>(_ => DeleteCustomer());
        }
        public RelayCommand<object> CloseCommand
        {
            get => _closeCommand ??= new RelayCommand<object>(_ => Close());
        }
        public Action Close { get; set; }

        public void SetData(string[] data)
        {
            InitCardNumber = data[Cust.card_number];
            ChangedCardNumber = data[Cust.card_number];
            Name = data[Cust.name];
            Surname = data[Cust.surname];
            Patronymic = data[Cust.patronymic];
            Phone_number = data[Cust.phone_number];
            City = data[Cust.city];
            Street = data[Cust.street];
            Zipcode = data[Cust.zipcode];
            Percent = data[Cust.percent];
        }

        private void UpdateCustomer()
        {
            string result = CustomerValidator.ValidateUpdate(InitCardNumber, ChangedCardNumber, Surname, Name, Patronymic,
                Phone_number, City, Street, Zipcode, Percent);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }


            Cust.UpdateCustomer(InitCardNumber, ChangedCardNumber, Surname, Name, Patronymic,
                Phone_number, City, Street, Zipcode, Percent);

            Close();
        }

        private void DeleteCustomer()
        {
            Cust.DeleteCustomerByCardNumber(InitCardNumber);
            Close();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedCardNumber)
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
