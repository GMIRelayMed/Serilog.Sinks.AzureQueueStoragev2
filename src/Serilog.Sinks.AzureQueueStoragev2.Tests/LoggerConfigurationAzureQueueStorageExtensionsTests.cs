using NUnit.Framework;
using System;

namespace Serilog.Sinks.AzureQueueStoragev2.Tests
{
    public class LoggerConfigurationAzureQueueStorageExtensionsTests
    {
        [Test]
        public void Throws_ArgumentNullException_when_logger_sink_null()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => LoggerConfigurationAzureQueueStorageExtensions.AzureQueueStorage(null, "azureConnectionString", "queueName"));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'loggerConfiguration')"));
        }

        [Test]
        public void Throws_ArgumentException_when_connectionstring_empty()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => LoggerConfigurationAzureQueueStorageExtensions.AzureQueueStorage(null, "", "queueName"));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'connectionString')"));
        }

        [Test]
        public void Throws_ArgumentException_whenqueuename_empty()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => LoggerConfigurationAzureQueueStorageExtensions.AzureQueueStorage(null, "azureConnectionString", ""));
            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'queueName')"));
        }
    }
}