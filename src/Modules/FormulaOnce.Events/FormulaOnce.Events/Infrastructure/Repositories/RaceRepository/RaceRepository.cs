using FormulaOnce.Events.Domain.Race;
using FormulaOnce.Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Events.Infrastructure.Repositories.RaceRepository;

public class RaceRepository : IRaceRepository
{
    private readonly EventsDbContext _dbContext;

    public RaceRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Race?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Races
            .Include(r => r.Sessions)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<IEnumerable<Race>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Races
            .Include(r => r.Circuit)
            .Include(r => r.Sessions)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<Race>> GetBySeasonAsync(int season, CancellationToken ct = default)
    {
        return await _dbContext.Races
            .Include(r => r.Sessions)
            .Where(r => r.Season == season)
            .OrderBy(r => r.Round)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Race race, CancellationToken ct = default)
    {
        await _dbContext.Races.AddAsync(race, ct);
    }

    public void Delete(Race race)
    {
        _dbContext.Races.Remove(race);
    }


    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }
}