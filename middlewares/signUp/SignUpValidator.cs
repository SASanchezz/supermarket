using System;
using System.Collections.Generic;
using System.Linq;
using supermarket.Connections;
using supermarket.Utils;
using supermarket.Data;

namespace supermarket.Middlewares.SignUp
{
    public static class SignUpValidator
    {
        public static string Validate(string surname, string name, string patronymic, string role,
            string salary, string dateBirth, string dateStart, string phoneNumber,
            string city, string street, string zipcode, string password)
        {
            string uniqueId;
            List<string[]> result;
            do //to avoid similar id's
            {
                uniqueId = IdUtils.Id();
                string sql = String.Format("SELECT * FROM Employee WHERE id_employee='{0}'", uniqueId);
                result = DbUtils.FindAll(sql);
            } while (result.Any());
            
            if (surname.Length is 0 or > 51)
            {
                return "Введіть прізвище довжиною < 51";
            }
            if (name.Length is 0 or > 51)
            {
                return "Введіть ім'я довжиною < 51";
            }
            if (patronymic.Length > 51)
            {
                return "Введіть по батькові довжиною < 51";
            }

            try
            {
                int findRole = Roles.roleKeys[role];
            } catch (KeyNotFoundException)
            {
                return "Нема такої ролі";
            }

            salary = salary.Replace(',', '.');
            try
            {
                float floatSalary = float.Parse(salary);
            } catch (FormatException)
            {
                return "Некоректно введена зарплата";
            }

            DateTime nowDate = DateTime.Now;
            DateTime birtDate;
            DateTime startDate;
            try
            {
                birtDate = Convert.ToDateTime(dateBirth);
                startDate = Convert.ToDateTime(dateStart);
            } catch
            {
                return "Дата некоректна";
            }

            int age = DateUtils.GetAge(birtDate, nowDate);
            if (age < 18)
            {
                return "Особа не досягла повноліття";
            }

            int ageAtStart = DateUtils.GetAge(birtDate, startDate);
            if (ageAtStart < 18)
            {
                return "Особа не досягла повноліття на момент початку роботи";
            }

            if (phoneNumber.Length is > 13 or < 10)
            {
                return "Номер телефу некоректний";
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

            if (password.Length is 0 or > 51)
            {
                return "Введіть пароль до 16 символів";
            }

            return uniqueId; //Alright
        }
    }
}
