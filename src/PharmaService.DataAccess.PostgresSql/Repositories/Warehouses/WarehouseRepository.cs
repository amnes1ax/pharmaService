using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Warehouses;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Warehouses;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly PharmaDbContext _context;

    public WarehouseRepository(PharmaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
    {
        await _context.Warehouses.AddAsync(warehouse, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Warehouse?> GetByIdAsync(Guid warehouseId, bool withRelations = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Warehouses
            .Where(warehouse => warehouse.Id == warehouseId);
        if (withRelations)
        {
            await query
                .Include(x => x.Pharmacy)
                .LoadAsync(cancellationToken);
        }
        else
        {
            query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Warehouse>> GetListAsync(bool withRelations = false,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Warehouses;
        if (withRelations)
        {
            await query
                .Include(x => x.Pharmacy)
                .LoadAsync(cancellationToken);
        }
        else
        {
            query.AsNoTracking();
        }
        return await query.ToListAsync(cancellationToken);
    }


    public async Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
    {
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync(cancellationToken);
    }
}