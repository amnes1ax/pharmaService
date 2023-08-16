using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Products;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly PharmaDbContext _context;

    public ProductRepository(PharmaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var query = _context.Products
            .Where(x => x.Id == productId)
            .AsNoTracking();
        return await query.FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Product>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetListByPharmacyIdAsync(Guid pharmacyId,
        CancellationToken cancellationToken = default)
    {
        var pharmacy = await _context.Pharmacies.Where(x => x.Id == pharmacyId)
            .Include(x => x.Warehouses)
            .ThenInclude(x=>x.Batches)
            .ThenInclude(x=>x.Product)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstAsync(cancellationToken);
        
        var batchedProducts =
            from warehouses in pharmacy.Warehouses
            from batch in warehouses.Batches
            select batch.Product;

        return batchedProducts.DistinctBy(x=>x.Id);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}