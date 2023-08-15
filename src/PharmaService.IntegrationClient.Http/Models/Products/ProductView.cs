namespace PharmaService.IntegrationClient.Http.Models.Products;

public class ProductView
{
    public required Guid ProductId { get; init; }
    public required string Title { get; init; }
}