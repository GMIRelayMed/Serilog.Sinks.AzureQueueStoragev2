using Azure.Storage.Queues;

namespace Serilog.Sinks.AzureQueueStoragev2
{
    public class QueueClientFactory
    {
        public virtual QueueClient Create(string connectionString, string queueName)
        {
            return new QueueClient(connectionString, queueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });
        }
    }
}
