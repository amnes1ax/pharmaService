namespace PharmaService.IntegrationClient.Http.Models.Products;

public class ProductModel
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public int? ShelfLife { get; init; }
}