namespace PharmaService.Service.Models.Warehouse;

public sealed class CreateWarehouseModel
{
    public required Guid PharmacyId { get; init; }
    public required string Title { get; init; }
}