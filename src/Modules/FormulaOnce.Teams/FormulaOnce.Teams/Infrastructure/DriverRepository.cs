using FormulaOnce.Teams.Domain;
using FormulaOnce.Teams.Infrastructure.Data;
using FormulaOnce.Teams.Services;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Teams.Infrastructure;

internal class DriverRepository : IDriverRepository
{
    private readonly TeamsDbContext _context;

    public DriverRepository(TeamsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Driver>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Drivers.ToListAsync(cancellationToken);
    }

    public async Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync([id], cancellationToken);
        return driver ?? null;
    }

    public async Task AddDriverAsync(Driver driver, CancellationToken cancellationToken)
    {
        await _context.Drivers.AddAsync(driver, cancellationToken);
    }

    public async Task DeleteDriverAsync(Guid id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (driver != null)
        {
            _context.Drivers.Remove(driver);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDriverAsync(Driver driver, CancellationToken cancellationToken)
    {
        var driverToUpdate = await _context.Drivers.FindAsync([driver.Id], cancellationToken);
        if (driverToUpdate is not null)
        {
            driverToUpdate.UpdateDriver(driver);
        }
    }
}