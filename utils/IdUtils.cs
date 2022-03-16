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

        public static string IdToName(string thatId)
        {
            return 'i' + thatId;
        }

        public static string NameToId(string thatId)
        {
            return thatId[1..]; //removes first char
        }

        public static string Compound(string first, string second)
        {
            return first + " - " + second;
        }

        public static string[] Decompound(string word)
        {
            try
            {
                return new string[] { word.Split(" - ")[0], word.Split(" - ")[1] };
            } catch (IndexOutOfRangeException)
            {
                return new string[] { "", "-1" };
            }
        }
    }
}
