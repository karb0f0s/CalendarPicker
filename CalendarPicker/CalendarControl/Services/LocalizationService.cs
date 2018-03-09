using System.Globalization;

namespace CalendarPicker.CalendarControl.Services
{
    public class LocalizationService
    {
        private readonly CalendarBotConfiguration _configuration;

        public DateTimeFormatInfo DateCulture;

        public LocalizationService(CalendarBotConfiguration configuration)
        {
            _configuration = configuration;

            DateCulture = configuration.BotLocale == null
                ? new CultureInfo("en-US", false).DateTimeFormat
                : new CultureInfo(configuration.BotLocale, false).DateTimeFormat;
        }
    }
}
