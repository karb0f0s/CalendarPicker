using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot.Framework;

namespace CalendarPicker
{
    public class CalendarBot : BotBase
    {
        private readonly ILogger<CalendarBot> _logger;

        public CalendarBot(IOptions<BotOptions<CalendarBot>> botOptions, ILogger<CalendarBot> logger)
            : base(botOptions.Value)
        {
            _logger = logger;
        }
    }
}
