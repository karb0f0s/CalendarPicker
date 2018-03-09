using System;
using System.Globalization;
using System.Threading.Tasks;
using CalendarPicker.CalendarControl.Services;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class YearPickerHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;

        public YearPickerHandler(LocalizationService locale)
        {
            _locale = locale;
        }

        public bool CanHandleUpdate(IBot bot, Update update)
        {
            return
                update.Type == UpdateType.CallbackQuery &&
                update.IsCallbackCommand(Constants.PickYear);
        }

        public async Task<UpdateHandlingResult> HandleUpdateAsync(IBot bot, Update update)
        {
            if (!DateTime.TryParseExact(
                    update.TrimCallbackCommand(Constants.PickYear),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date))
                return UpdateHandlingResult.Handled;

            var monthPickerMarkup = Markup.PickYear(date, _locale.DateCulture);

            await bot.Client.EditMessageReplyMarkupAsync(
                update.CallbackQuery.Message.Chat.Id,
                update.CallbackQuery.Message.MessageId,
                replyMarkup: monthPickerMarkup);

            return UpdateHandlingResult.Handled;
        }
    }
}
