using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.Data
{
    class DbMaps
    {
        public enum EmployeeMap : int
        {
            id_employee,
            empl_surname,
            empl_name,
            empl_patronumic,
            empl_role,
            salary,
            date_of_birth,
            date_of_start,
            phone_number,
            password,
            city,
            street,
            zipcode
        }
    }
}
