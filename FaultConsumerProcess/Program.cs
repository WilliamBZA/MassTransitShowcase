using MassTransit;
using Microsoft.Extensions.Hosting;

namespace FaultConsumerProcess
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
                            // cfg.SendTopology.ErrorQueueNameFormatter
                        });
                    });

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
