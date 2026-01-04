using FormulaOnce.Events.Domain.Race;

namespace FormulaOnce.Events.Infrastructure.Repositories.RaceRepository;

public interface IRaceRepository
{
    Task<Race?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Race>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Race>> GetBySeasonAsync(int season, CancellationToken ct = default);
    Task AddAsync(Race race, CancellationToken ct = default);
    void Delete(Race race);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}