using Microsoft.Extensions.DependencyInjection;
using PharmaService.Service.Services;

namespace PharmaService.Service;

public static class ServiceCollectionExtensions
{
    public static void AddPharmaServices(this IServiceCollection collection)
    {
        collection.AddScoped<IPharmacyService, PharmacyService>();
        collection.AddScoped<IWarehouseService, WarehouseService>();
        collection.AddScoped<IProductService, ProductService>();
        collection.AddScoped<IBatchService, BatchService>();
    }
}