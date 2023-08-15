namespace PharmaService.Domain.Entities;

public class Batch
{
    public required Guid Id { get; set; }
    public required Guid ProductId { get; set; }
    public required long ProductCount { get; set; }
    public required DateTimeOffset ArrivedOn { get; set; }
    public DateTimeOffset? CreatedOn { get; set; }
    public DateTimeOffset? ExpiredOn { get; set; }
    public required Guid WarehouseId { get; set; }
    
    public virtual Product? Product { get; set; }
    public virtual Warehouse? Warehouse { get; set; }
}