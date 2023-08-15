using PharmaService.DataAccess.Pharmacies;
using PharmaService.DataAccess.Pharmacies.Exceptions;
using PharmaService.DataAccess.Products;
using PharmaService.Domain.Entities;
using PharmaService.Service.Models.Pharmacy;

namespace PharmaService.Service.Services;

public class PharmacyService : IPharmacyService
{
    private readonly IPharmacyRepository _pharmacyRepository;
    private readonly IProductRepository _productRepository;


    public PharmacyService(IPharmacyRepository pharmacyRepository,
        IProductRepository productRepository)
    {
        _pharmacyRepository = pharmacyRepository;
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
        var pharmacy = await _pharmacyRepository.GetByIdAsync(pharmacyId, cancellationToken: cancellationToken);
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
        var pharmacy = await _pharmacyRepository.GetByIdAsync(pharmacyId, cancellationToken: cancellationToken);
        if (pharmacy is null) throw new PharmacyNotFoundException();
        var products =
            await _productRepository.GetListByPharmacyIdAsync(pharmacyId, cancellationToken: cancellationToken);
        return products.Select(x=>new AvailableProduct
        {
            Id = x.Id,
            Title = x.Title
        });
    }

    public async Task<IEnumerable<PharmacyModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var pharmacies = await _pharmacyRepository.GetListAsync(cancellationToken: cancellationToken);
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
        var pharmacy = await _pharmacyRepository.GetByIdAsync(pharmacyId, cancellationToken: cancellationToken);
        if (pharmacy is null) throw new PharmacyNotFoundException();
        await _pharmacyRepository.DeleteAsync(pharmacy, cancellationToken);
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