using PharmaService.DataAccess.Batches;
using PharmaService.DataAccess.Batches.Exceptions;
using PharmaService.DataAccess.Products;
using PharmaService.DataAccess.Products.Exceptions;
using PharmaService.DataAccess.Warehouses;
using PharmaService.DataAccess.Warehouses.Exceptions;
using PharmaService.Domain.Entities;
using PharmaService.Service.Models._shared;
using PharmaService.Service.Models.Batch;

namespace PharmaService.Service.Services;

public class BatchService : IBatchService
{
    private readonly IBatchRepository _batchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;

    public BatchService(IBatchRepository batchRepository, IProductRepository productRepository, IWarehouseRepository warehouseRepository)
    {
        _batchRepository = batchRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<Guid> CreateAsync(CreateBatchModel batchModel, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(batchModel.ProductId, cancellationToken);
        if (product is null) throw new ProductNotFoundException();
        var batch = new Batch
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            ProductCount = batchModel.ProductCount,
            ArrivedOn = DateTimeOffset.UtcNow,
            CreatedOn = batchModel.CreatedOn,
            ExpiredOn = product.ShelfLife is not null
                ? batchModel.CreatedOn?.AddDays(product.ShelfLife.Value)
                : null,
            WarehouseId = batchModel.WarehouseId
        };
        await _batchRepository.AddAsync(batch, cancellationToken);
        return batch.Id;
    }

    public async Task<BatchModel?> GetByIdAsync(Guid batchId, CancellationToken cancellationToken)
    {
        var batch = await _batchRepository.GetByIdAsync(batchId, withRelations: true,
            cancellationToken: cancellationToken);
        if (batch is null) throw new BatchNotFoundException();
        return new BatchModel
        {
            Id = batch.Id,
            Product = new ProductView
            {
                ProductId = batch.ProductId,
                Title = batch.Product!.Title
            },
            ProductCount = batch.ProductCount,
            ArrivedOn = batch.ArrivedOn,
            CreatedOn = batch.CreatedOn,
            ExpiredOn = batch.ExpiredOn,
            Warehouse = new WarehouseView
            {
                WarehouseId = batch.WarehouseId,
                Title = batch.Warehouse!.Title
            }
        };
    }

    public async Task<IEnumerable<BatchModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var batches = await _batchRepository.GetListAsync(withRelations: true, cancellationToken);
        return batches.Select(batch => new BatchModel
        {
            Id = batch.Id,
            Product = new ProductView
            {
                ProductId = batch.ProductId,
                Title = batch.Product!.Title
            },
            ProductCount = batch.ProductCount,
            ArrivedOn = batch.ArrivedOn,
            CreatedOn = batch.CreatedOn,
            ExpiredOn = batch.ExpiredOn,
            Warehouse = new WarehouseView
            {
                WarehouseId = batch.WarehouseId,
                Title = batch.Warehouse!.Title
            }
        });
    }

    public async Task<IEnumerable<BatchModel>> GetListByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken)
    {
        var warehouse = await _warehouseRepository.GetByIdAsync(warehouseId, withRelations:true, cancellationToken: cancellationToken);
        if (warehouse is null) throw new WarehouseNotFoundException();
        var result = await _batchRepository.GetListByWarehouseIdAsync(warehouseId, cancellationToken);
        return result.Select(x => new BatchModel
        {
            Id = x.Id,
            Product = new ProductView
            {
                ProductId = x.ProductId,
                Title = x.Product!.Title
            },
            ProductCount = x.ProductCount,
            ArrivedOn = x.ArrivedOn,
            CreatedOn = x.CreatedOn,
            ExpiredOn = x.ExpiredOn,
            Warehouse = new WarehouseView
            {
                WarehouseId = x.WarehouseId,
                Title = x.Warehouse!.Title
            }
        });
    }

    public async Task UpdateAsync(UpdateBatchModel batchModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid batchId, CancellationToken cancellationToken)
    {
        var batch = await _batchRepository.GetByIdAsync(batchId, cancellationToken: cancellationToken);
        if (batch is null) throw new BatchNotFoundException();
        await _batchRepository.DeleteAsync(batch, cancellationToken);
    }
}

public interface IBatchService
{
    Task<Guid> CreateAsync(CreateBatchModel batchModel, CancellationToken cancellationToken);
    Task<BatchModel?> GetByIdAsync(Guid batchId, CancellationToken cancellationToken);
    Task<IEnumerable<BatchModel>> GetListAsync(CancellationToken cancellationToken);
    Task<IEnumerable<BatchModel>> GetListByWarehouseAsync(Guid warehouseId, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateBatchModel batchModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid batchId, CancellationToken cancellationToken);
}