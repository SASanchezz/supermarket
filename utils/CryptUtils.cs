using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace supermarket.utils
{
    public static class CryptUtils
    {
        public static string hashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        public static bool compare(string plainText, string hashedPassword)
        {
            return BC.Verify(plainText, hashedPassword);
        }
    }
        
}
