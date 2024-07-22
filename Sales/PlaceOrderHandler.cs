using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    internal class PlaceOrderHandler : IConsumer<PlaceOrder>
    {
        public PlaceOrderHandler(ILogger<PlaceOrderHandler> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<PlaceOrder> context)
        {
            // 20% chance of failure
            if (new Random().Next(5) == 1)
            {
                throw new NotImplementedException();
            }

            _logger.LogInformation("Placing order: {Text}", context.Message.OrderId.ToString());
            return Task.CompletedTask;
        }

        readonly ILogger<PlaceOrderHandler> _logger;
    }
}
