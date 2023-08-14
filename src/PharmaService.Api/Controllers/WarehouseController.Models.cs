using FluentValidation;

namespace PharmaService.Api.Controllers;

public partial class WarehouseController
{
    public sealed class CreationWarehouseModel
    {
        public Guid? PharmacyId { get; init; }
        public string? Title { get; init; }
        
        public sealed class Validator : AbstractValidator<CreationWarehouseModel>
        {
            public Validator()
            {
                
            }
        }
    }
}