namespace PharmaService.Service.Models.Warehouse;

public class AvailableBatch
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required long ProductCount { get; init; }
    public DateTimeOffset? ExpiredOn { get; init; }
}