using FormulaOnce.Teams.Domain.Constructor;
using FormulaOnce.Teams.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Teams.Infrastructure.ConstructorRepository;

internal class ConstructorRepository : IConstructorRepository
{
    private readonly TeamsDbContext _dbContext;

    public ConstructorRepository(TeamsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Constructor>> GetAllConstructorsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Constructors
            .Include(c => c.Drivers)
            .Include(c => c.Stats.AllTimeSummary)
            .Include(c => c.Stats.SeasonStats.GrandPrixStats)
            .Include(c => c.Stats.SeasonStats.SprintStats)
            .AsNoTracking() // Recommended for Read-Only operations
            .ToListAsync(cancellationToken);
    }

    public async Task<Constructor?> GetConstructorByIdAsync(Guid constructorId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Constructors
            .Include(c => c.Drivers.Where(d => d.ConstructorId.Equals(constructorId)))
            .Include(c => c.Stats.AllTimeSummary)
            .Include(c => c.Stats.SeasonStats.GrandPrixStats)
            .Include(c => c.Stats.SeasonStats.SprintStats)
            .FirstOrDefaultAsync(x => x.Id == constructorId, cancellationToken);
    }

    public async Task<Constructor> CreateConstructorAsync(Constructor constructor,
        CancellationToken cancellationToken = default)
    {
        var entry = await _dbContext.Constructors.AddAsync(constructor, cancellationToken);
        return entry.Entity;
    }

    public async Task UpdateConstructorAsync(Constructor constructor, CancellationToken cancellationToken = default)
    {
        var existingConstructor = await _dbContext.Constructors
            .Include(x => x.Stats) // Ensure stats are loaded so they can be updated
            .FirstOrDefaultAsync(x => x.Id == constructor.Id, cancellationToken);

        if (existingConstructor is not null)
            existingConstructor.UpdateDetails(
                constructor.Name,
                constructor.BaseLocation,
                constructor.EstablishedYear,
                constructor.Stats);
    }

    public async Task<bool> DeleteConstructorAsync(Guid constructorId, CancellationToken cancellationToken)
    {
        // Fetch ONCE with everything needed for the delete operation
        var constructor = await _dbContext.Constructors
            .Include(c => c.Drivers) 
            .FirstOrDefaultAsync(x => x.Id == constructorId, cancellationToken);

        if (constructor == null) return false;
        if (constructor.Drivers is not null && constructor.Drivers.Any())
        {
            _dbContext.Drivers.RemoveRange(constructor.Drivers);
        }
        _dbContext.Constructors.Remove(constructor);
        return true;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (await GetConstructorByIdAsync(id, cancellationToken) is null) return false;

        return true;
    }
}