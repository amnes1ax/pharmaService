namespace PharmaService.IntegrationClient.Http.Models._shared;

public class PharmacyItem
{
    public required Guid PharmacyId { get; init; }
    public required string Title { get; init; }
}