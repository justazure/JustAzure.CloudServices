using System;
using System.Globalization;
using Microsoft.WindowsAzure.Storage.Table;

namespace RoleLifecycleDemo.Worker.Model
{
    public class TableLifecycleEvent : TableEntity
    {
        public string InstanceName
        {
            get;
            set;
        }

        public string EventType
        {
            get;
            set;
        }

        public static TableLifecycleEvent New(string instanceName, string eventType)
        {
            return new TableLifecycleEvent
            {
                PartitionKey = (DateTime.MaxValue - DateTime.UtcNow).Ticks.ToString(CultureInfo.InvariantCulture),
                RowKey = Guid.NewGuid().ToString(),
                InstanceName = instanceName,
                EventType = eventType
            };
        }
    }
}
