using Microsoft.Extensions.Hosting;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace FaultTests
{
    internal class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host("localhost", "/", h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                            cfg.ConfigureEndpoints(context);
                        });

                        x.AddConfigureEndpointsCallback((name, cfg) =>
                        {
                            if (cfg is IRabbitMqReceiveEndpointConfigurator rmq)
                                rmq.SetQuorumQueue();
                        });

                        x.AddConsumer<StartProcessHandler>(e =>
                        {
                            e.UseMessageRetry(c => c.Immediate(0));
                        });
                    });

                    services.AddHostedService<Worker>();
                });

            return host;
        }

        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Console.WriteLine("Starting endpoints...");

            await host.StartAsync();

            Console.ReadLine();
        }
    }
}
