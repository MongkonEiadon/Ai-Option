﻿namespace iqoption.domain.Common.Configuration {
    public class AzureServiceBusConfiguration {
        public AzureServiceBusConfiguration() {
        }

        public AzureServiceBusConfiguration(string connectionString, string queueName) {
            ConnectionString = connectionString;
            QueueName = queueName;
        }

        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}