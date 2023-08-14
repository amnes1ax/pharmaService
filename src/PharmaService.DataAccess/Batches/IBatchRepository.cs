using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Batches;

public interface IBatchRepository
{
    public Task AddAsync(Batch batch, CancellationToken cancellationToken);
    public Task<Batch?> GetByIdAsync(Guid batchId, CancellationToken cancellationToken);
    public Task<IEnumerable<Batch>> GetListAsync(CancellationToken cancellationToken);
    public Task UpdateAsync(Batch batch, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid batchId, CancellationToken cancellationToken);
}