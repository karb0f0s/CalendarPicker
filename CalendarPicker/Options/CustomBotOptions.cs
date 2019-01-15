using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace CalendarPicker.Options
{
    public class CustomBotOptions<TBot> : BotOptions<TBot>
        where TBot : IBot
    {
        public string WebhookDomain { get; set; }
    }
}
