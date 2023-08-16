using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class BatchController
{
    public sealed class CreationBatchModel
    {
        public Guid? ProductId { get; init; }
        public long? ProductCount { get; init; }
        public DateTimeOffset? CreatedOn { get; init; }
        public Guid? WarehouseId { get; init; }
        
        [SuppressMessage("ReSharper", "UnusedType.Global")]
        public sealed class Validator : AbstractValidator<CreationBatchModel>
        {
            public Validator()
            {
                RuleFor(model => model.ProductId)
                    .NotEmpty()
                    .WithMessage("ProductId is required.");

                RuleFor(model => model.ProductCount)
                    .NotEmpty()
                    .WithMessage("ProductCount is required.")
                    .GreaterThan(0)
                    .WithMessage("ProductCount must be greater than 0.");

                RuleFor(model => model.CreatedOn)
                    .LessThanOrEqualTo(DateTimeOffset.UtcNow)
                    .WithMessage("CreatedOn cannot be greater than the current date and time.");

                RuleFor(model => model.WarehouseId)
                    .NotEmpty()
                    .WithMessage("WarehouseId is required.");
            }
        }
    }
}