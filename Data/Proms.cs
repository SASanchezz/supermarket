using System.Collections.Generic;

/*
 * This class contains employee roles and its corresponding number in database
 */

namespace supermarket.Data
{
    public static class Proms
    {
        public static readonly string[] promNames = new string[2]
        {
            "Неакційні", "Акційні"
        };

        public static readonly Dictionary<string, int> promKeys = new()
        {
            { "Неакційні", 0 },
            { "Акційні", 1 },
        };
    }
}
