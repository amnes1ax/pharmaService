using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Batches;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Batches;

public class BatchRepository : IBatchRepository
{
    private readonly PharmaDbContext _context;

    public BatchRepository(PharmaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Batch batch, CancellationToken cancellationToken = default)
    {
        await _context.Batches.AddAsync(batch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Batch?> GetByIdAsync(Guid batchId, bool withRelations = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Batches
            .Where(x => x.Id == batchId)
            .AsNoTracking();
        if (withRelations)
            query.Include(x => x.Product);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Batch>> GetListAsync(bool withRelations = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Batches.AsNoTracking();
        if (withRelations)
            query.Include(x => x.Product);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Batch>> GetListByWarehouseIdAsync(Guid warehouseId,
        CancellationToken cancellationToken = default)
    {
        var warehouse = await _context.Warehouses.Where(x => x.Id == warehouseId)
            .Include(x => x.Batches)
            .AsNoTracking()
            .FirstAsync(cancellationToken);

        return warehouse.Batches;
    }

    public async Task UpdateAsync(Batch batch, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public async Task DeleteAsync(Batch batch, CancellationToken cancellationToken = default)
    {
        _context.Batches.Remove(batch);
        await _context.SaveChangesAsync(cancellationToken);
    }
}