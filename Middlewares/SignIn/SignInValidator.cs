using System;
using System.Collections.Generic;
using System.Linq;
using supermarket.Utils;

/*
 * This class validates data for signing in
 */

namespace supermarket.Middlewares.SignIn
{
    public static class SignInValidator
    {
        public static void Validate(string phone, string password)
        {
            string sql = String.Format("SELECT * FROM Employee WHERE phone_number='{0}'", phone);
            List<string[]> result = DbUtils.FindAll(sql);

            if (!result.Any())
            {
                throw new Exception("Нема такого телефону");
            }
            if (password.Length == 0 || !CryptUtils.Compare(password, result[0][9]))
            {
                throw new Exception("Неправильний пароль");
            }
        }
    }
}