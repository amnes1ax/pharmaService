namespace PharmaService.IntegrationClient.Http.Models._shared;

public class ProductItem
{
    public required Guid ProductId { get; init; }
    public required string Title { get; init; }
}