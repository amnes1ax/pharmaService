using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Warehouses;

public interface IWarehouseRepository
{
    public Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
    public Task<Warehouse?> GetByIdAsync(Guid warehouseId, bool withRelations = false,
        CancellationToken cancellationToken = default);
    public Task<IEnumerable<Warehouse>> GetListAsync(bool withRelations = false,
        CancellationToken cancellationToken = default);
    public Task UpdateAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
}