﻿using supermarket.Middlewares.Category;
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
            InitializeComponent();
        }
        
    }
}
