using System.Windows;
using supermarket.Utils;
using supermarket.ViewModels;
/*
* Main window with Signing in
*/
namespace supermarket
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DbUtils.SetDbConnection();
            InitializeComponent();

            DataContext = new MainWindowVM();
        }
    }
}