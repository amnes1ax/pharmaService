using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Warehouses;

public interface IWarehouseRepository
{
    public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken);
    public Task<Warehouse?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken);
    public Task<IEnumerable<Warehouse>> GetListAsync(CancellationToken cancellationToken);
    public Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid warehouseId, CancellationToken cancellationToken);
}