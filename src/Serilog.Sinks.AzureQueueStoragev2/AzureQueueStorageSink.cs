using Azure.Storage.Queues;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Json;

namespace Serilog.Sinks.AzureQueueStoragev2
{
    public class AzureQueueStorageSink : ILogEventSink
    {
        private readonly string ConnectionString;
        private readonly string QueueName;
        private readonly ITextFormatter textFormatter;

        public AzureQueueStorageSink(string ConnectionString, string QueueName)
        {
            this.ConnectionString = ConnectionString;
            this.QueueName = QueueName;
            this.textFormatter = new JsonFormatter("", false, null);
        }

        public void Emit(LogEvent logEvent)
        {
            QueueClient queueClient = new QueueClient(ConnectionString, QueueName);
            queueClient.SendMessageAsync(JsonConvert.SerializeObject(logEvent)).SyncContextSafeWait();
        }
    }
}
