using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.AzureQueueStoragev2
{
    public static class LoggerConfigurationAzureQueueStorageExtensions
    {
        public static LoggerConfiguration AzureQueueStorage(this LoggerSinkConfiguration loggerConfiguration, string connectionString, string queueName, LogEventLevel restrictedToMinimumLevel = 0)
        {
            ILogEventSink logEventSink;
            logEventSink = new AzureQueueStorageSink(connectionString, queueName);
            return loggerConfiguration.Sink(logEventSink, restrictedToMinimumLevel);
        }    
    }
}
