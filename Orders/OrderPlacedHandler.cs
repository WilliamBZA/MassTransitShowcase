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
            // 20% chance of failure
            if (new Random().Next(5) == 1)
            {
                throw new NotImplementedException();
            }

            _logger.LogInformation("Order Placed order: {Text}", context.Message.OrderId.ToString());
            return Task.CompletedTask;
        }

        readonly ILogger<OrderPlacedHandler> _logger;
    }
}
