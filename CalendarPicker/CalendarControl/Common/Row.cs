using System;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace CalendarPicker.CalendarControl
{
    public static class Row
    {
        public static IEnumerable<InlineKeyboardButton> Date(DateTime date, DateTimeFormatInfo dtfi) =>
             new InlineKeyboardButton[] { date.ToString("Y", dtfi) };

        public static IEnumerable<InlineKeyboardButton> DayOfWeek(DateTimeFormatInfo dtfi)
        {
            var dayNames = new InlineKeyboardButton[7];

            var firstDayOfWeek = (int)dtfi.FirstDayOfWeek;
            for (int i = 0; i < 7; i++)
            {
                yield return dtfi.AbbreviatedDayNames[(firstDayOfWeek + i) % 7];
            }
        }

        public static IEnumerable<IEnumerable<InlineKeyboardButton>> Month(DateTime date, DateTimeFormatInfo dtfi)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1).Day;

            for (int dayOfMonth = 1, weekNum = 0; dayOfMonth <= lastDayOfMonth; weekNum++)
            {
                yield return NewWeek(weekNum, ref dayOfMonth);
            }

            IEnumerable<InlineKeyboardButton> NewWeek(int weekNum, ref int dayOfMonth)
            {
                var week = new InlineKeyboardButton[7];
                var firstDayOfWeek = ((int)firstDayOfMonth.DayOfWeek - (int)dtfi.FirstDayOfWeek) % 7;

                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    if ((weekNum == 0 && dayOfWeek < firstDayOfWeek) ||
                        dayOfMonth > lastDayOfMonth)
                    {
                        week[dayOfWeek] = " ";
                        continue;
                    }

                    week[dayOfWeek] = InlineKeyboardButton.WithCallbackData(
                        dayOfMonth.ToString(),
                        $"{Constants.PickDate}{date.ToString(Constants.DateFormat)}");

                    dayOfMonth++;
                }
                return week;
            }
        }

        public static IEnumerable<InlineKeyboardButton> Controls(DateTime date)
        {
            var previousMonth = date.AddMonths(-1);
            var nextMonth = date.AddMonths(1);

            return new InlineKeyboardButton[] {
                InlineKeyboardButton.WithCallbackData(
                    "<",
                    $"{Constants.ChangeTo}{previousMonth.ToString(Constants.DateFormat)}"),
                " ",
                InlineKeyboardButton.WithCallbackData(
                    ">",
                    $"{Constants.ChangeTo}{nextMonth.ToString(Constants.DateFormat)}"),
            };
        }
    }
}
