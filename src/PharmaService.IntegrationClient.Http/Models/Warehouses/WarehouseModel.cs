using PharmaService.IntegrationClient.Http.Models._shared;

namespace PharmaService.IntegrationClient.Http.Models.Warehouses;

public class WarehouseModel
{
    public Guid Id { get; init; }
    public PharmacyItem Pharmacy { get; init; }
    public string Title { get; init; }
}