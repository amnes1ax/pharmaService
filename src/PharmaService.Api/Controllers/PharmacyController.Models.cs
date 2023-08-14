using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class PharmacyController
{
    public sealed class CreationPharmacyModel
    {
        public string? Title { get; init; }
        public string? Address { get; init; }
        public string? PhoneNumber { get; init; }
        
        public sealed class Validator : AbstractValidator<CreationPharmacyModel>
        {
            public Validator()
            {
                
            }
        }
    }
}