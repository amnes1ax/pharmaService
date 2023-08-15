using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Batches;

public interface IBatchRepository
{
    public Task AddAsync(Batch batch, CancellationToken cancellationToken = default);
    public Task<Batch?> GetByIdAsync(Guid batchId, bool withRelations = false,
        CancellationToken cancellationToken = default);
    public Task<IEnumerable<Batch>> GetListAsync(bool withRelations = false,
        CancellationToken cancellationToken = default);
    public Task<IEnumerable<Batch>> GetListByWarehouseIdAsync(Guid warehouseId, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Batch batch, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Batch batch, CancellationToken cancellationToken = default);
}