namespace PharmaService.Service.Models.Product;

public class ProductModel
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public int? ShelfLife { get; init; }
}