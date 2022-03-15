using System.Collections.Generic;

namespace supermarket.Utils
{
    public static class DbQueries
    {

        public static List<string[]> GetAllEmployee()
        {
            string sql = "SELECT * FROM Employee";
            return DbUtils.FindAll(sql);
        }
        public static List<string[]> GetEmployeeByID(string employeeId)
        {
            string sql = string.Format("SELECT * FROM Employee WHERE id_employee='{0}'", employeeId);
            return DbUtils.FindAll(sql);
        }

        public static void DeleteEmployeeByID(string employeeId)
        {
            string sql = string.Format("DELETE FROM Employee WHERE id_employee='{0}'", employeeId);
            DbUtils.Execute(sql);
        }
    }
}
