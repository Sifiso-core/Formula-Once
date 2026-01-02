using FormulaOnce.Teams.Domain;

namespace FormulaOnce.Teams.Infrastructure;

internal interface IDriverRepository
{
    Task<List<Driver>> GetAllAsync(CancellationToken cancellationToken);
    Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddDriverAsync(Driver driver, CancellationToken cancellationToken);
    Task DeleteDriverAsync(Guid id, CancellationToken cancellationToken);
}