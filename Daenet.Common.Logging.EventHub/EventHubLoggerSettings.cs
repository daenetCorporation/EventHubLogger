﻿using System.Collections.Generic;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;

namespace Daenet.Common.Logging.EventHub
{
    public class EventHubLoggerSettings : IEventHubLoggerSettings
    {
        public IDictionary<string, LogLevel> Switches { get; set; } = new Dictionary<string, LogLevel>();

        public string ConnectionString { get; set; }

        public bool IncludeScopes { get; set; }

        public bool IncludeExceptionStackTrace { get; set; }

        public RetryPolicy RetryPolicy { get ; set; }

        public bool TryGetSwitch(string name, out LogLevel level)
        {
            return Switches.TryGetValue(name, out level);
        }
    }
}
