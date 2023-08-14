using PharmaService.Service.Models._shared;

namespace PharmaService.Service.Models.Warehouse;

public class AvailableBatch
{
    public required Guid Id { get; init; }
    public required ProductView Product { get; init; }
    public required long ProductCount { get; init; }
    public DateTimeOffset? ExpiredOn { get; init; }
}