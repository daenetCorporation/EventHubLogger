# EventHubLogger
Implementation of .netcore ILogger for Azure Event Hub

## Installation

**First**, install the Daenet.Common.Logging.EventHub [NuGet Package](https://www.nuget.org/packages/Daenet.Common.Logging.EventHub) into your application.

```C#
Install-Package Daenet.Common.Logging.EventHub
```
## Usage

**Following code block**, shows how to add EventHubLogger provider to the loggerFactory in EventHubLoggerUnitTests class:
```C#
  private void initializeEventHubLogger(Func<string, LogLevel, bool> filter,
              Func<LogLevel, EventId, object, Exception, EventData> eventDataFormatter = null,
           Dictionary<string, object> additionalValues = null)
        {
            ConfigurationBuilder cfgBuilder = new ConfigurationBuilder();
            cfgBuilder.AddJsonFile("EventHubLoggerSettings.json");
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
Your log output would look like:
 ```JSON
  "Name": "Daenet.Common.EventHubLogger.UnitTests.EventHubLoggerUnitTests",
      "Scope": null,
      "EventId": "0",
      "Message": "Test Warning Log Message",
      "Level": 3,
      "LocalEnqueuedTime": "2017-07-04T17:32:46.3321255+02:00",
      "Exception": null,
      "{OriginalFormat}": "Test Warning Log Message"
 ```

