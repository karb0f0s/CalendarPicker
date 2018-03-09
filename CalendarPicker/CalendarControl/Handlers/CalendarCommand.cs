using System;
using System.Threading.Tasks;
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
        public CalendarCommand() : base(name: "calendar")
        { }

        public override async Task<UpdateHandlingResult> HandleCommand(IBot bot, Update update, CalendarCommandArgs args)
        {
            var date = DateTime.Today;

            var calendarMarkup = Markup.Calendar(date, Constants.DateCulture);

            await bot.Client.SendTextMessageAsync(
                update.Message.Chat.Id,
                "Select date:",
                replyMarkup: calendarMarkup);

            return UpdateHandlingResult.Handled;
        }
    }
}
