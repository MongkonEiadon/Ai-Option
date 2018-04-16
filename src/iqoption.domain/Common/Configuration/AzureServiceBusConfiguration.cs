using System;
using System.Collections.Generic;
using System.Text;

namespace iqoption.domain.Common.Configuration
{
    public class AzureServiceBusConfiguration
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }

        public AzureServiceBusConfiguration() {

        }

        public AzureServiceBusConfiguration(string connectionString, string queueName) {
            ConnectionString = connectionString;
            QueueName = queueName;
        }
    }
}
