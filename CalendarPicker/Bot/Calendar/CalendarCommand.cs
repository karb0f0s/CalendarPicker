using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Framework;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CalendarPicker.Bot
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
            var dtfi = new CultureInfo("es-ES", false).DateTimeFormat;

            var replyMarkup = CalculatorMarkup(date, dtfi);

            await bot.Client.SendTextMessageAsync(
                update.Message.Chat.Id,
                "Select date:",
                replyMarkup: replyMarkup);

            return UpdateHandlingResult.Handled;
        }

        private static InlineKeyboardMarkup CalculatorMarkup(DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();
            keyboardRows.Add(DateRow(date, dtfi));
            keyboardRows.Add(DayOfWeekRow(dtfi));
            keyboardRows.AddRange(DaysOfMonth(date, dtfi));
            keyboardRows.Add(ControlButtons());

            return new InlineKeyboardMarkup(keyboardRows);
        }

        private static IEnumerable<InlineKeyboardButton> DateRow(DateTime date, DateTimeFormatInfo dtfi) =>
             new InlineKeyboardButton[] { date.ToString("Y", dtfi) };

        private static IEnumerable<InlineKeyboardButton> DayOfWeekRow(DateTimeFormatInfo dtfi)
        {
            var dayNames = new InlineKeyboardButton[7];

            var firstDayOfWeek = (int)dtfi.FirstDayOfWeek;
            for (int i = 0; i < 7; i++)
            {
                yield return dtfi.AbbreviatedDayNames[(firstDayOfWeek + i) % 7];
            }

            //return dayNames;
        }

        //private static List<IEnumerable<InlineKeyboardButton>> DaysOfMonth(DateTime date, DateTimeFormatInfo dtfi)
        private static IEnumerable<IEnumerable<InlineKeyboardButton>> DaysOfMonth(DateTime date, DateTimeFormatInfo dtfi)
        {
            //var days = new List<IEnumerable<InlineKeyboardButton>>();

            var firstDay = new DateTime(date.Year, date.Month, 1);
            var lastDay = firstDay.AddMonths(1).AddDays(-1);

            for (int dayOfMonth = 1, weekNum = 0; dayOfMonth <= lastDay.Day; weekNum++)
            {

                //days.Add(NewWeek(weekNum, ref dayOfMonth));
                yield return NewWeek(weekNum, ref dayOfMonth);
            }
            //return days;

            IEnumerable<InlineKeyboardButton> NewWeek(int weekNum, ref int dayOfMonth)
            {
                var week = new InlineKeyboardButton[7];
                var fdow = ((int)firstDay.DayOfWeek - (int)dtfi.FirstDayOfWeek) % 7;

                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    if ((weekNum == 0 && dayOfWeek < fdow) ||
                        dayOfMonth > lastDay.Day)
                    {
                        week[dayOfWeek] = " ";
                        continue;
                    }

                    week[dayOfWeek] = dayOfMonth.ToString();
                    dayOfMonth++;
                }
                return week;
            }
        }

        private static IEnumerable<InlineKeyboardButton> ControlButtons() =>
            new InlineKeyboardButton[] {
                "<", " ", ">"
            };
    }
}
