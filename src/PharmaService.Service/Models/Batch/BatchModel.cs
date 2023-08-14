using PharmaService.Service.Models._shared;

namespace PharmaService.Service.Models.Batch;

public class BatchModel
{
    public required Guid Id { get; init; }
    public required ProductView Product { get; init; }
    public required long ProductCount { get; init; }
    public required DateTimeOffset ArrivedOn { get; init; }
    public DateTimeOffset? CreatedOn { get; init; }
    public DateTimeOffset? ExpiredOn { get; init; }
    public required WarehouseView Warehouse { get; init; }
}