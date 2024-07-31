using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders
{
    internal class OrderPlacedHandler : IConsumer<OrderPlaced>
    {
        public OrderPlacedHandler(ILogger<OrderPlacedHandler> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderPlaced> context)
        {
            // 14% chance of failure
            if (new Random().Next(7) == 1)
            {
                throw new ApplicationException("Could not ship the order");
            }

            _logger.LogInformation("Order Placed order: {Text}", context.Message.OrderId.ToString());
            return Task.CompletedTask;
        }

        readonly ILogger<OrderPlacedHandler> _logger;
    }
}
