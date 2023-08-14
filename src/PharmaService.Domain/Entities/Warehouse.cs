namespace PharmaService.Domain.Entities;

public class Warehouse
{
    public required Guid Id { get; set; }
    public required Guid PharmacyId { get; set; }
    public required string Title { get; set; }
}