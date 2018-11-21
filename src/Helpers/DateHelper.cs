using System;

namespace Helpers
{
    public static class DateHelper
    {
        public static string GetUNIVERSALStringFromDate(DateTime date)
        {
            string year = date.Year.ToString("d4");
            string month = date.Month.ToString("d2");
            string day = date.Day.ToString("d2");

            return $"{year}-{month}-{day}";
        }
    }
}
