using Azure.Storage.Queues;
using Moq;
using NUnit.Framework;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;

namespace Serilog.Sinks.AzureQueueStoragev2.Tests
{
    public class AzureQueueStorageSinkTests
    {
        private AzureQueueStorageSink azureQueueStorageSink;
        private LogEvent logEvent;
        private Mock<QueueClient> queueClientMock;
        private Mock<QueueClientFactory> queueClientFactoryMock;
        private string expectedLogEventOutput;
        private readonly string connectionString = "connectionString";
        private readonly string queueName = "queueName";

        [SetUp]
        public void BaseSetup()
        {
            SetupLogEvent();
            SetupQueueClient();
            azureQueueStorageSink = new AzureQueueStorageSink(connectionString, queueName, queueClientFactoryMock.Object);
        }

        private void SetupLogEvent()
        {
            var messageTemplate = new MessageTemplate("messagetemplate", new List<MessageTemplateToken>());
            logEvent = new LogEvent(DateTime.UnixEpoch, LogEventLevel.Verbose, new Exception(), messageTemplate, new List<LogEventProperty>());
            expectedLogEventOutput = "{\"Timestamp\":\"1970-01-01T00:00:00+00:00\",\"Level\":0,\"MessageTemplate\":{\"Text\":\"messagetemplate\",\"Tokens\":[]},\"Properties\":{},\"Exception\":{\"ClassName\":\"System.Exception\",\"Message\":null,\"Data\":null,\"InnerException\":null,\"HelpURL\":null,\"StackTraceString\":null,\"RemoteStackTraceString\":null,\"RemoteStackIndex\":0,\"ExceptionMethod\":null,\"HResult\":-2146233088,\"Source\":null,\"WatsonBuckets\":null}}";
        }

        private void SetupQueueClient()
        {
            queueClientMock = new Mock<QueueClient>();
            queueClientFactoryMock = new Mock<QueueClientFactory>();
            queueClientFactoryMock.Setup(qcf => qcf.Create(It.Is<string>(cs => cs.Equals("connectionString")), It.Is<string>(qn => qn.Equals("queueName")))).Returns(queueClientMock.Object);
        }

        public class Emit_CallsSuccessfully : AzureQueueStorageSinkTests
        {
            [SetUp]
            public void Setup()
            {
                azureQueueStorageSink.Emit(logEvent);
            }

            [Test]
            public void QueueClientCalledWithExpectedLogEvent()
            {
                queueClientMock.Verify(qc => qc.SendMessageAsync(expectedLogEventOutput));
            }
        }

        public class Emit_TimesOut : AzureQueueStorageSinkTests
        {
            private Exception exception;

            [SetUp]
            public void Setup()
            {
                queueClientMock.Setup(qc => qc.SendMessageAsync(expectedLogEventOutput)).Throws(new TimeoutException("Timed out, dawg"));
                exception = Assert.Throws<TimeoutException>(() => azureQueueStorageSink.Emit(logEvent));
            }

            [Test]
            public void ThrowsTimeoutException()
            {
                Assert.That(exception.Message, Is.EqualTo("Timed out, dawg"));
            }
        }
    }
}