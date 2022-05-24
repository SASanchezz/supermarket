using System.Collections.Generic;

/*
 * This class contains employee roles and its corresponding number in database
 */

namespace supermarket.Data
{
    public static class Roles
    {
        public static readonly string[] roleNames = new string[2]
        {
            "Менеджер", "Касир"
        };

        public static readonly Dictionary<string, int> roleKeys = new()
        {
            { "Менеджер", 1 },
            { "Касир", 2 }
        };
    }
}
