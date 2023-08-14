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

    public BatchService(IBatchRepository batchRepository, IProductRepository productRepository,
        IWarehouseRepository warehouseRepository)
    {
        _batchRepository = batchRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
    }

    public async Task CreateAsync(CreateBatchModel batchModel, CancellationToken cancellationToken)
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
    }

    public async Task<BatchModel?> GetByIdAsync(Guid batchId, CancellationToken cancellationToken)
    {
        var batch = await _batchRepository.GetByIdAsync(batchId, cancellationToken);
        if (batch is null) throw new BatchNotFoundException();
        var product = await _productRepository.GetByIdAsync(batch.ProductId, cancellationToken);
        if (product is null) throw new ProductNotFoundException();
        var warehouse = await _warehouseRepository.GetByIdAsync(batch.WarehouseId, cancellationToken);
        if (warehouse is null) throw new WarehouseNotFoundException();
        return new BatchModel
        {
            Id = batch.Id,
            Product = new ProductView
            {
                ProductId = product.Id,
                Title = product.Title
            },
            ProductCount = batch.ProductCount,
            ArrivedOn = batch.ArrivedOn,
            CreatedOn = batch.CreatedOn,
            ExpiredOn = batch.ExpiredOn,
            Warehouse = new WarehouseView
            {
                WarehouseId = warehouse.Id,
                Title = warehouse.Title
            }
        };
    }

    public async Task<IEnumerable<BatchModel>> GetListAsync(CancellationToken cancellationToken)
    {
        var batches = await _batchRepository.GetListAsync(cancellationToken);
        var products = await _productRepository.GetListAsync(cancellationToken);
        var warehouses = await _warehouseRepository.GetListAsync(cancellationToken);
        return batches.Select(b =>
        {
            var product = products.FirstOrDefault(pr => pr.Id == b.ProductId);
            if (product is null) throw new ProductNotFoundException();
            var warehouse = warehouses.FirstOrDefault(w => w.Id == b.WarehouseId);
            if (warehouse is null) throw new WarehouseNotFoundException();
            return new BatchModel
            {
                Id = b.Id,
                Product = new ProductView
                {
                    ProductId = b.ProductId,
                    Title = product.Title
                },
                ProductCount = b.ProductCount,
                ArrivedOn = b.ArrivedOn,
                CreatedOn = b.CreatedOn,
                ExpiredOn = b.ExpiredOn,
                Warehouse = new WarehouseView
                {
                    WarehouseId = b.WarehouseId,
                    Title = warehouse.Title
                }
            };
        });
    }

    public async Task UpdateAsync(UpdateBatchModel batchModel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid batchId, CancellationToken cancellationToken)
    {
        await _batchRepository.DeleteAsync(batchId, cancellationToken);
    }
}

public interface IBatchService
{
    Task CreateAsync(CreateBatchModel batchModel, CancellationToken cancellationToken);
    Task<BatchModel?> GetByIdAsync(Guid batchId, CancellationToken cancellationToken);
    Task<IEnumerable<BatchModel>> GetListAsync(CancellationToken cancellationToken);
    Task UpdateAsync(UpdateBatchModel batchModel, CancellationToken cancellationToken);
    Task DeleteAsync(Guid batchId, CancellationToken cancellationToken);
}