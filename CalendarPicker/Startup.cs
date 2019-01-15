using System;
using System.Threading.Tasks;
using CalendarPicker.CalendarControl;
using CalendarPicker.CalendarControl.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Framework;
using Telegram.Bot.Framework.Abstractions;

namespace CalendarPicker
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        private IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCalendarBot(_configuration.GetSection("CalendarBot"));

            // Add configuration
            services.AddSingleton(
                _configuration.GetSection("CalendarBot").Get<CalendarBotConfiguration>()
            );

            services.AddOperationServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseTelegramBotLongPolling<CalendarBot>(ConfigureBot(), startAfter: TimeSpan.FromSeconds(2));
            }
            else
            {
                app.UseTelegramBotWebhook<CalendarBot>(ConfigureBot());
                app.EnsureWebhookSet<CalendarBot>();
            }

            app.Run(async context => await context.Response.WriteAsync("Hello World!"));
        }

        private IBotBuilder ConfigureBot() =>
            new BotBuilder()
                .Use<FaultedUpdateHandler>()
                .UseCommand<CalendarCommand>("calendar")
                .UseWhen<ChangeToHandler>(ChangeToHandler.CanHandle)
                .UseWhen<PickDateHandler>(PickDateHandler.CanHandle)
                .UseWhen<YearMonthPickerHandler>(YearMonthPickerHandler.CanHandle)
                .UseWhen<MonthPickerHandler>(MonthPickerHandler.CanHandle)
                .UseWhen<YearPickerHandler>(YearPickerHandler.CanHandle)
            ;
    }
}
