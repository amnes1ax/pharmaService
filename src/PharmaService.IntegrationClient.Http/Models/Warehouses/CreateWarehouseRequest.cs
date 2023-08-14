namespace PharmaService.IntegrationClient.Http.Models.Warehouses;

public class CreateWarehouseRequest
{
    public required Guid PharmacyId { get; init; }
    public required string Title { get; init; }
}