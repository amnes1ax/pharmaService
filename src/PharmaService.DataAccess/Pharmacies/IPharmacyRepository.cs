using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.Pharmacies;

public interface IPharmacyRepository
{
    public Task AddAsync(Pharmacy pharmacy, CancellationToken cancellationToken = default);
    public Task<Pharmacy?> GetByIdAsync(Guid pharmacyId, bool withRelations = false,
        CancellationToken cancellationToken = default);
    public Task<IEnumerable<Pharmacy>> GetListAsync(bool withRelations = false,
        CancellationToken cancellationToken = default);
    public Task UpdateAsync(Pharmacy pharmacy, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Pharmacy pharmacy, CancellationToken cancellationToken = default);
}