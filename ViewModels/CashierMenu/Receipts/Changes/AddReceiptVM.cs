using supermarket.Utils;
using System;
using System.Windows;
using supermarket.Middlewares.Receipts;
using supermarket.Models;
using supermarket.ViewModels.BaseClasses;
using Receipt = supermarket.Models.Receipt;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace supermarket.ViewModels.CashierMenu.Receipts.Changes
{
    internal class AddReceiptVM : ViewModel
    {
        private const string cashierId = "44"; //constant cashier id

        private string _cashier_id = cashierId;
        private string _client_card = "";
        private string _product_amount = "";
        private string _choosen_product = "";
        private double _sum = 0;
       
        public AddReceiptVM()
        {
            AddReceiptCommand = new RelayCommand<object>(_ => AddReceipt(), CanExecute);
            AddNewProductCommand = new RelayCommand<object>(_ => AddNewProduct(), CanAddNewProduct);
            DeleteProductFromListCommand = new RelayCommand<object>(_ => DeleteProductFromList());
            CloseCommand = new RelayCommand<object>(_ => CloseWindow());
        }
        
        public RelayCommand<object> AddNewProductCommand { get; }
        public RelayCommand<object> DeleteProductFromListCommand { get; }
        
        public RelayCommand<object> AddReceiptCommand { get; }
        
        public RelayCommand<object> CloseCommand { get; }

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

        public ObservableCollection<string[]> SelectedProducts { get; set; } = new();

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
                OnPropertyChanged(nameof(SelectiveStoreProduct));
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
        public string[] SelectedStoreProductInReceipt { get; set; }

        /**
         * Sum of all products
         */
        public double Sum
        {
            get => Math.Round(_sum, 2);
            set
            {
                if (value < 0) _sum = 0;
                else _sum = value;
                OnPropertyChanged();
            }
        }


        private void ResetFields()
        {
            ClientCard = "";
            Sum = 0;
        }

        private void ResetNewProductFields()
        {
            ChoosenProduct = "";
            ProductAmount = "";
        }

        private void DeleteProductFromList()
        {
            if (SelectedStoreProductInReceipt == null)
            {
                MessageBox.Show("Оберіть товар з чеку");
                return;
            }
            
            Sum -= double.Parse(SelectedStoreProductInReceipt[3]);
            SelectedProducts.Remove(SelectedStoreProductInReceipt);


            
        }
        /**
         * Method to add new product to list
         */
        private void AddNewProduct()
        {
            ////Validates entered information
            string[] storeProiduct = StoreProduct.GetStoreProductByUPC(_choosen_product.Split(" ")[0]);
            if (storeProiduct == null)
            {
                MessageBox.Show("Немає такого товару");
                return;
            }

            try
            {
                int.Parse(ProductAmount);
            }
            catch 
            {
                MessageBox.Show("Некоректна кількість товару");
                return;
            }
            
            if (int.Parse(ProductAmount) <= 0)
            {
                MessageBox.Show("Кількість товару від'ємна");
                return;
            }

            string[] storeProduct = StoreProduct.GetStoreProductByUPC(_choosen_product.Split(" ")[0]);
            double generalProductsPrice = Math.Round(double.Parse(ProductAmount) * double.Parse(storeProduct[StoreProduct.selling_price]), 2);

            string[] listRow = new string[storeProduct.Length + 3];
            storeProduct.CopyTo(listRow, 0);
            listRow[7] = ProductAmount;
            listRow[8] = storeProduct[StoreProduct.selling_price];
            listRow[9] = generalProductsPrice.ToString();

            SelectedProducts.Add(listRow);

            /**
             * Add products to general sum
             */

            Sum += generalProductsPrice;

            ResetNewProductFields();
        }

        private void AddReceipt()
        {
            ////Validates entered information
            string result = ReceiptValidator.Validate(ClientCard, Sum.ToString());

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            //Query to insert new employee
            string receiptId = Receipt.AddReceipt(_cashier_id, ClientCard, Sum.ToString());
            foreach(var storeProduct in SelectedProducts)
            {
                Sale.AddSale(storeProduct[StoreProduct.UPC], receiptId, storeProduct[7], storeProduct[9]);
            }
            

            ResetFields();
            CloseWindow();
        }

        private bool CanExecute(object obj)
        {
            return true;
        }
        
        private bool CanAddNewProduct(object obj)
        {
            return !string.IsNullOrWhiteSpace(ChoosenProduct) &&
                   !string.IsNullOrWhiteSpace(ProductAmount);
        }
    }
}
