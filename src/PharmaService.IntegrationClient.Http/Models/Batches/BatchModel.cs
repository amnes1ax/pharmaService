using PharmaService.IntegrationClient.Http.Models._shared;

namespace PharmaService.IntegrationClient.Http.Models.Batches;

public class BatchModel
{
    public required Guid Id { get; init; }
    public required ProductItem Product { get; init; }
    public required long ProductCount { get; init; }
    public required DateTimeOffset ArrivedOn { get; init; }
    public DateTimeOffset? CreatedOn { get; init; }
    public DateTimeOffset? ExpiredOn { get; init; }
    public required WarehouseItem Warehouse { get; init; }
}