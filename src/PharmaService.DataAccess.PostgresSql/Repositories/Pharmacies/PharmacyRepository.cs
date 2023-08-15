using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Pharmacies;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Pharmacies;

public class PharmacyRepository : IPharmacyRepository
{
    private readonly PharmaDbContext _context;
    
    public PharmacyRepository(PharmaDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken = default)
    {
        await _context.Pharmacies.AddAsync(pharmacy, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Pharmacy?> GetByIdAsync(Guid pharmacyId, bool withRelations = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Pharmacies
            .Where(x => x.Id == pharmacyId);
        if (withRelations)
        {
            await query
                .Include(x => x.Warehouses)
                .LoadAsync(cancellationToken);
        }
        else
        {
            query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Pharmacy>> GetListAsync(bool withRelations = false, CancellationToken cancellationToken = default)
    {
        var query = _context.Pharmacies;
        if (withRelations)
        {
            await query
                .Include(x => x.Warehouses)
                .LoadAsync(cancellationToken: cancellationToken);
        }
        else
        {
            query.AsNoTracking();
        }
        return await query.ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Pharmacy pharmacy, CancellationToken cancellationToken = default)
    {
        _context.Pharmacies.Remove(pharmacy);
        await _context.SaveChangesAsync(cancellationToken);
    }
}