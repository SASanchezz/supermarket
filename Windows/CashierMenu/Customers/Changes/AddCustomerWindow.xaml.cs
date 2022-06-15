using System.Windows;

namespace supermarket.Windows.CashierMenu.Customers.Changes
{
    public partial class AddCustomerWindow : Window
    {
        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        /*
        * This method is realization for adding new product to database
        */
        //public void AddClick(object sender, RoutedEventArgs e)
        //{
        //    string cardNumber = cardNumberBox.Text;
        //    string surname = surnameBox.Text;
        //    string name = nameBox.Text;
        //    string patronymic = patronymicBox.Text;
        //    string phoneNumber = phoneNumberBox.Text;
        //    string city = cityBox.Text;
        //    string street = streetBox.Text;
        //    string zipcode = zipcodeBox.Text;
        //    string percent = percentBox.Text;

        //    //Validate enetered data
        //    string result = CustomerValidator.Validate(cardNumber, surname, name, patronymic, phoneNumber, city, street, zipcode, percent);

        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    string sql = String.Format("INSERT INTO Customer_Card " +
        //        "(card_number, cust_surname, cust_name, cust_patronymic, phone_number, city, street, zip_code, percent) " +
        //        "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8})",
        //        cardNumber, surname, name, patronymic, phoneNumber, city, street, zipcode, percent);

        //    DbUtils.Execute(sql);

        //    ManagerClientsWindow owner = (ManagerClientsWindow)Owner;
        //    //Renew buttons in MainProductWindow (i.e. add new product window)
        //    owner.DeleteOldButtons();
        //    owner.SetButtons(owner.SetSortList());
        //    owner.Show();
        //    Close();
        //}
    }
}
