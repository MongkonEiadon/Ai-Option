using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using iqoption.domain.Common.Configuration;
using iqoption.domain.Orders;
using iqoption.servicebus.Abstracts;
using Microsoft.Extensions.Options;

namespace iqoption.servicebus.Position
{
    public interface IOrderSender {
        Task Send(Order content);
    }
    public class OrderSender : QueueSender<Order>, IOrderSender {

        public OrderSender(IOptions<AzureServiceBusConfiguration> configuration) :
            base(configuration.Value?.ConnectionString, "order") {

        }



        public Task Send(Order content) {
            return base.SendMessage(content);
        }

    }
}
