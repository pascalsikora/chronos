namespace Chronos.Orders.Contracts;

public record Orders
{
    public DateTime Datetime { get; init; }
    public string Value { get; init; }
}
