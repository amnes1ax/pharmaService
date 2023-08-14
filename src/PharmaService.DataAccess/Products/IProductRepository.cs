using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Products;

public interface IProductRepository
{
    public Task AddAsync(Product product, CancellationToken cancellationToken);
    public Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    public Task<IEnumerable<Product>> GetListAsync(CancellationToken cancellationToken);
    public Task UpdateAsync(Product product, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid productId, CancellationToken cancellationToken);
}