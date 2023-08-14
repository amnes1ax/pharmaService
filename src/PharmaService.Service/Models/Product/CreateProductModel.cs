namespace PharmaService.Service.Models.Product;

public class CreateProductModel
{
    public required string Title { get; init; }
    public int? ShelfLife { get; init; }
}