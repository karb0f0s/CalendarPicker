using CalendarPicker.CalendarControl.Handlers;
using CalendarPicker.Options;
using CalendarPicker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Framework;

namespace CalendarPicker.CalendarControl
{
    public static class CalendarControlExtension
    {
        public static IServiceCollection AddCalendarBot(this IServiceCollection services, IConfigurationSection botConfiguration) =>
            services.AddTransient<CalendarBot>()
                .Configure<BotOptions<CalendarBot>>(botConfiguration)
                .Configure<CustomBotOptions<CalendarBot>>(botConfiguration)
                .AddScoped<FaultedUpdateHandler>()
                .AddScoped<CalendarCommand>()
                .AddScoped<ChangeToHandler>()
                .AddScoped<PickDateHandler>()
                .AddScoped<YearMonthPickerHandler>()
                .AddScoped<MonthPickerHandler>()
                .AddScoped<YearPickerHandler>();

        public static IServiceCollection AddOperationServices(this IServiceCollection services) =>
            services
                .AddTransient<LocalizationService>();
    }
}
