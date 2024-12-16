namespace Chronos.Orders.Consumers;

using Chronos.Orders.Contracts;
using MassTransit;

public class OrdersConsumer :
    IConsumer<Orders>
{
    private readonly ILogger<OrdersConsumer> logger;

    public OrdersConsumer(ILogger<OrdersConsumer> logger)
    {
        this.logger = logger;
    }

    public Task Consume(ConsumeContext<Orders> context)
    {
        this.logger.LogInformation("Received Order: {Text}, published at: {Date}", context.Message.Value, context.Message.Datetime);

        return Task.CompletedTask;
    }
}
