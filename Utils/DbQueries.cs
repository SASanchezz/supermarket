using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.Connections;

namespace supermarket.Utils
{
    public static class DbQueries
    {

        public static List<string> GetAllEmployee()
        {
            string sql = "SELECT * FROM Employee";
            return DBUtils.FindAll(sql);
        }
    }
}
