using FormulaOnce.Teams.Domain.Driver;

namespace FormulaOnce.Teams.Infrastructure.DriverRepository;

internal interface IDriverRepository
{
    Task<List<Driver>> GetAllAsync(CancellationToken cancellationToken);
    Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> AddDriverAsync(Driver driver, CancellationToken cancellationToken);
    Task DeleteDriverAsync(Guid id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task UpdateDriverAsync(Driver driver, CancellationToken cancellationToken);
}