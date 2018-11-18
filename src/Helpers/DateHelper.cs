using System;

namespace Helpers
{
    public static class DateHelper
    {
        public static string GetUNIVERSALStringFromDate(DateTime date)
        {
            string year = DateTime.Now.Year.ToString("d4");
            string month = DateTime.Now.Month.ToString("d2");
            string day = DateTime.Now.Day.ToString("d2");

            return $"{year}-{month}-{day}";
        }
    }
}
