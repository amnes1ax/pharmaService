using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class PharmacyController
{
    public sealed class CreationPharmacyModel
    {
        public string? Title { get; init; }
        public string? Address { get; init; }
        public string? PhoneNumber { get; init; }
        
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationPharmacyModel>
        {
            public Validator()
            {
                RuleFor(model => model.Title)
                    .NotEmpty()
                    .WithMessage("Title is required.")
                    .MaximumLength(100)
                    .WithMessage("Title cannot exceed 100 characters.");

                RuleFor(model => model.Address)
                    .MaximumLength(200)
                    .WithMessage("Address cannot exceed 200 characters.");

                RuleFor(model => model.PhoneNumber)
                    .Matches(@"^\d{10}$")
                    .WithMessage("PhoneNumber must be a valid 10-digit number.");
            }
        }
    }
}