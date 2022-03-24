using supermarket.Middlewares.Category;
using supermarket.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace supermarket.Windows.ManagerMenu.CategoryWindows
{
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
        }

        /*
        * This method is realization for adding new product to database
        */
        public void AddClick(object sender, RoutedEventArgs e)
        {
            string categoryNumber = idBox.Text;
            string categoryName = nameBox.Text;

            //Validate enetered data
            string result = CategoryValidator.Validate(categoryNumber, categoryName);

            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            string sql = String.Format("INSERT INTO Category " +
                "(category_number, category_name) " +
                "VALUES ({0}, '{1}')",
                categoryNumber, categoryName);

            DbUtils.Execute(sql);

            MainCategoryWindow owner = (MainCategoryWindow)Owner;
            //Renew buttons in MainProductWindow (i.e. add new product window)
            owner.DeleteOldButtons();
            owner.SetButtons(owner.SetSortList());
            owner.Show();
            Close();
        }

        public void ReturnClick(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();
        }
    }
}

