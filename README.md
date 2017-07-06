# EventHubLogger
Implementation of .netcore ILogger for Azure Event Hub

### Installation 
**Install** the Daenet.Common.Logging.EventHub [NuGet Package](http:// "Link missing") in your application.

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
        Following configuration needs to be added in the ***EventHubLoggerSettings.json*** file. 
