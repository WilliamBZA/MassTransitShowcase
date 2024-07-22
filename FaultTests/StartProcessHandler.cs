using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaultTests
{
    internal class StartProcessHandler : IConsumer<StartProcess>
    {
        public StartProcessHandler(ILogger<StartProcessHandler> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StartProcess> context)
        {
            // 20% chance of failure
            if (new Random().Next(5) == 1)
            {
                throw new NotImplementedException();
            }

            _logger.LogInformation("Received Text: {Text}", context.Message.Message);

            await context.Publish(new PlaceOrder { OrderId = Guid.NewGuid() });
        }

        readonly ILogger<StartProcessHandler> _logger;
    }
}
