using System.Collections.Generic;
using System.Windows;
using supermarket.Models;

namespace supermarket.Windows.ManagerMenu.Products
{
    public partial class Query_2_Window : Window
    {
        public Query_2_Window()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        public List<string[]> Products { get; set; }

        public void SetData()
        {
            Products = Product.GetPopularProducts();
        }
    }
}