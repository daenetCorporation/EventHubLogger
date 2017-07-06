# EventHubLogger
Implementation of .netcore ILogger for Azure Event Hub

### Configuration

**Following code block**, shows how to add EventHubLogger provider to the loggerFactory in EventHubLoggerUnitTests class:
```C#
  private void initializeEventHubLogger(Func<string, LogLevel, bool> filter,
              Func<LogLevel, EventId, object, Exception, EventData> eventDataFormatter = null,
           Dictionary<string, object> additionalValues = null)
        {
            ConfigurationBuilder cfgBuilder = new ConfigurationBuilder();
            cfgBuilder.AddJsonFile(@"EventHubLoggerSettings.json");
            var configRoot = cfgBuilder.Build();

            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddEventHub(configRoot.GetEventHubLoggerSettings(), filter, eventDataFormatter, additionalValues);

            m_Logger = loggerFactory.CreateLogger<EventHubLoggerUnitTests>();
        }
 ```
   Following configuration needs to be added in the ***EventHubLogger>settings.json*** file.
   ```JSON
   {
	"IncludeScopes": true,
  "Switches": {
    "Daenet.Common.EventHubLogger.UnitTests": 0
  },
  "EventHub": {
    "ConnectionString": "EventHub Connection String",
    "IncludeExceptionStackTrace": false,
    "RetryPolicy": 0
  }
}
```
