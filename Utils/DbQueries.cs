using System.Collections.Generic;
using supermarket.Connections;

namespace supermarket.Utils
{
    public static class DbQueries
    {

        public static List<string[]> GetAllEmployee()
        {
            string sql = "SELECT * FROM Employee";
            return DbUtils.FindAll(sql);
        }
    }
}
