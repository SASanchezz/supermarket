using System;
using System.Linq;

namespace supermarket.Utils
{
    class IdUtils
    {
        private readonly static Random random = new();
        public static string Id(int length=8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
