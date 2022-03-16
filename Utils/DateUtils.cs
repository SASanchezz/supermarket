using System;
/*
* This class contains methods for comparing dates
*/
namespace supermarket.Utils
{
    static class DateUtils
    {
        /*
         * Get age between start and end dates
         */
        public static int GetAge(DateTime start, DateTime end)
        {
            int birthYearsPassed = end.Year - start.Year;
            if (DateTime.Now.Month < start.Month || (end.Month == start.Month && end.Day < start.Day))
            {
                --birthYearsPassed;
            }
            return birthYearsPassed;
        }

        /*
         * Get days amount between start and end dates (can be negative and this's okay)
         */
        static public int GetDays(DateTime start, DateTime end)
        {
            return (end - start).Days;
        }

    }
}
