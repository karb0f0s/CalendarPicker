using CalendarPicker.CalendarControl.Handlers;
using CalendarPicker.CalendarControl.Services;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Framework;
using static Microsoft.Extensions.DependencyInjection.TelegramBotFrameworkIServiceCollectionExtensions;

namespace CalendarPicker.CalendarControl
{
    public static class CalendarControlExtension
    {
        public static ITelegramBotFrameworkBuilder<TBot> AddCalendarHandlers<TBot>(this ITelegramBotFrameworkBuilder<TBot> botBuilder)
            where TBot : BotBase<TBot> =>
            botBuilder
                .AddUpdateHandler<CalendarCommand>()
                .AddUpdateHandler<ChangeToHandler>()
                .AddUpdateHandler<PickDateHandler>()
                .AddUpdateHandler<YearMonthPickerHandler>()
                .AddUpdateHandler<MonthPickerHandler>()
                .AddUpdateHandler<YearPickerHandler>();

        public static IServiceCollection AddCalendarControlServices(this IServiceCollection services) =>
            services
                .AddTransient<LocalizationService>();
    }
}
