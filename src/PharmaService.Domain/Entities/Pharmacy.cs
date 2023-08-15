namespace PharmaService.Domain.Entities;

public class Pharmacy
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual ICollection<Warehouse> Warehouses { get; set; } = null!;
}