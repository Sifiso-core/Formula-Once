using FormulaOnce.Teams.Domain;

namespace FormulaOnce.Teams.Services;

internal interface IDriverService
{
    Task<List<DriverDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<DriverDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddDriverAsync(DriverDto driver, CancellationToken cancellationToken);
    Task DeleteDriverAsync(Guid id, CancellationToken cancellationToken);
}