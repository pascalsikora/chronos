namespace Chronos.Orders;

using Chronos.Orders.Contracts;
using MassTransit;

public class Publisher : BackgroundService
{
    private readonly IBus _bus;

    public Publisher(IBus bus)
    {
        this._bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var orderId = 1;

        while (!stoppingToken.IsCancellationRequested)
        {
            var orderNumber = $"ORD{DateTime.Now.Year}/{orderId.ToString().PadLeft(totalWidth: 5, paddingChar: '0')}";

            await this._bus.Publish(new Orders
                {
                    Datetime = DateTime.Now,
                    Value = $"Order {orderNumber}",
                },
                stoppingToken
            );

            await Task.Delay(millisecondsDelay: 1000, stoppingToken);
            orderId++;
        }
    }
}
