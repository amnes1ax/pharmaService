namespace PharmaService.IntegrationClient.Http.Models._shared;

public class WarehouseItem
{
    public required Guid WarehouseId { get; init; }
    public required string Title { get; init; }
}