using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Middlewares.Receipts;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using Receipt = supermarket.Models.Receipt;
using System.Collections.Generic;

namespace supermarket.ViewModels.CashierMenu.Receipts.Changes
{
    internal class AddReceiptVM : ViewModel
    {
        private List<string[]> _selectedProducts = new(0);


        private const string cashierId = "44"; //constant cashier id

        private string _cashier_id = cashierId;
        private string _client_card = "";
        private string _product_amount = "";
        private string _choosen_product = "";
        private string _sum;
        public RelayCommand<object> AddNewProductCommand { get; }
        public RelayCommand<object> AddReceiptCommand { get; }
        public RelayCommand<object> CloseCommand { get; }
        public AddReceiptVM()
        {
            //Temp
            string[] one = { "Milk", "23" };
            _selectedProducts.Add(one);
            //Temp
            AddReceiptCommand = new RelayCommand<object>(_ => AddReceipt(), CanExecute);
            AddNewProductCommand = new RelayCommand<object>(_ => AddNewProduct(), CanAddNewProduct);
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }

        public List<string> SelectiveClients
        {
            get
            {
                List<string[]> customers = Models.Customer.GetCustomersLikeSNPOrCardOrPhone(_client_card);

                if (customers == null)
                {
                    return new List<string>(0);
                }

                List<string> resultCustomers = new(customers.Count);

                foreach (var customer in customers)
                {
                    resultCustomers.Add(customer[Customer.card_number] + "  --  " +
                        customer[Customer.phone_number] + "  --  " +
                        customer[Customer.surname]);
                }

                return resultCustomers;
            }
        }

        public List<string> SelectiveStoreProduct
        {
            get
            {
                List<string[]> strProducts = StoreProduct.GetStoreProductsLikeUPCOrProductName(_choosen_product);

                if (strProducts == null)
                {
                    return new List<string>(0);
                }

                List<string> resultStrProducts = new(strProducts.Count);

                foreach (var strProduct in strProducts)
                {
                    string isProm = strProduct[StoreProduct.promotional_product] == "True" ? "Акційний" : "Неакційний";

                    resultStrProducts.Add(strProduct[StoreProduct.UPC] + "  --  " +
                        strProduct[StoreProduct.product_name] + "  --  " +
                        isProm);
                }

                return resultStrProducts;
            }
        }

        public List<string[]> SelectedProducts
        {
            get => _selectedProducts;
            set
            {
                _selectedProducts = value;
            }
        }

        public string CashierId
        {
            get => _cashier_id;
            set
            {
                _cashier_id = value;
                OnPropertyChanged();
            }
        }

        public string ClientCard
        {
            get => _client_card.Split(' ')[0];
            set
            {
                _client_card = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectiveClients));
            }
        }

        /**
         * New product to add to list
         */
        public string ChoosenProduct
        {
            get
            {
                try
                {
                    return _choosen_product.Split(" -- ")[1];
                } catch { return _choosen_product.Split(" ")[0]; }
            }
            set
            {
                _choosen_product = value;
                OnPropertyChanged();
            }
        }
        /**
         * Amount of one selected product
         */
        public string ProductAmount
        {
            get => _product_amount;
            set
            {
                _product_amount = value;
                OnPropertyChanged();
            }
        }

        /**
         * Sum of all products
         */
        public string Sum
        {
            get => _sum;
            set
            {
                _sum = value;
                OnPropertyChanged();
            }
        }


        private void ResetFields()
        {
            ClientCard = "";
            Sum = "";
        }

        private void ResetNewProductFields()
        {
            ChoosenProduct = "";
            ProductAmount = "";
        }
        /**
         * Method to add new product to list
         */
        private void AddNewProduct()
        {
            ////Validates entered information
            if (StoreProduct.GetStoreProductByUPC(_choosen_product.Split(" ")[0]) == null)
            {
                MessageBox.Show("Немає такого товару");
                return;
            }

            try
            {
                int.Parse(ProductAmount);
            }
            catch {
                MessageBox.Show("Некоректна ціна товару");
                return;
            }
            if (int.Parse(ProductAmount) <= 0)
            {
                MessageBox.Show("Кількість товару від'ємна");
                return;
            }
            SelectedProducts.Add(new string[] { _choosen_product.Split(" -- ")[1], ProductAmount });

            OnPropertyChanged(nameof(SelectedProducts));
            ResetNewProductFields();
        }

        private void AddReceipt()
        {
            ////Validates entered information
            string result = ReceiptValidator.Validate(ClientCard, Sum);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            Receipt.AddReceipt(_cashier_id, ClientCard, Sum);

            ResetFields();
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Sum);
        }
        private bool CanAddNewProduct(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChoosenProduct) &&
                   !string.IsNullOrWhiteSpace(ProductAmount);
        }
    }
}
