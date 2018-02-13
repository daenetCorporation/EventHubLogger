using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Daenet.Common.Logging.EventHub;
using System.Threading.Tasks;
using System.Net.Http;

namespace Daenet.Common.Logging.EventHub.UnitTests
{
    [TestClass]
    public class AsyncTest
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
        public async Task AsyncTest1()
        {
            var logger = m_Factory.CreateLogger(nameof(AsyncTest1));
            using (var scope = logger.BeginScope("Scope Base"))
            {
                logger.LogError("Test Error"); // Calls Event hub
                await Call_Async();

            }
        }

        public async Task Call_Async()
        {
            var logger = m_Factory.CreateLogger(nameof(Call_Async));

            for (int i = 0; i < 10; i++)
            {
                using (var scope = logger.BeginScope("Scope " + i))
                {
                    HttpClient client = new HttpClient();
                    await client.GetAsync("http://www.google.de");
                    logger.LogError("Test Error");
                }
            }

        }


        [TestMethod()]
        public void NotAsyncTest1()
        {
            var logger = m_Factory.CreateLogger(nameof(AsyncTest1));
            using (var scope = logger.BeginScope("Scope Base"))
            {
                logger.LogError("Test Error"); // Calls Event hub
                Call();

            }
            //logger.LogWarning("Test Warning"); // Calls Event hub
            //logger.LogInformation("Test Information"); // Does not calls event hub
        }

        public void Call()
        {
            var logger = m_Factory.CreateLogger(nameof(Call_Async));

            for (int i = 0; i < 10; i++)
            {
                using (var scope = logger.BeginScope("Scope " + i))
                {
                    logger.LogError("Test Information");
                }
            }

        }
    }
}
