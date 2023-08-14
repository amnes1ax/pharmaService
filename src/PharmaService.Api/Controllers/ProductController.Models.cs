using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class ProductController
{
    public sealed class CreationProductModel
    {
        public string? Title { get; init; }
        public int? ShelfLife { get; init; }
        
        public sealed class Validator : AbstractValidator<CreationProductModel>
        {
            public Validator()
            {
                
            }
        }
    }
}