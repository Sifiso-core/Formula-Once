using FormulaOnce.Teams.Domain.Driver;
using FormulaOnce.Teams.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOnce.Teams.Infrastructure.DriverRepository;

internal class DriverRepository : IDriverRepository
{
    private readonly TeamsDbContext _context;

    public DriverRepository(TeamsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Driver>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Drivers
            .Include(d => d.Constructor)
            .Include(d => d.CareerDriverStats)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // Switching to FirstOrDefaultAsync because FindAsync doesn't support .Include()
        return await _context.Drivers
            .Include(d => d.Constructor)
            .Include(d => d.CareerDriverStats)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<bool> AddDriverAsync(Driver driver, CancellationToken cancellationToken)
    {
        var constructor = await _context.Constructors.FindAsync([driver.ConstructorId], cancellationToken);
        if (constructor is null) return false;
        await _context.Drivers.AddAsync(driver, cancellationToken);
        return true;
    }

    public async Task DeleteDriverAsync(Guid id, CancellationToken cancellationToken)
    {
        var driver = await _context.Drivers.FindAsync([id], cancellationToken);
        if (driver != null) _context.Drivers.Remove(driver);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDriverAsync(Driver driver, CancellationToken cancellationToken)
    {
        var driverToUpdate = await GetByIdAsync(driver.Id, cancellationToken);

        if (driverToUpdate is not null)
            // Using your new UpdateDetails method
            driverToUpdate.UpdateDetails(
                driver.FirstName,
                driver.LastName,
                driver.ConstructorId,
                driver.RacingNumber,
                driver.Acronym);
    }
}