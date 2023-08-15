namespace PharmaService.IntegrationClient.Http.Models.Pharmacies;

public class CreatePharmacyRequest
{
    public required string Title { get; init; }
    public string? Address { get; init; }
    public string? PhoneNumber { get; init; }
}