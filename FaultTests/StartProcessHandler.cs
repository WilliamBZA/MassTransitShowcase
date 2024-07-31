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
            // 14% chance of failure
            if (new Random().Next(7) == 1)
            {
                throw new ApplicationException("Could not start a new sales process");
            }

            _logger.LogInformation("Received Text: {Text}", context.Message.Message);

            await context.Publish(new PlaceOrder { OrderId = Guid.NewGuid() });
        }

        readonly ILogger<StartProcessHandler> _logger;
    }
}
