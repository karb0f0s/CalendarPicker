using System;
using System.Threading.Tasks;
using CalendarPicker.CalendarControl.Services;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class CalendarCommandArgs : ICommandArgs
    {
        public string RawInput { get; set; }

        public string ArgsInput { get; set; }
    }

    public class CalendarCommand : CommandBase<CalendarCommandArgs>
    {
        private readonly LocalizationService _locale;

        public CalendarCommand(LocalizationService locale)
            : base(name: Constants.Command)
        {
            _locale = locale;
        }

        public override async Task<UpdateHandlingResult> HandleCommand(IBot bot, Update update, CalendarCommandArgs args)
        {
            var calendarMarkup = Markup.Calendar(DateTime.Today, _locale.DateCulture);

            await bot.Client.SendTextMessageAsync(
                update.Message.Chat.Id,
                "Pick date:",
                replyMarkup: calendarMarkup);

            return UpdateHandlingResult.Handled;
        }
    }
}
