using FormulaOnce.Events.Domain.Circuit;
using FormulaOnce.Events.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Events.Infrastructure.Repositories.CircuitRepository;

public class CircuitRepository : ICircuitRepository
{
    private readonly EventsDbContext _dbContext;

    public CircuitRepository(EventsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Circuit?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbContext.Circuits
            .Include(c => c.Landmarks)
            .Include(c => c.LapRecord)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<IEnumerable<Circuit>> GetAllAsync(CancellationToken ct = default)
    {
        return await _dbContext.Circuits
            .Include(c => c.Landmarks)
            .Include(c => c.LapRecord)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Circuit circuit, CancellationToken ct = default)
    {
        await _dbContext.Circuits.AddAsync(circuit, ct);
    }

    public void Update(Circuit circuit)
    {
        _dbContext.Circuits.Update(circuit);
    }

    public void Delete(Circuit circuit)
    {
        _dbContext.Circuits.Remove(circuit);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _dbContext.SaveChangesAsync(ct);
    }
}