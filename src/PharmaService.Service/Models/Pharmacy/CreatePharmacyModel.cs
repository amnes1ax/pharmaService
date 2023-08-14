namespace PharmaService.Service.Models.Pharmacy;

public class CreatePharmacyModel
{
    public required string Title { get; init; }
    public string? Address { get; init; }
    public string? PhoneNumber { get; init; }
}