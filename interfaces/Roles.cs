using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.interfaces
{
    static class Roles
    {
        public static Dictionary<int, string> roleNames = new()
        {
            { 0, "Менеджер" },
            { 1, "Касир" }
        };

        public static Dictionary<string, int> roleKeys = new()
        {
            { "Менеджер", 0 },
            { "Касир", 1 }
        };
    }
}
