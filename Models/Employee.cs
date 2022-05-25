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
        //Native
        public const int id = 0;
        public const int surname = 1;
        public const int name = 2;
        public const int patronymic = 3;
        public const int role = 4;
        public const int salary = 5;
        public const int date_of_birth = 6;
        public const int date_of_start = 7;
        public const int phone_number = 8;
        public const int password = 9;
        public const int city = 10;
        public const int street = 11;
        public const int zipcode = 12;

        const string s_format = "yyyy-MM-dd HH:mm:ss";

        public static List<string[]> GetAllEmployee()
        {
            string sql = "SELECT * FROM Employee";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static string[] GetEmployeeByID(string employeeId)
        {
            string sql = string.Format("SELECT * FROM Employee WHERE id_employee='{0}'", employeeId);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static List<string[]> GetEmployeesByRole(int employeeRole)
        {
            string sql = string.Format("SELECT * FROM Employee WHERE empl_role_id={0}", employeeRole);

            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }
        public static void DeleteEmployeeByID(string employeeId)
        {
            string sql = string.Format("DELETE FROM Employee WHERE id_employee='{0}'", employeeId);
            DbUtils.Execute(sql);
        }

        public static string[] GetEmployeeByPhone(string phoneNumber)
        {
            string sql = string.Format("SELECT * FROM Employee WHERE phone_number='{0}'", phoneNumber);
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static void AddEmployee(string surname, string name, string patronymic, string role,
            string salary, DateTime dateBirth, DateTime dateStart, string phoneNumber,
            string password, string city, string street, string zipcode)
        {
            string sql = String.Format("INSERT INTO Employee " +
                "(empl_surname, empl_name, empl_patronymic, empl_role_id, salary, " +
                "date_of_birth, date_of_start, phone_number, password, city, street, zip_code) " +
                "VALUES ('{0}', '{1}', '{2}', {3}, {4}, " +
                "'{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')",
                surname, name, patronymic, Roles.roleKeys[role], float.Parse(salary),
                dateBirth.ToString(s_format), dateStart.ToString(s_format),
                phoneNumber, CryptUtils.HashPassword(password), city, street, zipcode);

            DbUtils.Execute(sql);
        }

        public static void UpdateEmployee(string id, string surname, string name, string patronymic, string role,
            string salary, DateTime dateBirth, DateTime dateStart, string phoneNumber,
            string password, string city, string street, string zipcode)
        {
            string sql = string.Format("UPDATE Employee SET " +
                "empl_surname='{1}', empl_name='{2}', empl_patronymic='{3}', empl_role_id={4}, salary={5}," +
                "date_of_birth='{6}', date_of_start='{7}', phone_number='{8}', password='{9}', city='{10}', street='{11}', zip_code='{12}'" +
                "WHERE id_employee='{0}'",
                id, surname, name, patronymic, Roles.roleKeys[role], float.Parse(salary),
                Convert.ToDateTime(dateBirth).ToString(s_format), Convert.ToDateTime(dateStart).ToString(s_format),
                phoneNumber, password, city, street, zipcode);

            DbUtils.Execute(sql);
        }
    }
}
