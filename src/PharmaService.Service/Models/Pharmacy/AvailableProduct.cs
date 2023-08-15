namespace PharmaService.Service.Models.Pharmacy;

public sealed class AvailableProduct
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
}