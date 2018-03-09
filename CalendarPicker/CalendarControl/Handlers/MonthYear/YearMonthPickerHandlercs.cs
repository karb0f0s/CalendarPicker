using System;
using System.Globalization;
using System.Threading.Tasks;
using CalendarPicker.CalendarControl.Services;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class YearMonthPickerHandler : IUpdateHandler
    {
        private readonly LocalizationService _locale;

        public YearMonthPickerHandler(LocalizationService locale)
        {
            _locale = locale;
        }

        public bool CanHandleUpdate(IBot bot, Update update)
        {
            return
                update.Type == UpdateType.CallbackQuery &&
                update.IsCallbackCommand(Constants.YearMonthPicker);
        }

        public async Task<UpdateHandlingResult> HandleUpdateAsync(IBot bot, Update update)
        {
            if (!DateTime.TryParseExact(
                    update.TrimCallbackCommand(Constants.YearMonthPicker),
                    Constants.DateFormat,
                    null,
                    DateTimeStyles.None,
                    out var date))
                return UpdateHandlingResult.Handled;

            var monthYearMarkup = Markup.PickMonthYear(date, _locale.DateCulture);

            await bot.Client.EditMessageReplyMarkupAsync(
                update.CallbackQuery.Message.Chat.Id,
                update.CallbackQuery.Message.MessageId,
                replyMarkup: monthYearMarkup);

            return UpdateHandlingResult.Handled;
        }
    }
}
