namespace PharmaService.IntegrationClient.Http.Models.Products;

public class CreateProductRequest
{
    public required string Title { get; init; }
    public int? ShelfLife { get; init; }
}