using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;

namespace Serilog.Sinks.AzureQueueStoragev2
{
    public static class LoggerConfigurationAzureQueueStorageExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to an Azure Queue Storage queue using the given storage account connection string and queue name.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="connectionString">The connection string for your Azure storage account</param>
        /// <param name="queueName">The queue name for your Azure storage account.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration AzureQueueStorage(this LoggerSinkConfiguration loggerConfiguration, string connectionString, string queueName, LogEventLevel restrictedToMinimumLevel = 0)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));  
            }

            if (string.IsNullOrWhiteSpace(queueName)) 
            {
                throw new ArgumentNullException(nameof(queueName));
            }

            if (loggerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(loggerConfiguration));
            }

            ILogEventSink logEventSink;
            logEventSink = new AzureQueueStorageSink(connectionString, queueName, new QueueClientFactory());
            return loggerConfiguration.Sink(logEventSink, restrictedToMinimumLevel);
        }
    }
}
