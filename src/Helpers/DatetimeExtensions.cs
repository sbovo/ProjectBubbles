namespace System
{
    public static class DatetimeExtensions
    {
        public static string GetUNIVERSALString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
