using System;
using System.Collections.Generic;
using supermarket.Data;
using supermarket.Utils;
using Empl = supermarket.Models.Employee;

namespace supermarket.Middlewares.Employee
{
    public class EmployeeValidator
    {
         public static string ValidateInsert(string surname, string name, string patronymic, string role,
            string salary, DateTime birtDate, DateTime startDate, string phoneNumber,
            string city, string street, string zipcode, string password)
        {

            if (surname.Length is 0 or > 51)
            {
                return "Введіть прізвище довжиною < 51";
            }
            
            if (name.Length is 0 or > 51)
            {
                return "Введіть ім'я довжиною < 51";
            }
            
            if (patronymic != null && patronymic.Length > 51)
            {
                return "Введіть по батькові довжиною < 51";
            }

            /*
             * Check if we have such role in static data
             */
            try
            {
                int findRole = Roles.roleKeys[role];
            } 
            catch (KeyNotFoundException)
            {
                return "Нема такої ролі";
            }

            /*
             * Check if salary is enetered correctly
             */
            
            //salary = salary.Replace('.', ',');
            try
            {
                float.Parse(salary);
            }
            catch (FormatException)
            {
                return "Некоректно введена зарплата";
            }

            /*
            * Check for age correctness 
            */
            int age = DateUtils.GetAge(birtDate, DateTime.Now);
            if (age < 18)
            {
                return "Особа не досягла повноліття";
            }
            
            int ageAtStart = DateUtils.GetAge(birtDate, startDate);
            if (ageAtStart < 18)
            {
                return "Особа не досягла повноліття на момент початку роботи";
            }

            /*
            * Check for phone correctness (we should add check for letters)
            */
            if (phoneNumber.Length is > 13 or < 10)
            {
                return "Номер телефу некоректний";
            }
            
            if (Models.Employee.GetEmployeeByPhone(phoneNumber) != null)
            {
                return "Номер телефону зайнятий";
            }

            if (city.Length is 0 or > 51)
            {
                return "Введіть місто довжиною < 51";
            }
            
            if (street.Length is 0 or > 51)
            {
                return "Введіть вулицю довжиною < 51";
            }
            
            if (zipcode.Length is 0 or > 9)
            {
                return "Введіть поштовий індекс довжиною < 10";
            }

            if (password.Length is 0 or > 16)
            {
                return "Введіть пароль до 16 символів";
            }

            return ""; //Alright
        }
         
         public static string ValidateUpdate(string id, string surname, string name, string patronymic, string role,
            string salary, DateTime birtDate, DateTime startDate, string phoneNumber,
            string city, string street, string zipcode, string password)
        {

            if (surname.Length is 0 or > 51)
            {
                return "Введіть прізвище довжиною < 51";
            }
            if (name.Length is 0 or > 51)
            {
                return "Введіть ім'я довжиною < 51";
            }
            if (patronymic != null && patronymic.Length > 51)
            {
                return "Введіть по батькові довжиною < 51";
            }

            /*
             * Check if salary is enetered correctly
             */
            //salary = salary.Replace(',', '.');
            try
            {
                float.Parse(salary);
            }
            catch (FormatException)
            {
                return "Некоректно введена зарплата";
            }

            /*
            * Check for age correctness 
            */
            int age = DateUtils.GetAge(birtDate, DateTime.Now);
            if (age < 18)
            {
                return "Особа не досягла повноліття";
            }
            
            int ageAtStart = DateUtils.GetAge(birtDate, startDate);
            if (ageAtStart < 18)
            {
                return "Особа не досягла повноліття на момент початку роботи";
            }

            /*
            * Check for phone correctness (we should add check for letters)
            */
            if (phoneNumber.Length is > 13 or < 10)
            {
                return "Номер телефу некоректний";
            }
            
            string[] employee = Models.Employee.GetEmployeeByPhone(phoneNumber);
            if (employee != null && employee[Empl.id] != id)
            {
                return "Номер телефону зайнятий";
            }

            if (city.Length is 0 or > 51)
            {
                return "Введіть місто довжиною < 51";
            }
            
            if (street.Length is 0 or > 51)
            {
                return "Введіть вулицю довжиною < 51";
            }
            
            if (zipcode.Length is 0 or > 9)
            {
                return "Введіть поштовий індекс довжиною < 10";
            }

            if (password.Length != 0 && password.Length > 16)
            {
                return "Введіть пароль до 16 символів";
            }

            return ""; //Alright
        }
    }
}