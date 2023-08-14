namespace PharmaService.Domain.Entities;

public class Product
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public int? ShelfLife { get; set; }
}