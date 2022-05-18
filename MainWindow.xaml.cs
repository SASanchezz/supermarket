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
            DbUtils.SetDbConnection(); //open connection to MySQL
            var vm = new MainWindowVM();
            DataContext = vm;
            vm.ClosingRequest += (sender, e) => this.Close();
        }
    }
}