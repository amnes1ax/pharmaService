using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Warehouses;
using PharmaService.DataAccess.Warehouses.Exceptions;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Warehouses;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly PharmaDbContext _context;
    
    public WarehouseRepository(PharmaDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        await _context.Warehouses.AddAsync(warehouse, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Warehouse?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken) =>
        await _context.Warehouses.AsNoTracking().Where(x => x.Id == warehouseId).FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Warehouse>> GetListAsync(CancellationToken cancellationToken) =>
        await _context.Warehouses.AsNoTracking().ToListAsync(cancellationToken);


    public async Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        var existingWarehouse = await GetByIdAsync(warehouseId, cancellationToken);
        if (existingWarehouse is null) throw new WarehouseNotFoundException();
        _context.Warehouses.Remove(existingWarehouse);
        await _context.SaveChangesAsync(cancellationToken);
    }
}