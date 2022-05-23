using supermarket.Middlewares.Customer;
using supermarket.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using cstmr = supermarket.Data.DbMaps.CustomerMap;


namespace supermarket.Windows.ManagerMenu.ClientWindows
{
    public partial class ManageClientWindow : Window
    {
        string _customerId;
        public ManageClientWindow(string customerId)
        {
            //_customerId = customerId;
            InitializeComponent();
            //FillBoxes();
        }

        ///*
        //* This method fills boxes in window with information from database
        //*/
        //private void FillBoxes()
        //{
        //    string[] customer = DbQueries.GetCustomerByID(_customerId)[0];

        //    cardNumberBox.Text = customer[(int)cstmr.card_number];
        //    surnameBox.Text = customer[(int)cstmr.cust_surname];
        //    nameBox.Text = customer[(int)cstmr.cust_name];
        //    patronymicBox.Text = customer[(int)cstmr.cust_patronymic];
        //    phoneNumberBox.Text = customer[(int)cstmr.phone_number];
        //    cityBox.Text = customer[(int)cstmr.city];
        //    streetBox.Text = customer[(int)cstmr.street];
        //    zipcodeBox.Text = customer[(int)cstmr.zip_code];
        //    percentBox.Text = customer[(int)cstmr.percent];
        //}

        ///*
        //* This method updates information in database from text boxes
        //*/
        //public void UpdateClick(object sender, RoutedEventArgs e)
        //{
        //    ManagerClientsWindow owner = (ManagerClientsWindow)Owner;
        //    //Renew buttons in MainProductWindow
        //    owner.DeleteOldButtons();

        //    string cardNumber = (cardNumberBox.Text != _customerId) ? cardNumberBox.Text : "90";
        //    string surname = surnameBox.Text;
        //    string name = nameBox.Text;
        //    string patronymic = patronymicBox.Text;
        //    string phoneNumber = phoneNumberBox.Text;
        //    string city = cityBox.Text;
        //    string street = streetBox.Text;
        //    string zipcode = zipcodeBox.Text;
        //    string percent = percentBox.Text;

        //    string result = CustomerValidator.Validate(cardNumber, surname, name, patronymic, phoneNumber, city, street, zipcode, percent);


        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    cardNumber = cardNumberBox.Text;

        //    string sql = string.Format("UPDATE Customer_Card SET " +
        //        "card_number='{1}', cust_surname='{2}', cust_name='{3}', cust_patronymic='{4}', phone_number='{5}', city='{6}', street='{7}', zip_code='{8}', percent={9} " +
        //        "WHERE card_number='{0}'",
        //        _customerId, cardNumber, surname, name, patronymic, phoneNumber, city, street, zipcode, percent);

        //    DbUtils.Execute(sql);


        //    owner.SetButtons(owner.SetSortList());
        //    owner.Show();
        //    Close();
        //}
        ///*
        // * Delete production unit from database
        // */
        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    ManagerClientsWindow owner = (ManagerClientsWindow)Owner; //So we can renew buttons 
        //    owner.DeleteOldButtons();
        //    DbQueries.DeleteCategoryByID(_customerId);
        //    owner.SetButtons(owner.SetSortList());
        //    owner.Show();
        //    Close();
        //}

        //private void ReturnClick(object sender, RoutedEventArgs e)
        //{
        //    Owner.Show();
        //    Close();
        //}
    }
}
