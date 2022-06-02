﻿using System;
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

        const string s_format = "yyyy-MM-dd HH:mm:ss";
        private const string AllString = "Всі";

        public static List<string[]> GetAllEmployee(string roleName = AllString, string surname = "")
        {
            string whereClause = "WHERE 1 ";

            whereClause = roleName == AllString ? whereClause : whereClause +=
                "AND empl_role_id IN " + 
                    "(SELECT employee_role_id " + 
                    "FROM Employee_Role " +
                    $"WHERE employee_role_title='{roleName}')";

            whereClause = surname == "" ? whereClause : whereClause +=
                $"AND empl_surname LIKE '%{surname}%'";


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

        public static void DeleteEmployeeById(string employeeId)
        {
            string sql = $"DELETE FROM Employee WHERE id_employee='{employeeId}'";
            DbUtils.Execute(sql);
        }

        public static string[] GetEmployeeByPhone(string phoneNumber)
        {
            string sql = $"SELECT * FROM Employee WHERE phone_number='{phoneNumber}'";
            List<string[]> result = DbUtils.FindAll(sql);

            return result.Count > 0 ? result[0] : null;
        }

        public static void AddEmployee(string surname, string name, string patronymic, string role,
            string salary, DateTime dateBirth, DateTime dateStart, string phoneNumber,
            string password, string city, string street, string zipcode)
        {
            string sql = "INSERT INTO Employee " + "(empl_surname, empl_name, empl_patronymic, empl_role_id, salary, " +
                         "date_of_birth, date_of_start, phone_number, password, city, street, zip_code) " +
                         $"VALUES ('{surname}', '{name}', '{patronymic}', {Roles.roleKeys[role]}, {float.Parse(salary)}, " +
                         $"'{dateBirth.ToString(s_format)}', '{dateStart.ToString(s_format)}', '{phoneNumber}', " +
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
                         $"date_of_birth='{Convert.ToDateTime(dateBirth).ToString(s_format)}', " +
                         $"date_of_start='{Convert.ToDateTime(dateStart).ToString(s_format)}', " +
                         $"phone_number='{phoneNumber}', password='{password}', city='{city}', " +
                         $"street='{street}', zip_code='{zipcode}'" +
                         $"WHERE id_employee='{id}'";

            DbUtils.Execute(sql);
        }
    }
}
