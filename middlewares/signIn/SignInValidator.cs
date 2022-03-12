using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using supermarket.connections;

namespace supermarket.middlewares.signIn
{
    public static class SignInValidator
    {

        public static string validate(string phone, string password)
        {
            string sql = String.Format("SELECT * FROM Employee WHERE phone_number='{0}' AND password='{1}'", phone, password);
            List<string> result = DBUtils.FindAll(sql);
            if (!result.Any())
            {
                return "Нема такого телефону або неправильний пароль";
            }

            return ""; //Alright
        }
    }
}
