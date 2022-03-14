using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.connections;
using supermarket.utils;
using supermarket.interfaces;

namespace supermarket.middlewares.signUp
{
    public static class SignUpValidator
    {
        public static string Validate(string surname, string name, string patronymic, string role,
            string salary, string dateBirth, string dateStart, string phoneNumber,
            string city, string street, string zipcode, string password)
        {
            string uniqueId;
            List<string> result;
            do //to avoid similar id's
            {
                uniqueId = IdUtils.Id();
                string sql = String.Format("SELECT * FROM Employee WHERE id_employee='{0}'", uniqueId);
                result = DBUtils.FindAll(sql);
            } while (result.Any());
            
            if (surname.Length == 0 || surname.Length > 51)
            {
                return "Введіть фамілію довжиною < 51";
            }
            if (name.Length == 0 || name.Length > 51)
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
            } catch (KeyNotFoundException e)
            {
                return "Нема такої ролі";
            }

            salary = salary.Replace(',', '.');
            try
            {
                float floatSalary = float.Parse(salary);
            } catch (FormatException e)
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

            int daysToStart = DateUtils.GetDays(startDate, nowDate); // let it be

            if (phoneNumber.Length > 13 || phoneNumber.Length < 10)
            {
                return "Номер телефу некоректний";
            }
            if (city.Length == 0 || city.Length > 51)
            {
                return "Введіть місто довжиною < 51";
            }
            if (street.Length == 0 || street.Length > 51)
            {
                return "Введіть вулицю довжиною < 51";
            }
            if (zipcode.Length == 0 || zipcode.Length > 9)
            {
                return "Введіть поштовий індекс довжиною < 10";
            }

            if (password.Length == 0 || password.Length > 51)
            {
                return "Введіть пароль до 16 символів";
            }

            return uniqueId; //Alright
        }
    }
}
