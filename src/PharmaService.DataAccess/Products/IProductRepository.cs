using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Products;

public interface IProductRepository
{
    public Task AddAsync(Product product, CancellationToken cancellationToken = default);
    public Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Product>> GetListAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<Product>> GetListByPharmacyIdAsync(Guid pharmacyId,
        CancellationToken cancellationToken = default);
    public Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
}