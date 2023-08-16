using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class ProductController
{
    public sealed class CreationProductModel
    {
        public string? Title { get; init; }
        public int? ShelfLife { get; init; }
        
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationProductModel>
        {
            public Validator()
            {
                RuleFor(model => model.Title)
                    .NotEmpty()
                    .WithMessage("Title is required.")
                    .MaximumLength(100)
                    .WithMessage("Title cannot exceed 100 characters.");

                RuleFor(model => model.ShelfLife)
                    .GreaterThan(0)
                    .WithMessage("ShelfLife must be greater than 0.");
            }
        }
    }
}