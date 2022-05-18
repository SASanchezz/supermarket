using supermarket.Utils;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace supermarket.ViewModels
{
    public class MainUserWindowVM : ViewModel
    {
        private readonly List<string[]> _employees = DbQueries.GetAllEmployee();
        private RelayCommand<object> _startAddingNewEmployeeCommand;

        public List<string[]> Employees { get => _employees; }
        public RelayCommand<object> StartAddingNewEmployeeCommand
        {
            get
            {
                return _startAddingNewEmployeeCommand ??= new RelayCommand<object>(_ => StartAddingNewEmployee());
            }
        }

        private void StartAddingNewEmployee()
        {
            //_employees.Add()
        }

        //public void SetEmployeeButtons()
        //{
        //   }
        //    //RegisterName(grid.Name, grid);

        //}
    }
}