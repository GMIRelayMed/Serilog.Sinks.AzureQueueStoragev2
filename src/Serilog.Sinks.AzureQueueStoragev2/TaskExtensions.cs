using System;
using System.Threading;
using System.Threading.Tasks;

namespace Serilog.Sinks.AzureQueueStoragev2
{
    internal static class TaskExtensions
    {
        internal static T SyncContextSafeWait<T>(this Task<T> task, int timeout = -1)
        {
            SynchronizationContext current = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);
            try
            {
                return task.Wait(timeout) ? task.Result : throw new TimeoutException("Operation failed to complete within allotted time.");
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(current);
            }
        }
    }
}
