using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Pharmacies;

public interface IPharmacyRepository
{
    public Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
    public Task<Pharmacy?> GetByIdAsync(Guid pharmacyId, CancellationToken cancellationToken);
    public Task<IEnumerable<Pharmacy>> GetListAsync(CancellationToken cancellationToken);
    public Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid pharmacyId, CancellationToken cancellationToken);
}