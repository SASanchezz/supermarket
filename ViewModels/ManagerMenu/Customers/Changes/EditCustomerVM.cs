using supermarket.Utils;
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
        private string _initCardNumber;
        private string _changedCardNumber;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phoneNumber;
        private string _city;
        private string _street;
        private string _zipcode;
        private string _percent;

        public EditCustomerVM()
        {
            UpdateCustomerCommand = new RelayCommand<object>(_ => UpdateCustomer(), CanExecute);
            DeleteCustomerCommand = new RelayCommand<object>(_ => DeleteCustomer());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public string InitCardNumber
        {
            get => _initCardNumber;
            set
            {
                _initCardNumber = value;
                OnPropertyChanged();
            }
        }
        public string ChangedCardNumber
        {
            get => _changedCardNumber;
            set
            {
                _changedCardNumber = value;
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

        public RelayCommand<object> UpdateCustomerCommand { get; }
        
        public RelayCommand<object> DeleteCustomerCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

        public void SetData(string[] data)
        {
            InitCardNumber = data[Cust.card_number];
            ChangedCardNumber = InitCardNumber;
            Name = data[Cust.name];
            Surname = data[Cust.surname];
            Patronymic = data[Cust.patronymic];
            PhoneNumber = data[Cust.phone_number];
            City = data[Cust.city];
            Street = data[Cust.street];
            Zipcode = data[Cust.zipcode];
            Percent = data[Cust.percent];
        }

        private void UpdateCustomer()
        {
            string result = CustomerValidator.ValidateUpdate(InitCardNumber, ChangedCardNumber, Surname, Name, Patronymic,
                PhoneNumber, City, Street, Zipcode, Percent);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }
            
            Cust.UpdateCustomer(InitCardNumber, ChangedCardNumber, Surname, Name, Patronymic,
                PhoneNumber, City, Street, Zipcode, Percent);

            CloseWindow();
        }

        private void DeleteCustomer()
        {
            Cust.DeleteCustomerByCardNumber(InitCardNumber);
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChangedCardNumber)
                && !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Surname)
                && !string.IsNullOrWhiteSpace(PhoneNumber)
                && !string.IsNullOrWhiteSpace(Percent);
        }
    }
}
