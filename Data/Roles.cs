using System.Collections.Generic;

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
            { "Менеджер", 0 },
            { "Касир", 1 }
        };
    }
}
