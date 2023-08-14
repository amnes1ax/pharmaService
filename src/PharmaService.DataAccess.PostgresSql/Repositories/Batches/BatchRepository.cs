using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Batches;
using PharmaService.DataAccess.Batches.Exceptions;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Batches;

public class BatchRepository : IBatchRepository
{
    private readonly PharmaDbContext _context;

    public BatchRepository(PharmaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Batch batch, CancellationToken cancellationToken)
    {
        await _context.Batches.AddAsync(batch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Batch?> GetByIdAsync(Guid batchId, CancellationToken cancellationToken) =>
        await _context.Batches.AsNoTracking().Where(x => x.Id == batchId).FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Batch>> GetListAsync(CancellationToken cancellationToken) =>
        await _context.Batches.AsNoTracking().ToListAsync(cancellationToken);

    public async Task UpdateAsync(Batch batch, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid batchId, CancellationToken cancellationToken)
    {
        var existingBatch = await GetByIdAsync(batchId, cancellationToken);
        if (existingBatch is null) throw new BatchNotFoundException();
        _context.Batches.Remove(existingBatch);
        await _context.SaveChangesAsync(cancellationToken);
    }
}