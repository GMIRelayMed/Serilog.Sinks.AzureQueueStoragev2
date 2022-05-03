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

        public AzureQueueStorageSink(string ConnectionString, string QueueName)
        {
            this.ConnectionString = ConnectionString;
            this.QueueName = QueueName;
        }

        public void Emit(LogEvent logEvent)
        {
            QueueClient queueClient = new QueueClient(ConnectionString, QueueName);
            queueClient.SendMessageAsync(JsonConvert.SerializeObject(logEvent)).SyncContextSafeWait();
        }
    }
}
