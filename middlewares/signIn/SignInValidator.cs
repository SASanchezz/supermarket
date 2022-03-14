using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.Connections;
using supermarket.Utils;

namespace supermarket.Middlewares.SignIn
{
    public static class SignInValidator
    {

        public static string Validate(string phone, string password)
        {
            string sql = String.Format("SELECT * FROM Employee WHERE phone_number='{0}'", phone);
            List<string> result = DBUtils.FindAll(sql);
            if (!result.Any())
            {
                return "Нема такого телефону";
            }
            if (password.Length == 0 || !CryptUtils.Compare(password, result[0].Split(',')[9]))
            {
                return "Неправильний пароль";
            }

            return ""; //Alright
        }
    }
}
