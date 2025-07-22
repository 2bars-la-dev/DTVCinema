namespace Utility
{
    public static class TimeHelper
    {
        public static DateTime GetVietnamTime()
        {
            string timezoneId = GetTimeZoneId();
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        }

        private static string GetTimeZoneId()
        {
            // Windows dùng ID khác với Unix (Linux, macOS)
            if (OperatingSystem.IsWindows())
                return "SE Asia Standard Time";
            else
                return "Asia/Ho_Chi_Minh";
        }
    }
}

