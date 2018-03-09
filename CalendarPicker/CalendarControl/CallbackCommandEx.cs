using System;
using Telegram.Bot.Types;

namespace CalendarPicker.CalendarControl
{
    public static class CallbackCommandEx
    {
        public static bool IsCallbackCommand(this Update update, string command) =>
            update.CallbackQuery.Data.StartsWith(
                command,
                StringComparison.Ordinal);

        public static string TrimCallbackCommand(this Update update, string pattern) =>
            update.CallbackQuery.Data.Replace(pattern, string.Empty);
    }
}
