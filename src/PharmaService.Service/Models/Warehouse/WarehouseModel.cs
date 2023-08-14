using PharmaService.Service.Models._shared;

namespace PharmaService.Service.Models.Warehouse;

public class WarehouseModel
{
    public required Guid Id { get; init; }
    public required PharmacyView Pharmacy { get; init; }
    public required string Title { get; init; }
}