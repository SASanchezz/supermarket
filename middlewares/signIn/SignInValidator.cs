using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.connections;
using supermarket.utils;

namespace supermarket.middlewares.signIn
{
    public static class SignInValidator
    {

        public static string validate(string phone, string password)
        {
            string sql = String.Format("SELECT * FROM Employee WHERE phone_number='{0}'", phone);
            List<string> result = DBUtils.FindAll(sql);
            if (!result.Any())
            {
                return "Нема такого телефону";
            }
            if (password.Length == 0 || !CryptUtils.compare(password, result[0].Split(',')[9]))
            {
                return "Неправильний пароль";
            }

            return ""; //Alright
        }
    }
}
