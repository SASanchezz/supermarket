using System.Linq;
using Cust = supermarket.Models.Customer;


namespace supermarket.Middlewares.Customer
{
    class CustomerValidator
    {
        public static string ValidateInsert(string cardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {
            
            if (cardNumber.Length is > 13 or 0)
            {
                return "Введіть номер карти < 13 символів";
            }
            try
            {
                int.Parse(cardNumber);
            } catch
            {
                return "Введіть корректний номер карти";
            }
            if (Cust.GetCustomerByCardNumber(cardNumber) != null)
            {
                return "Клієнт з таким номером карти вже існує";
            }
            if (surname.Length is > 50 or 0)
            {
                return "Введіть прізвище < 50 символів";
            }
            if (name.Length is > 50 or 0)
            {
                return "Введіть ім'я < 50 символів";
            }
            if (patronymic.Length is > 50)
            {
                return "Введіть по батькові < 50 символів";
            }

            if (phoneNumber.Length is > 13 or 0)
            {
                return "Введіть номер телефону < 13 символів";
            }
            if (Cust.GetCustomerByPhone(phoneNumber) != null)
            {
                return "Клієнт з таким номером телефону вже існує";
            }

            if (city.Length is > 50)
            {
                return "Введіть місто < 50 символів";
            }
            if (street.Length is > 50)
            {
                return "Введіть вулицю < 50 символів";
            }
            if (zipcode.Length is > 50)
            {
                return "Введіть поштовий індекс < 13 символів";
            }

            try
            {
                double.Parse(percent);
            } catch
            {
                return "Введіть корректний процент";
            }
            if (double.Parse(percent) is > 100 or < 0)
            {
                return "Введіть корректний процент";
            }

            return ""; //Alright
        }

        public static string ValidateUpdate(string oldCardNumber, string changedCardNumber, string surname, string name, string patronymic,
            string phoneNumber, string city, string street, string zipcode, string percent)
        {

            if (changedCardNumber.Length is > 13 or 0)
            {
                return "Введіть номер карти < 13 символів";
            }
            try
            {
                int.Parse(changedCardNumber);
            }
            catch
            {
                return "Введіть корректний номер карти";
            }
            if (oldCardNumber != changedCardNumber && Cust.GetCustomerByCardNumber(changedCardNumber).Any())
            {
                return "Клієнт з таким номером карти вже існує";
            }
            if (surname.Length is > 50 or 0)
            {
                return "Введіть прізвище < 50 символів";
            }
            if (name.Length is > 50 or 0)
            {
                return "Введіть ім'я < 50 символів";
            }
            if (patronymic.Length is > 50)
            {
                return "Введіть по батькові < 50 символів";
            }

            if (phoneNumber.Length is > 13 or 0)
            {
                return "Введіть номер телефону < 13 символів";
            }
            if (Cust.GetCustomerByCardNumber(oldCardNumber)[Cust.phone_number] != phoneNumber 
                && Cust.GetCustomerByPhone(phoneNumber).Any())
            {
                return "Клієнт з таким номером телефону вже існує";
            }

            if (city.Length is > 50)
            {
                return "Введіть місто < 50 символів";
            }
            if (street.Length is > 50)
            {
                return "Введіть вулицю < 50 символів";
            }
            if (zipcode.Length is > 50)
            {
                return "Введіть поштовий індекс < 13 символів";
            }

            try
            {
                double.Parse(percent);
            }
            catch
            {
                return "Введіть корректний процент";
            }
            if (double.Parse(percent) is > 100 or < 0)
            {
                return "Введіть корректний процент";
            }

            return ""; //Alright
        }

    }
}
