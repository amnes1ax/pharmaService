namespace PharmaService.IntegrationClient.Http.Models.Batches;

public class CreateBatchRequest
{
    public required Guid ProductId { get; init; }
    public required long ProductCount { get; init; }
    public DateTimeOffset? CreatedOn { get; init; }
    public required Guid WarehouseId { get; init; }
}