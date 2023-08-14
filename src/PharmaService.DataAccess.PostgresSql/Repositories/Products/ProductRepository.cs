using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Products;
using PharmaService.DataAccess.Products.Exceptions;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly PharmaDbContext _context;
    
    public ProductRepository(PharmaDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken) =>
        await _context.Products.AsNoTracking().Where(x => x.Id == productId).FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Product>> GetListAsync(CancellationToken cancellationToken) =>
        await _context.Products.AsNoTracking().ToListAsync(cancellationToken);

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid productId, CancellationToken cancellationToken)
    {
        var existingProduct = await GetByIdAsync(productId, cancellationToken);
        if (existingProduct is null) throw new ProductNotFoundException();
        _context.Products.Remove(existingProduct);
        await _context.SaveChangesAsync(cancellationToken);
    }
}