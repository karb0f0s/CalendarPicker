using System;
using System.Threading;
using System.Threading.Tasks;
using CalendarPicker.Services;
using Telegram.Bot.Framework.Abstractions;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class CalendarCommand : CommandBase
    {
        private readonly LocalizationService _locale;

        public CalendarCommand(LocalizationService locale)
        {
            _locale = locale;
        }

        public override async Task HandleAsync(
            IUpdateContext context,
            UpdateDelegate next,
            string[] args,
            CancellationToken cancellationToken
        )
        {
            var calendarMarkup = Markup.Calendar(DateTime.Today, _locale.DateCulture);

            await context.Bot.Client.SendTextMessageAsync(
                context.Update.Message.Chat.Id,
                "Pick date:",
                replyMarkup: calendarMarkup
            );
        }
    }
}
