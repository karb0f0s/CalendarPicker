# Telegram Bot with Calendar Picker
## About
Simple Telegram Bot with Calendar Picker control. Based on [Telegram.Bot.Framework](https://github.com/TelegramBots/Telegram.Bot.Framework)

## Configuration
Just modify appsettings.json and run. 
The only mandatory parameter is `ApiToken`. 
Default `BotLocale` is `"en-US"`, but you can play with [other options](https://msdn.microsoft.com/en-us/library/ee825488(v=cs.20).aspx):
```javascript
{
  "CalendarBot": {
    "ApiToken": "{your-bots-api-token}",
    "BotUserName": "{your-bots-username}",
    "PathToCertificate": "",
    "WebhookUrl": "https://example.com/bots/{bot}/webhook/{token}",

    "BotLocale": "es-ES"
  },
}
```

## Screenshots
![](screenshot/markup_preview.png)
