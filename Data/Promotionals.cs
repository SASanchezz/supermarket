using System.Collections.Generic;

namespace supermarket.Data
{
    internal class Promotionals
    {
        public static readonly string[] promotionalNames = new string[2]
        {
            "Неакційний", "Акційний"
        };

        public static readonly Dictionary<string, int> promotionalKeys = new()
        {
            { "Неакційний", 0 },
            { "Акційний", 1 }
        };
    }
}
