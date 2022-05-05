using Azure.Storage.Queues;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.AzureQueueStoragev2
{
    public class AzureQueueStorageSink : ILogEventSink
    {
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly QueueClientFactory queueClientFactory;

        public AzureQueueStorageSink(string ConnectionString, string QueueName, QueueClientFactory queueClientFactory)
        {
            this.ConnectionString = ConnectionString;
            this.QueueName = QueueName;
            this.queueClientFactory = queueClientFactory;
        }

        /// <summary>
        /// Emit the provided log event to the queue sink
        /// </summary>
        /// <param name="logEvent">The log event to write to the queue</param>
        /// <exception cref="TimeoutException">Sending message to the queue timed out</exception>
        public void Emit(LogEvent logEvent)
        {
            QueueClient queueClient = queueClientFactory.Create(ConnectionString, QueueName);
            queueClient.SendMessageAsync(JsonConvert.SerializeObject(logEvent)).SyncContextSafeWait();
        }
    }
}
