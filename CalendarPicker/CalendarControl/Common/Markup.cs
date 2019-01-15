using System;
using System.Collections.Generic;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace CalendarPicker.CalendarControl
{
    public static class Markup
    {
        public static InlineKeyboardMarkup Calendar(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new List<IEnumerable<InlineKeyboardButton>>();

            keyboardRows.Add(Row.Date(date, dtfi));
            keyboardRows.Add(Row.DayOfWeek(dtfi));
            keyboardRows.AddRange(Row.Month(date, dtfi));
            keyboardRows.Add(Row.Controls(date));

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickMonthYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[][]
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        date.ToString("MMMM", dtfi),
                        $"{Constants.PickMonth}{date.ToString(Constants.DateFormat)}"
                    ),
                    InlineKeyboardButton.WithCallbackData(
                        date.ToString("yyyy", dtfi),
                        $"{Constants.PickYear}{date.ToString(Constants.DateFormat)}"
                    )
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        "<<",
                        $"{Constants.ChangeTo}{date.ToString(Constants.DateFormat)}"
                    ),
                    " "
                }
            };

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickMonth(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            for (int month = 0, row = 0; month < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, month++)
                {
                    var day = new DateTime(date.Year, month + 1, 1);

                    keyboardRow[j] = InlineKeyboardButton.WithCallbackData(
                        dtfi.MonthNames[month],
                        $"{Constants.YearMonthPicker}{day.ToString(Constants.DateFormat)}"
                    );
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }

        public static InlineKeyboardMarkup PickYear(in DateTime date, DateTimeFormatInfo dtfi)
        {
            var keyboardRows = new InlineKeyboardButton[5][];

            var startYear = date.AddYears(-7);

            for (int i = 0, row = 0; i < 12; row++)
            {
                var keyboardRow = new InlineKeyboardButton[3];
                for (var j = 0; j < 3; j++, i++)
                {
                    var day = startYear.AddYears(i);

                    keyboardRow[j] = InlineKeyboardButton.WithCallbackData(
                        day.ToString("yyyy", dtfi),
                        $"{Constants.YearMonthPicker}{day.ToString(Constants.DateFormat)}"
                    );
                }

                keyboardRows[row] = keyboardRow;
            }
            keyboardRows[4] = Row.BackToMonthYearPicker(date);

            return new InlineKeyboardMarkup(keyboardRows);
        }
    }
}
