using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Daenet.Common.Logging.EventHub;

namespace Daenet.Common.Logging.EventHub.UnitTests
{
    [TestClass]
    public class InitLoggerTest
    {

        private ILoggerFactory m_Factory;

        [TestInitialize()]
        public void Init()
        {
            var configuration = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile($"EventHubLoggerSettings.json")
                          .Build();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(builder =>
             {
                 builder
                     .AddConfiguration(configuration.GetSection("Logging"))
                     .AddEventHub(a => a.SetEventHubLoggerSettings(configuration.GetSection("Logging")));
             });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            m_Factory = serviceProvider.GetService<ILoggerFactory>();

        }

        [TestMethod()]
        public void TestConfiguration_Load()
        {
            var logger = m_Factory.CreateLogger<InitLoggerTest>();
            logger.LogError("Test Error"); // Calls Event hub
            logger.LogWarning("Test Warning"); // Calls Event hub
            logger.LogInformation("Test Information"); // Does not calls event hub
        }
    }
}
