namespace PharmaService.Domain.Entities;

public class Warehouse
{
    public required Guid Id { get; set; }
    public required Guid PharmacyId { get; set; }
    public required string Title { get; set; }
    
    public virtual Pharmacy? Pharmacy { get; set; }
    public virtual ICollection<Batch> Batches { get; set; } = null!;
}