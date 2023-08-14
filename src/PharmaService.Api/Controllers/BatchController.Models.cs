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
        
        public sealed class Validator : AbstractValidator<CreationBatchModel>
        {
            public Validator()
            {
                
            }
        }
    }
}