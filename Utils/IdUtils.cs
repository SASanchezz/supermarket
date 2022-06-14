using System;
using System.Linq;
/*
 * This class contains methods for working with ids in project
 */


namespace supermarket.Utils
{
    internal class IdUtils
    {
        private readonly static Random random = new();

        /*
        * This method generates unique id with specific length
        */
        public static string Id(int length=8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ReceiptId(int length = 9)
        {
            return '#' + RandomNumbers(length);
        }

        public static string RandomNumbers(int length = 9)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /*
        * This method adds 'i' before id so WPF doesn't swear on x:Name 
        */
        public static string IdToName(string thatId)
        {
            return 'i' + thatId;
        }

        /*
        * This method removes 'i' before id
        */
        public static string NameToId(string thatId)
        {
            return thatId[1..]; //removes first char.
        }

        /*
        * This method concatinates 2 words via dash, so we can write them in some lists.
        */
        public static string Compound(string first, string second)
        {
            return first + " - " + second;
        }

        /*
        * This method takes compounded word and returns string array with both words
        *   OR
        * ["", "-1"] if there is no " - " in string.
        */
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
