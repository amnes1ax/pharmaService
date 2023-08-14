using PharmaService.DataAccess.Batches;
using PharmaService.DataAccess.Pharmacies;
using PharmaService.DataAccess.Pharmacies.Exceptions;
using PharmaService.DataAccess.Warehouses;
using PharmaService.DataAccess.Warehouses.Exceptions;
using PharmaService.Domain.Entities;
using PharmaService.Service.Models._shared;
using PharmaService.Service.Models.Warehouse;

namespace PharmaService.Service.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IPharmacyRepository _pharmacyRepository;
    private readonly IBatchRepository _batchRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IPharmacyRepository pharmacyRepository,
        IBatchRepository batchRepository)
    {
        _warehouseRepository = warehouseRepository;
        _pharmacyRepository = pharmacyRepository;
        _batchRepository = batchRepository;
    }

    public async Task CreateAsync(CreateWarehouseModel warehouseModel, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            PharmacyId = warehouseModel.PharmacyId,
            Title = warehouseModel.Title,
        };
        await _warehouseRepository.AddAsync(warehouse, cancellationToken);
    }

    public async Task<WarehouseModel?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId, cancellationToken);
        if (warehouse is null) throw new WarehouseNotFoundException();
        var pharmacy = await _pharmacyRepository.GetByIdAsync(warehouse.PharmacyId, cancellationToken);
        if (pharmacy is null) throw new PharmacyNotFoundException();
        return new WarehouseModel
        {
            Id = warehouse.Id,
            Pharmacy = new PharmacyView
            {
                PharmacyId = pharmacy!.Id,
                Title = pharmacy!.Title
            },
            Title = warehouse.Title
        };
    }

    public async Task<IEnumerable<AvailableBatch>> GetBatchesAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        var allBatches = await _batchRepository.GetListAsync(cancellationToken);
        return allBatches.Where(b=>b.WarehouseId == warehouseId).Select(b => new AvailableBatch
        {
            Id = b.Id,
            Product = new ProductView
            {
                ProductId = b.ProductId,
                Title = null
            },
            ProductCount = 0,
            ExpiredOn = null
        });
    }

    public async Task<IEnumerable<WarehouseModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var warehouses = await _warehouseRepository.GetListAsync(cancellationToken);
        var pharmacies = await _pharmacyRepository.GetListAsync(cancellationToken);

        return warehouses.Select(w =>
        {
            var pharmacy = pharmacies.FirstOrDefault(ph => ph.Id == w.PharmacyId);
            if (pharmacy is null) throw new PharmacyNotFoundException();
            return new WarehouseModel
            {
                Id = w.Id,
                Pharmacy = new PharmacyView
                {
                    PharmacyId = w.PharmacyId,
                    Title = pharmacy.Title
                },
                Title = w.Title
            };
        });
    }

    public async Task UpdateAsync(UpdateWarehouseModel warehouseModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        await _warehouseRepository.DeleteAsync(warehouseId, cancellationToken);
    }
}

public interface IWarehouseService
{
    Task CreateAsync(CreateWarehouseModel warehouseModel, CancellationToken cancellationToken);
    Task<WarehouseModel?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken);
    Task<IEnumerable<AvailableBatch>> GetBatchesAsync(Guid warehouseId, CancellationToken cancellationToken);
    Task<IEnumerable<WarehouseModel>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdateWarehouseModel warehouseModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid warehouseId, CancellationToken cancellationToken);
}