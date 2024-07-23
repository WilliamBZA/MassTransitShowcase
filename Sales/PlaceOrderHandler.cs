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

        public async Task Consume(ConsumeContext<PlaceOrder> context)
        {
            // 10% chance of failure
            if (new Random().Next(10) == 1)
            {
                throw new ApplicationException("Could not place the order successfully");
            }

            _logger.LogInformation("Placing order: {Text}", context.Message.OrderId.ToString());

            await context.Publish(new OrderPlaced { OrderId = context.Message.OrderId });
        }

        readonly ILogger<PlaceOrderHandler> _logger;
    }
}
