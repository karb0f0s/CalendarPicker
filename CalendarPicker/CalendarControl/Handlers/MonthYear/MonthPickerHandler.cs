using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CalendarPicker.Services;
using Telegram.Bot.Framework.Abstractions;
using Telegram.Bot.Types.Enums;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class MonthPickerHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;

        public MonthPickerHandler(LocalizationService locale)
        {
            _locale = locale;
        }

        public static bool CanHandle(IUpdateContext context)
        {
            return
                context.Update.Type == UpdateType.CallbackQuery
                &&
                context.Update.IsCallbackCommand(Constants.PickMonth);
        }

        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            if (!DateTime.TryParseExact(
                    context.Update.TrimCallbackCommand(Constants.PickMonth),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date)
            )
            {
                return;
            }

            var monthPickerMarkup = Markup.PickMonth(date, _locale.DateCulture);

            await context.Bot.Client.EditMessageReplyMarkupAsync(
                context.Update.CallbackQuery.Message.Chat.Id,
                context.Update.CallbackQuery.Message.MessageId,
                replyMarkup: monthPickerMarkup
            );
        }
    }
}
