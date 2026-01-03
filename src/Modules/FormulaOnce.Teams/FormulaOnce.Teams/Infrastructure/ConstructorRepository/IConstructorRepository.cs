using FormulaOnce.Teams.Domain.Constructor;

namespace FormulaOnce.Teams.Infrastructure.ConstructorRepository;

internal interface IConstructorRepository
{
    Task<IEnumerable<Constructor>> GetAllConstructorsAsync(CancellationToken cancellationToken = default);
    Task<Constructor?> GetConstructorByIdAsync(Guid constructorId, CancellationToken cancellationToken = default);
    Task<Constructor> CreateConstructorAsync(Constructor constructor, CancellationToken cancellationToken = default);
    Task UpdateConstructorAsync(Constructor constructor, CancellationToken cancellationToken = default);
    Task<bool> DeleteConstructorAsync(Guid constructorId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}