using System;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace CalendarPicker.CalendarControl
{
    public static class Markup
    {
        public static InlineKeyboardMarkup Calendar(DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(Row.Date(date, dtfi));
            keyboardRows.Add(Row.DayOfWeek(dtfi));
            keyboardRows.AddRange(Row.Month(date, dtfi));
            keyboardRows.Add(Row.Controls(date));

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}
