using PharmaService.IntegrationClient.Http.Models.Batches;
using PharmaService.IntegrationClient.Http.Models.Pharmacies;
using PharmaService.IntegrationClient.Http.Models.Products;
using Refit;
using PharmaService.IntegrationClient.Http.Models.Warehouses;

namespace PharmaService.IntegrationClient.Http;

public interface IPharmaServiceClient
{
    /// <summary>
    /// Get warehouses
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/warehouse")]
    Task<ApiResponse<IEnumerable<WarehouseModel>>> GetWarehousesListAsync(
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get warehouse by id
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/warehouse/{warehouseId}")]
    Task<ApiResponse<WarehouseModel>> GetWarehouseByIdAsync(
        Guid warehouseId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get pharmacies
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/pharmacy")]
    Task<ApiResponse<IEnumerable<PharmacyModel>>> GetPharmaciesListAsync(
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get pharmacy by id
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/pharmacy/{pharmacyId}")]
    Task<ApiResponse<PharmacyModel>> GetPharmacyByIdAsync(
        Guid pharmacyId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get products
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/product")]
    Task<ApiResponse<IEnumerable<ProductModel>>> GetProductsListAsync(
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get product by id
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/product/{productId}")]
    Task<ApiResponse<ProductModel>> GetProductByIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get batches
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/batch")]
    Task<ApiResponse<IEnumerable<BatchModel>>> GetBatchesListAsync(
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get batch by id
    /// </summary>
    /// <returns>
    /// 200 - Ok
    /// </returns>
    [Get("/api/batch/{batchId}")]
    Task<ApiResponse<BatchModel>> GetBatchByIdAsync(
        Guid batchId,
        CancellationToken cancellationToken = default);
}