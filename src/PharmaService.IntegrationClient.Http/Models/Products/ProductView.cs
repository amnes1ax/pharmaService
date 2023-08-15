namespace PharmaService.IntegrationClient.Http.Models.Products;

public class ProductView
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
}