using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaService.DataAccess.Batches;
using PharmaService.DataAccess.Pharmacies;
using PharmaService.DataAccess.PostgresSql.Repositories.Batches;
using PharmaService.DataAccess.PostgresSql.Repositories.Pharmacies;
using PharmaService.DataAccess.PostgresSql.Repositories.Products;
using PharmaService.DataAccess.PostgresSql.Repositories.Warehouses;
using PharmaService.DataAccess.Products;
using PharmaService.DataAccess.Warehouses;

namespace PharmaService.DataAccess.PostgresSql;

public static class ServiceCollectionExtension
{
    public static void AddPostgresSqlCompanySchemaMigrator(this IServiceCollection collection, string connectionString)
    {
        collection.AddDbContext<DataAccessSchemaMigratorDbContext>(builder =>
            builder.UseNpgsql(connectionString));
        collection.AddScoped<IDataAccessSchemaMigrator, DataAccessSchemaMigrator>();
    }

    public static void AddRepositories(this IServiceCollection collection, string connectionString)
    {
        collection.AddPostgresSqlRepositories<IBatchRepository, BatchRepository, PharmaDbContext>(connectionString);
        collection.AddPostgresSqlRepositories<IProductRepository, ProductRepository, PharmaDbContext>(connectionString);
        collection.AddPostgresSqlRepositories<IWarehouseRepository, WarehouseRepository, PharmaDbContext>(connectionString);
        collection.AddPostgresSqlRepositories<IPharmacyRepository, PharmacyRepository, PharmaDbContext>(connectionString);
    }

    private static void AddPostgresSqlRepositories<TRepository, TRepositoryImplementation, TRepositoryDbContext>(
        this IServiceCollection collection, string connectionString)
        where TRepository : class
        where TRepositoryImplementation : class, TRepository
        where TRepositoryDbContext : DbContext
    {
        collection.AddDbContextFactory<TRepositoryDbContext>(builder =>
            builder.UseNpgsql(connectionString)
        );
        collection.AddScoped<TRepository, TRepositoryImplementation>();
    }
}