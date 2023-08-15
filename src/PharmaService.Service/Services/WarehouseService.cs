using PharmaService.DataAccess.Batches;
using PharmaService.DataAccess.Warehouses;
using PharmaService.DataAccess.Warehouses.Exceptions;
using PharmaService.Domain.Entities;
using PharmaService.Service.Models._shared;
using PharmaService.Service.Models.Warehouse;

namespace PharmaService.Service.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IBatchRepository _batchRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository, IBatchRepository batchRepository)
    {
        _warehouseRepository = warehouseRepository;
        _batchRepository = batchRepository;
    }

    public async Task<Guid> CreateAsync(CreateWarehouseModel warehouseModel, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            PharmacyId = warehouseModel.PharmacyId,
            Title = warehouseModel.Title,
        };
        await _warehouseRepository.AddAsync(warehouse, cancellationToken);
        return warehouse.Id;
    }

    public async Task<WarehouseModel?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse =
            await _warehouseRepository.GetByIdAsync(warehouseId, withRelations: true,
                cancellationToken: cancellationToken);
        if (warehouse is null) throw new WarehouseNotFoundException();
        return new WarehouseModel
        {
            Id = warehouse.Id,
            Pharmacy = new PharmacyView
            {
                PharmacyId = warehouse.PharmacyId,
                Title = warehouse.Pharmacy!.Title
            },
            Title = warehouse.Title
        };
    }

    public async Task<IEnumerable<AvailableBatch>> GetBatchesAsync(Guid warehouseId,
        CancellationToken cancellationToken)
    {
        var warehouse =
            await _warehouseRepository.GetByIdAsync(warehouseId, cancellationToken: cancellationToken);
        if (warehouse is null) throw new WarehouseNotFoundException();
        var result = await _batchRepository.GetListByWarehouseIdAsync(warehouseId, cancellationToken);
        return result.Select(x =>
            new AvailableBatch
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductCount = x.ProductCount,
                ExpiredOn = x.ExpiredOn
            });
    }

    public async Task<IEnumerable<WarehouseModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var warehouses =
            await _warehouseRepository.GetListAsync(withRelations: true, cancellationToken: cancellationToken);
        return warehouses.Select(warehouse =>
            new WarehouseModel
            {
                Id = warehouse.Id,
                Pharmacy = new PharmacyView
                {
                    PharmacyId = warehouse.PharmacyId,
                    Title = warehouse.Pharmacy!.Title
                },
                Title = warehouse.Title
            }
        );
    }

    public async Task UpdateAsync(UpdateWarehouseModel warehouseModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId, cancellationToken: cancellationToken);
        if (warehouse is null) throw new WarehouseNotFoundException();
        await _warehouseRepository.DeleteAsync(warehouse, cancellationToken);
    }
}

public interface IWarehouseService
{
    Task<Guid> CreateAsync(CreateWarehouseModel warehouseModel, CancellationToken cancellationToken);
    Task<WarehouseModel?> GetByIdAsync(Guid warehouseId, CancellationToken cancellationToken);
    Task<IEnumerable<AvailableBatch>> GetBatchesAsync(Guid warehouseId, CancellationToken cancellationToken);
    Task<IEnumerable<WarehouseModel>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdateWarehouseModel warehouseModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid warehouseId, CancellationToken cancellationToken);
}