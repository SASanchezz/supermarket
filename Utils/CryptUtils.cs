using BC = BCrypt.Net.BCrypt;



/*
* This class contains methods for hashing and checking passwords with BCrypt lib
*/
namespace supermarket.Utils
{
    public static class CryptUtils
    {
        public static string HashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        public static bool Compare(string plainText, string hashedPassword)
        {
            return BC.Verify(plainText, hashedPassword);
        }
    }
        
}
