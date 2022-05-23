using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.Utils;
using supermarket.Data;

namespace supermarket.Models
{
    public static class Employee
    {
        const string s_format = "yyyy-MM-dd HH:mm:ss";

        public static void AddEmployee(string surname, string name, string patronymic, string role,
            string salary, DateTime dateBirth, DateTime dateStart, string phoneNumber,
            string password, string city, string street, string zipcode)
        {
            string sql = String.Format("INSERT INTO Employee " +
                "(empl_surname, empl_name, empl_patronymic, empl_role_id, salary, " +
                "date_of_birth, date_of_start, phone_number, password, city, street, zip_code) " +
                "VALUES ('{0}', '{1}', '{2}', '{3}', {4}, " +
                "'{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                surname, name, patronymic, Roles.roleKeys[role], float.Parse(salary),
                dateBirth.ToString(s_format), dateStart.ToString(s_format),
                phoneNumber, CryptUtils.HashPassword(password), city, street, zipcode);

            DbUtils.Execute(sql);
        }
    }
}
