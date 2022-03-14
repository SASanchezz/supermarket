using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supermarket.utils
{
    static class DateUtils
    {
        public static int GetAge(DateTime start, DateTime end)
        {
            int birthYearsPassed = end.Year - start.Year;
            if (DateTime.Now.Month < start.Month || (end.Month == start.Month && end.Day < start.Day))
            {
                --birthYearsPassed;
            }
            return birthYearsPassed;
        }
        static public int GetDays(DateTime start, DateTime end)
        {
            return (end - start).Days;
        }

    }
}
