namespace Chronos.Orders;

using System.Reflection;
using MassTransit;

public class Program
{
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddMassTransit(mt =>
                {
                    mt.SetKebabCaseEndpointNameFormatter();
                    mt.SetInMemorySagaRepositoryProvider();

                    var entryAssembly = Assembly.GetEntryAssembly();

                    mt.AddConsumers(entryAssembly);
                    mt.AddSagaStateMachines(entryAssembly);
                    mt.AddSagas(entryAssembly);
                    mt.AddActivities(entryAssembly);

                    mt.UsingInMemory((context, cfg) =>
                    {
                        cfg.ConfigureEndpoints(context);
                    });
                });

                services.AddHostedService<Publisher>();
            });

    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }
}
