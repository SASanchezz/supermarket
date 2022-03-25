using supermarket.Middlewares.Category;
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
using ctgry = supermarket.Data.DbMaps.CategoryMap;

namespace supermarket.Windows.ManagerMenu.CategoryWindows
{
    public partial class ManageCategoryWindow : Window
    {
        private string _categoryId;

        public ManageCategoryWindow(string categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();
            FillBoxes();
        }

        /*
        * This method fills boxes in window with information from database
        */
        private void FillBoxes()
        {
            string[] category = DbQueries.GetCategoryByID(_categoryId)[0];

            idBox.Text = category[(int)ctgry.category_number];
            nameBox.Text = category[(int)ctgry.category_name];
        }

        /*
        * This method updates information in database from text boxes
        */
        public void UpdateClick(object sender, RoutedEventArgs e)
        {
            MainCategoryWindow owner = (MainCategoryWindow)Owner;
            //Renew buttons in MainProductWindow
            owner.DeleteOldButtons();

            string categoryNumber = (idBox.Text != _categoryId) ? idBox.Text : "9999999999099999991999999999";
            string categoryName = nameBox.Text;

            string result = CategoryValidator.Validate(categoryNumber, categoryName);


            if (result.Length != 0)
            {
                MessageBox.Show(result);
                return;
            }

            categoryNumber = idBox.Text;

            string sql = string.Format("UPDATE Category SET " +
                "category_number={1}, category_name='{2}'" +
                "WHERE category_number={0}",
                _categoryId, categoryNumber, categoryName);

            DbUtils.Execute(sql);

            
            owner.SetButtons(owner.SetSortList());
            owner.Show();
            Close();
        }
        /*
         * Delete production unit from database
         */
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            MainCategoryWindow owner = (MainCategoryWindow)Owner; //So we can renew buttons 
            owner.DeleteOldButtons();
            DbQueries.DeleteCategoryByID(_categoryId);
            owner.SetButtons(owner.SetSortList());
            owner.Show();
            Close();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            Owner.UpdateLayout();
            Owner.Show();
            Close();
        }

    }
}
