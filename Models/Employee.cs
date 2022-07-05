using System;
using System.Collections.Generic;
using supermarket.Utils;
using supermarket.Data;

namespace supermarket.Models
{
    internal static class Employee
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
        
        private const int AllPosition = -1;

        public static List<string[]> GetAllEmployee(string roleName = Constants.AllString, string surname = "", bool isQuery = false)
        {
            string whereClause = "WHERE 1 ";

            whereClause = roleName == Constants.AllString ? whereClause : whereClause +=
                "AND empl_role_id IN " + 
                    "(SELECT employee_role_id " + 
                    "FROM Employee_Role " +
                    $"WHERE employee_role_title='{roleName}')";

            whereClause = surname == "" ? whereClause : whereClause +=
                $"AND empl_surname LIKE '%{surname}%'";

            whereClause = (!isQuery) ? whereClause : whereClause +=
                " AND NOT EXISTS (SELECT card_number " +
                                " FROM Customer_Card " +
                                " WHERE NOT EXISTS (SELECT * " +
                                                    "FROM Receipt " +
                                                    "WHERE Customer_Card.card_number = card_number " +
                                                    "AND Employee.id_employee=id_employee ) AND Customer_Card.city = 'Київ' );";


            string sql = "SELECT * FROM Employee " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static string[] GetEmployeeById(string employeeId)
        {
            string sql = $"SELECT * FROM Employee WHERE id_employee='{employeeId}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static int DeleteEmployeeById(string employeeId)
        {
            string sql = $"DELETE FROM Employee WHERE id_employee='{employeeId}'";
            int response = DbUtils.Execute(sql);
            return response;
        }

        public static string[] GetEmployeeByPhone(string phoneNumber)
        {
            string sql = $"SELECT * FROM Employee WHERE phone_number='{phoneNumber}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static List<string[]> GetCashierLikeIdOrSNP(string IdOrSNP = Constants.AllString) // SNP - Surname Name Patronymic
        {
            string whereClause = "WHERE Employee_Role.employee_role_title = 'Касир' ";

            whereClause = IdOrSNP == Constants.AllString ? whereClause : whereClause +=
                $"AND (id_employee LIKE '%{IdOrSNP}%' OR empl_surname LIKE '%{IdOrSNP}%' OR empl_name LIKE '%{IdOrSNP}%' OR empl_patronymic LIKE '%{IdOrSNP}%') ";


            string sql = "SELECT * FROM Employee LEFT JOIN Employee_Role " +
                            "ON Employee_Role.employee_role_id = Employee.empl_role_id " + whereClause;
            List<string[]> result = DbUtils.FindAll(sql);

            if (result.Count > 0) return result;

            sql = "SELECT * FROM Employee LEFT JOIN Employee_Role " +
                            "ON Employee_Role.employee_role_id = Employee.empl_role_id " +
                            "WHERE Employee_Role.employee_role_title = 'Касир' ";

            result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result : null;
        }

        public static void AddEmployee(string surname, string name, string patronymic, string role,
            string salary, DateTime dateBirth, DateTime dateStart, string phoneNumber,
            string password, string city, string street, string zipcode)
        {
            string sql = "INSERT INTO Employee " + "(empl_surname, empl_name, empl_patronymic, empl_role_id, salary, " +
                         "date_of_birth, date_of_start, phone_number, password, city, street, zip_code) " +
                         $"VALUES ('{surname}', '{name}', '{patronymic}', {Roles.roleKeys[role]}, {float.Parse(salary)}, " +
                         $"'{dateBirth.ToString(Constants.DateTimeStringFormat)}', '{dateStart.ToString(Constants.DateTimeStringFormat)}', '{phoneNumber}', " +
                         $"'{CryptUtils.HashPassword(password)}', '{city}', '{street}', '{zipcode}')";

            DbUtils.Execute(sql);
        }

        public static void UpdateEmployee(string id, string surname, string name, string patronymic, string role,
            string salary, DateTime dateBirth, DateTime dateStart, string phoneNumber,
            string password, string city, string street, string zipcode)
        {
            string sql = "UPDATE Employee " +
                         $"SET empl_surname='{surname}', empl_name='{name}', empl_patronymic='{patronymic}', " +
                         $"empl_role_id={Roles.roleKeys[role]}, salary={float.Parse(salary)}, " +
                         $"date_of_birth='{Convert.ToDateTime(dateBirth).ToString(Constants.DateTimeStringFormat)}', " +
                         $"date_of_start='{Convert.ToDateTime(dateStart).ToString(Constants.DateTimeStringFormat)}', " +
                         $"phone_number='{phoneNumber}', password='{password}', city='{city}', " +
                         $"street='{street}', zip_code='{zipcode}'" +
                         $"WHERE id_employee='{id}'";

            DbUtils.Execute(sql);
        }
    }
}
