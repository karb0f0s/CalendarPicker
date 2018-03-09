using System;
using System.Globalization;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class PickDateHandler : IUpdateHandler
    {
        public bool CanHandleUpdate(IBot bot, Update update)
        {
            return
                update.Type == UpdateType.CallbackQuery &&
                update.CallbackQuery.Data.StartsWith(Constants.PickDate, StringComparison.Ordinal);
        }

        public async Task<UpdateHandlingResult> HandleUpdateAsync(IBot bot, Update update)
        {
            if (!DateTime.TryParseExact(
                update.CallbackQuery.Data.Replace(Constants.PickDate, string.Empty),
                Constants.DateFormat,
                null,
                DateTimeStyles.None,
                out var date))
                return UpdateHandlingResult.Handled;


            await bot.Client.SendTextMessageAsync(
                update.CallbackQuery.Message.Chat.Id,
                date.ToString("d", Constants.DateCulture));

            return UpdateHandlingResult.Handled;
        }
    }
}
