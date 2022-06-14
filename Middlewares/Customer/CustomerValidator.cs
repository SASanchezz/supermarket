using System;
using Cust = supermarket.Models.Customer;

namespace supermarket.Middlewares.Customer
{
    public static class CustomerValidator
    {
        public static void ValidateInsert(string cardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            if (cardNumber.Length is > 13 or 0)
            {
                throw new Exception("Введіть номер карти < 13 символів");
            }
            
            try
            {
                int.Parse(cardNumber);
            } 
            catch
            {
                throw new Exception("Введіть корректний номер карти");
            }
            
            if (Cust.GetCustomerByCardNumber(cardNumber) != null)
            {
                throw new Exception("Клієнт з таким номером карти вже існує");
            }
            
            if (surname.Length is > 50 or 0)
            {
                throw new Exception("Введіть прізвище < 50 символів");
            }
            
            if (name.Length is > 50 or 0)
            {
                throw new Exception("Введіть ім'я < 50 символів");
            }
            
            if (patronymic.Length is > 50)
            {
                throw new Exception("Введіть по батькові < 50 символів");
            }

            if (phoneNumber.Length is > 13 or 0)
            {
                throw new Exception("Введіть номер телефону < 13 символів");
            }
            
            if (Cust.GetCustomerByPhone(phoneNumber) != null)
            {
                throw new Exception("Клієнт з таким номером телефону вже існує");
            }

            if (city.Length is > 50)
            {
                throw new Exception("Введіть місто < 50 символів");
            }
            
            if (street.Length is > 50)
            {
                throw new Exception("Введіть вулицю < 50 символів");
            }
            
            if (zipcode.Length is > 50)
            {
                throw new Exception("Введіть поштовий індекс < 13 символів");
            }

            try
            {
                double.Parse(percent);
            } 
            catch
            {
                throw new Exception("Введіть корректний процент");
            }
            
            if (double.Parse(percent) is > 100 or < 0)
            {
                throw new Exception("Введіть корректний процент");
            }
        }

        public static void ValidateUpdate(string oldCardNumber, string changedCardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            if (changedCardNumber.Length is > 13 or 0)
            {
                throw new Exception("Введіть номер карти < 13 символів");
            }
            
            try
            {
                int.Parse(changedCardNumber);
            }
            catch
            {
                throw new Exception("Введіть корректний номер карти");
            }
            
            if (oldCardNumber != changedCardNumber && Cust.GetCustomerByCardNumber(changedCardNumber) != null)
            {
                throw new Exception("Клієнт з таким номером карти вже існує");
            }
            
            if (surname.Length is > 50 or 0)
            {
                throw new Exception("Введіть прізвище < 50 символів");
            }
            
            if (name.Length is > 50 or 0)
            {
                throw new Exception("Введіть ім'я < 50 символів");
            }
            
            if (patronymic.Length is > 50)
            {
                throw new Exception("Введіть по батькові < 50 символів");
            }

            if (phoneNumber.Length is > 13 or 0)
            {
                throw new Exception("Введіть номер телефону < 13 символів");
            }
            
            if (Cust.GetCustomerByCardNumber(oldCardNumber)[Cust.phone_number] != phoneNumber 
                && Cust.GetCustomerByPhone(phoneNumber) != null)
            {
                throw new Exception("Клієнт з таким номером телефону вже існує");
            }

            if (city.Length is > 50)
            {
                throw new Exception("Введіть місто < 50 символів");
            }
            
            if (street.Length is > 50)
            {
                throw new Exception("Введіть вулицю < 50 символів");
            }
            
            if (zipcode.Length is > 50)
            {
                throw new Exception("Введіть поштовий індекс < 13 символів");
            }

            try
            {
                double.Parse(percent);
            }
            catch
            {
                throw new Exception("Введіть корректний процент");
            }
            
            if (double.Parse(percent) is > 100 or < 0)
            {
                throw new Exception("Введіть корректний процент");
            }
        }

    }
}
