using System.Collections.Generic;
using System.Windows;
using supermarket.Models;

namespace supermarket.Windows.ManagerMenu.Sales
{
    public partial class Query_1_Window : Window
    {
        public Query_1_Window()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetManufacturer(string manufacturer)
        {
            ManufacturerProductsSales = Product.GetManufacturerCountSales(manufacturer);
        }
        
        public List<string[]> ManufacturerProductsSales { get; set; }
    }
}