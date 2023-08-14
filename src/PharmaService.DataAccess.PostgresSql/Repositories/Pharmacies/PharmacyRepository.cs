using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.Pharmacies;
using PharmaService.DataAccess.Pharmacies.Exceptions;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Pharmacies;

public class PharmacyRepository : IPharmacyRepository
{
    private readonly PharmaDbContext _context;
    
    public PharmacyRepository(PharmaDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken)
    {
        await _context.Pharmacies.AddAsync(pharmacy, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Pharmacy?> GetByIdAsync(Guid pharmacyId, CancellationToken cancellationToken) =>
        await _context.Pharmacies.AsNoTracking().Where(x => x.Id == pharmacyId).FirstOrDefaultAsync(cancellationToken);

    public async Task<IEnumerable<Pharmacy>> GetListAsync(CancellationToken cancellationToken) =>
        await _context.Pharmacies.AsNoTracking().ToListAsync(cancellationToken);

    public async Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid pharmacyId, CancellationToken cancellationToken)
    {
        var existingPharmacy = await GetByIdAsync(pharmacyId, cancellationToken);
        if (existingPharmacy is null) throw new PharmacyNotFoundException();
        _context.Pharmacies.Remove(existingPharmacy);
        await _context.SaveChangesAsync(cancellationToken);
    }
}