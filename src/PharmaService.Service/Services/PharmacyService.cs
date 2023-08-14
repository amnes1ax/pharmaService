using PharmaService.DataAccess.Batches;
using PharmaService.DataAccess.Pharmacies;
using PharmaService.DataAccess.Pharmacies.Exceptions;
using PharmaService.DataAccess.Products;
using PharmaService.DataAccess.Warehouses;
using PharmaService.Domain.Entities;
using PharmaService.Service.Models.Pharmacy;

namespace PharmaService.Service.Services;

public class PharmacyService : IPharmacyService
{
    private readonly IPharmacyRepository _pharmacyRepository;
    private readonly IBatchRepository _batchRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;


    public PharmacyService(IPharmacyRepository pharmacyRepository, IBatchRepository batchRepository,
        IWarehouseRepository warehouseRepository, IProductRepository productRepository)
    {
        _pharmacyRepository = pharmacyRepository;
        _batchRepository = batchRepository;
        _warehouseRepository = warehouseRepository;
        _productRepository = productRepository;
    }

    public async Task CreateAsync(CreatePharmacyModel pharmacyModel, CancellationToken cancellationToken)
    {
        var pharmacy = new Pharmacy
        {
            Id = Guid.NewGuid(),
            Title = pharmacyModel.Title,
            Address = pharmacyModel.Address,
            PhoneNumber = pharmacyModel.PhoneNumber,
        };
        await _pharmacyRepository.AddAsync(pharmacy, cancellationToken);
    }

    public async Task<PharmacyModel?> GetByIdAsync(Guid pharmacyId, CancellationToken cancellationToken)
    {
        var pharmacy = await _pharmacyRepository.GetByIdAsync(pharmacyId, cancellationToken);
        if (pharmacy is null) throw new PharmacyNotFoundException();
        return new PharmacyModel
        {
            Id = pharmacy.Id,
            Title = pharmacy.Title,
            Address = pharmacy.Address,
            PhoneNumber = pharmacy.PhoneNumber
        };
    }

    public async Task<IEnumerable<AvailableProduct>> GetAvailableProductsListAsync(Guid pharmacyId,
        CancellationToken cancellationToken)
    {
        var pharmacy = await _pharmacyRepository.GetByIdAsync(pharmacyId, cancellationToken);
        if (pharmacy is null) throw new PharmacyNotFoundException();
        var allBatches = await _batchRepository.GetListAsync(cancellationToken);
        var allWarehouses = await _warehouseRepository.GetListAsync(cancellationToken);
        var warehouses = allWarehouses.Where(wh => wh.PharmacyId == pharmacyId);
        var batches = allBatches.Where(b =>
            warehouses.Any(wh => wh.Id == b.WarehouseId) && b.ExpiredOn > DateTimeOffset.UtcNow);
        var allProducts = await _productRepository.GetListAsync(cancellationToken);
        return batches.Select(x =>
        {
            var product = allProducts.First(pr => pr.Id == x.ProductId);
            return new AvailableProduct
            {
                Id = x.ProductId,
                Title = product.Title,
                ProductCount = x.ProductCount
            };
        });
    }

    public async Task<IEnumerable<PharmacyModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var pharmacies = await _pharmacyRepository.GetListAsync(cancellationToken);
        return pharmacies.Select(ph => new PharmacyModel
        {
            Id = ph.Id,
            Title = ph.Title,
            Address = ph.Address,
            PhoneNumber = ph.PhoneNumber
        });
    }

    public async Task UpdateAsync(UpdatePharmacyModel pharmacyModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid pharmacyId, CancellationToken cancellationToken)
    {
        await _pharmacyRepository.DeleteAsync(pharmacyId, cancellationToken);
    }
}

public interface IPharmacyService
{
    Task CreateAsync(CreatePharmacyModel pharmacyModel, CancellationToken cancellationToken);
    Task<PharmacyModel?> GetByIdAsync(Guid pharmacyId, CancellationToken cancellationToken);

    Task<IEnumerable<AvailableProduct>> GetAvailableProductsListAsync(Guid pharmacyId,
        CancellationToken cancellationToken);

    Task<IEnumerable<PharmacyModel>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdatePharmacyModel pharmacyModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid pharmacyId, CancellationToken cancellationToken);
}