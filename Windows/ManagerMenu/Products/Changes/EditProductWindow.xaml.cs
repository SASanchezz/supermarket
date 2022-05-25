using System.Windows;

namespace supermarket.Windows.ManagerMenu.Products.Changes
{
    public partial class EditProductWindow : Window
    {
        //private string _productId;

        public EditProductWindow()
        {
            InitializeComponent();
        }

        ///*
        //* This method sets comboBox with categories that we retrieve from database
        //* (the same as in AddProductWindow. Probably, we should make some inheritance later)
        //*/
        //private void SetCategoryList()
        //{
        //    List<string[]> categoryList = DbQueries.GetAllCategories();
        //    ComboBox comboBox = new();
        //    comboBox.Name = "categoryList";

        //    foreach (string[] category in categoryList)
        //    {
        //        comboBox.Items.Add(IdUtils.Compound(category[(int)ctgry.category_name], category[(int)ctgry.category_number]));
        //    }

        //    RegisterName(comboBox.Name, comboBox);
        //    productPanel.Children.Add(comboBox);
        //}

        ///*
        //* This method fills boxes in window with information from database
        //*/
        //private void FillBoxes()
        //{
        //    string[] product = DbQueries.GetProductByID(_productId)[0];

        //    ComboBox categoryList = (ComboBox)FindName("categoryList");
        //    string[] categoryRow = DbQueries.GetCategoryByID(product[(int)prdct.category_number])[0];

        //    idBox.Text = product[(int)prdct.id_product];
        //    categoryList.Text =  IdUtils.Compound(categoryRow[(int)ctgry.category_name], categoryRow[(int)ctgry.category_number]);
        //    nameBox.Text = product[(int)prdct.product_name];
        //    characteristicBox.Text = product[(int)prdct.characteristics];
        //}

        ///*
        //* This method updates information in database from text boxes
        //*/
        //public void UpdateClick(object sender, RoutedEventArgs e)
        //{
        //    MainProductWindow owner = (MainProductWindow)Owner;
        //    //Renew buttons in MainProductWindow
        //    owner.DeleteOldProductButtons();

        //    ComboBox categoryList = (ComboBox)FindName("categoryList");

        //    string idProduct = (idBox.Text != _productId) ? idBox.Text : "9999999999099999991999999999";
        //    string categoryNumber = IdUtils.Decompound(categoryList.Text)[1];
        //    string name = nameBox.Text;
        //    string characteristic = characteristicBox.Text;

        //    string result = ProductValidator.Validate(idProduct, name, categoryNumber, characteristic);


        //    if (result.Length != 0)
        //    {
        //        MessageBox.Show(result);
        //        return;
        //    }

        //    idProduct = idBox.Text;
        //    string sql = string.Format("UPDATE Product SET " +
        //        "id_product={1}, category_number={2}, product_name='{3}', characteristics='{4}' " +
        //        "WHERE id_product={0}",
        //        _productId, idProduct, categoryNumber, name, characteristic);

        //    DbUtils.Execute(sql);

        //    owner.SetProductButtons(owner.SetSortList());
        //    owner.Show();
        //    Close();
        //}
        ///*
        // * Delete production unit from database
        // */
        //private void DeleteClick(object sender, RoutedEventArgs e)
        //{
        //    MainProductWindow owner = (MainProductWindow)Owner; //So we can renew buttons 
        //    owner.DeleteOldProductButtons();
        //    DbQueries.DeleteProductByID(_productId);
        //    owner.SetProductButtons(owner.SetSortList());
        //    owner.Show();
        //    Close();
        //}

        //private void ReturnClick(object sender, RoutedEventArgs e)
        //{
        //    Owner.UpdateLayout();
        //    Owner.Show();
        //    Close();
        //
        //}        
    }
}
