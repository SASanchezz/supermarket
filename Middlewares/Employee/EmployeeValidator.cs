using System;
using supermarket.Utils;
using Empl = supermarket.Models.Employee;

namespace supermarket.Middlewares.Employee
{
    public static class EmployeeValidator
    {
         public static void ValidateInsert(string surname, string name, string patronymic, string role,
            string salary, DateTime birtDate, DateTime startDate, string phoneNumber,
            string city, string street, string zipcode, string password)
        {

            if (surname.Length is 0 or > 51)
            {
                throw new Exception("Введіть прізвище довжиною < 51");
            }
            
            if (name.Length is 0 or > 51)
            {
                throw new Exception("Введіть ім'я довжиною < 51");
            }
            
            if (patronymic != null && patronymic.Length > 51)
            {
                throw new Exception("Введіть по батькові довжиною < 51");
            }

            /*
             * Check if we have such role in static data
             */
            // try
            // {
            //     int findRole = Roles.roleKeys[role];
            // } 
            // catch (KeyNotFoundException)
            // {
            //     throw new Exception("Нема такої ролі");
            // }

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
                throw new Exception("Некоректно введена зарплата");
            }

            /*
            * Check for age correctness 
            */
            int age = DateUtils.GetAge(birtDate, DateTime.Now);
            if (age < 18)
            {
                throw new Exception("Особа не досягла повноліття");
            }
            
            int ageAtStart = DateUtils.GetAge(birtDate, startDate);
            if (ageAtStart < 18)
            {
                throw new Exception("Особа не досягла повноліття на момент початку роботи");
            }

            /*
            * Check for phone correctness (we should add check for letters)
            */
            if (phoneNumber.Length is > 13 or < 10)
            {
                throw new Exception("Номер телефу некоректний");
            }
            
            if (Models.Employee.GetEmployeeByPhone(phoneNumber) != null)
            {
                throw new Exception("Номер телефону зайнятий");
            }

            if (city.Length is 0 or > 51)
            {
                throw new Exception("Введіть місто довжиною < 51");
            }
            
            if (street.Length is 0 or > 51)
            {
                throw new Exception("Введіть вулицю довжиною < 51");
            }
            
            if (zipcode.Length is 0 or > 9)
            {
                throw new Exception("Введіть поштовий індекс довжиною < 10");
            }

            if (password.Length is 0 or > 16)
            {
                throw new Exception("Введіть пароль до 16 символів");
            }
        }
         
         public static void ValidateUpdate(string id, string surname, string name, string patronymic, string role,
            string salary, DateTime birtDate, DateTime startDate, string phoneNumber,
            string city, string street, string zipcode, string password)
        {

            if (surname.Length is 0 or > 51)
            {
                throw new Exception("Введіть прізвище довжиною < 51");
            }
            if (name.Length is 0 or > 51)
            {
                throw new Exception("Введіть ім'я довжиною < 51");
            }
            if (patronymic != null && patronymic.Length > 51)
            {
                throw new Exception("Введіть по батькові довжиною < 51");
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
                throw new Exception("Некоректно введена зарплата");
            }

            /*
            * Check for age correctness 
            */
            int age = DateUtils.GetAge(birtDate, DateTime.Now);
            if (age < 18)
            {
                throw new Exception("Особа не досягла повноліття");
            }
            
            int ageAtStart = DateUtils.GetAge(birtDate, startDate);
            if (ageAtStart < 18)
            {
                throw new Exception("Особа не досягла повноліття на момент початку роботи");
            }

            /*
            * Check for phone correctness (we should add check for letters)
            */
            if (phoneNumber.Length is > 13 or < 10)
            {
                throw new Exception("Номер телефу некоректний");
            }
            
            string[] employee = Models.Employee.GetEmployeeByPhone(phoneNumber);
            if (employee != null && employee[Empl.id] != id)
            {
                throw new Exception("Номер телефону зайнятий");
            }

            if (city.Length is 0 or > 51)
            {
                throw new Exception("Введіть місто довжиною < 51");
            }
            
            if (street.Length is 0 or > 51)
            {
                throw new Exception("Введіть вулицю довжиною < 51");
            }
            
            if (zipcode.Length is 0 or > 9)
            {
                throw new Exception("Введіть поштовий індекс довжиною < 10");
            }

            if (password.Length != 0 && password.Length > 16)
            {
                throw new Exception("Введіть пароль до 16 символів");
            }
        }
    }
}