using System.Collections.Generic;

namespace supermarket.Data
{
    static class Roles
    {
        public static string[] roleNames = new string[2]
        {
            "Менеджер", "Касир"
        };
        

        public static Dictionary<string, int> roleKeys = new()
        {
            { "Менеджер", 0 },
            { "Касир", 1 }
        };
    }
}
