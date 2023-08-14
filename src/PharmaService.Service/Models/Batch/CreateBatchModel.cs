namespace PharmaService.Service.Models.Batch;

public class CreateBatchModel
{
    public required Guid ProductId { get; init; }
    public required long ProductCount { get; init; }
    public DateTimeOffset? CreatedOn { get; init; }
    public required Guid WarehouseId { get; init; }
}