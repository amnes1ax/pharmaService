using PharmaService.DataAccess.Pharmacies;
using PharmaService.DataAccess.Pharmacies.Exceptions;
using PharmaService.DataAccess.Products;
using PharmaService.DataAccess.Products.Exceptions;
using PharmaService.Domain.Entities;
using PharmaService.Service.Models.Product;
using PharmaService.Service.Models._shared;

namespace PharmaService.Service.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IPharmacyRepository _pharmacyRepository;

    public ProductService(IProductRepository productRepository, IPharmacyRepository pharmacyRepository)
    {
        _productRepository = productRepository;
        _pharmacyRepository = pharmacyRepository;
    }
    
    public async Task CreateAsync(CreateProductModel productModel, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = productModel.Title,
            ShelfLife = productModel.ShelfLife,
        };
        await _productRepository.AddAsync(product, cancellationToken);
    }

    public async Task<ProductModel?> GetByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null) throw new ProductNotFoundException();
        return new ProductModel
        {
            Id = product.Id,
            Title = product.Title,
            ShelfLife = product.ShelfLife
        };
    }

    public async Task<IEnumerable<ProductModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var warehouses = await _productRepository.GetListAsync(cancellationToken);
        return warehouses.Select(product => new ProductModel
        {
            Id = product.Id,
            Title = product.Title,
            ShelfLife = product.ShelfLife
        });
    }

    public async Task<IEnumerable<ProductView>> GetListByPharmacyAsync(Guid pharmacyId, CancellationToken cancellationToken)
    {
        var pharmacy = await _pharmacyRepository.GetByIdAsync(pharmacyId, cancellationToken: cancellationToken);
        if (pharmacy is null) throw new PharmacyNotFoundException();
        var result = await _productRepository.GetListByPharmacyIdAsync(pharmacy.Id, cancellationToken);
        return result.Select(x => new ProductView
        {
            ProductId = x.Id,
            Title = x.Title
        });
    }

    public async Task UpdateAsync(UpdateProductModel productModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null) throw new ProductNotFoundException();
        await _productRepository.DeleteAsync(product, cancellationToken);
    }
}

public interface IProductService
{
    Task CreateAsync(CreateProductModel productModel, CancellationToken cancellationToken);
    Task<ProductModel?> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IEnumerable<ProductModel>> GetListAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ProductView>> GetListByPharmacyAsync(Guid pharmacyId, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateProductModel productModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid productId, CancellationToken cancellationToken);
}