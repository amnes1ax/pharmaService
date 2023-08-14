namespace PharmaService.Service.Models.Pharmacy;

public class PharmacyModel
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Address { get; init; }
    public string? PhoneNumber { get; init; }
}