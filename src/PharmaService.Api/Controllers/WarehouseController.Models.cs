using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class WarehouseController
{
    public sealed class CreationWarehouseModel
    {
        public Guid? PharmacyId { get; init; }
        public string? Title { get; init; }
        
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationWarehouseModel>
        {
            public Validator()
            {
                RuleFor(model => model.PharmacyId)
                    .NotEmpty()
                    .WithMessage("PharmacyId is required.");
                
                RuleFor(model => model.Title)
                    .NotEmpty()
                    .WithMessage("Title is required.")
                    .MaximumLength(100)
                    .WithMessage("Title cannot exceed 100 characters.");
            }
        }
    }
}