using System.Globalization;

namespace CalendarPicker.CalendarControl
{
    public static class Constants
    {
        public static DateTimeFormatInfo DateCulture = new CultureInfo("en-US", false).DateTimeFormat;

        public const string ChangeTo = "cng-to/";

        public const string PickDate = "pck/";

        public const string DateFormat = @"yyyy\/MM\/dd";
    }
}
