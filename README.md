# Serilog.Sinks.AzureQueueStoragev2

**Package** - [Serilog.Sinks.AzureQueueStoragev2](https://www.nuget.org/packages/Serilog.Sinks.AzureQueueStoragev2/) | .NET Standard 2.0

Package to write to an Azure Queue in an Azure Storage Account. This package replaces the deprecated and no longer maintained library of [Serilog.Sinks.AzureQueueStorage](https://www.nuget.org/packages/Serilog.Sinks.AzureQueueStorage). 

It now uses the [Azure.Storage.Queues](https://www.nuget.org/packages/Azure.Storage.Queues/) which replaces the deprecated [WindowsAzure.Storage](https://www.nuget.org/packages/WindowsAzure.Storage/), which was used in the previous package.

[Getting started guide with Azure Queues](https://docs.microsoft.com/en-us/azure/storage/queues/storage-quickstart-queues-dotnet?tabs=environment-variable-windows).

## Build status

[![Build Status](https://dev.azure.com/relaymed/Relaymed/_apis/build/status/GMIRelayMed.Serilog.Sinks.AzureQueueStoragev2?branchName=main)](https://dev.azure.com/relaymed/Relaymed/_build/latest?definitionId=97&branchName=main)


## Example use

```csharp
var log = new LoggerConfiguration().WriteTo
    .AzureQueueStorage("azureStorageAccountConnectionString", "azureQueueName")
    .CreateLogger();
```

Optional parameter of restrictedToMinimumLevel which is an `LogEventLevel` enum that defines the minimum level of logging. By default this is set to 0 (verbose). You can override like so:

```csharp
var log = new LoggerConfiguration().WriteTo
    .AzureQueueStorage(azureStorageAccountConnectionString, azureQueueName, LogEventLevel.Information)
    .CreateLogger();
```
