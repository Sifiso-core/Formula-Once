using FormulaOnce.Events.Domain.Circuit;

namespace FormulaOnce.Events.Infrastructure.Repositories.CircuitRepository;

public interface ICircuitRepository
{
    Task<Circuit?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Circuit>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Circuit circuit, CancellationToken ct = default);
    void Delete(Circuit circuit);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}