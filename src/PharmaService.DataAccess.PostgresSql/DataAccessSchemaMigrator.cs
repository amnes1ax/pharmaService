using Microsoft.EntityFrameworkCore;

namespace PharmaService.DataAccess.PostgresSql;

public class DataAccessSchemaMigrator : IDataAccessSchemaMigrator
{
    private readonly DataAccessSchemaMigratorDbContext _context;

    public DataAccessSchemaMigrator(DataAccessSchemaMigratorDbContext context) => _context = context;

    public async Task MigrateAsync() => await _context.Database.MigrateAsync();
}

public interface IDataAccessSchemaMigrator
{
    Task MigrateAsync();
}