using FormulaOnce.Teams.Domain;
using FormulaOnce.Teams.Infrastructure;
using FormulaOnce.Teams.Mappings;

namespace FormulaOnce.Teams.Services;

internal class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;

    public DriverService(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }

    public async Task<List<DriverDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var drivers = await _driverRepository.GetAllAsync(cancellationToken);
        return drivers.Select(d => d.AsDto()).ToList();
    }

    public async Task<DriverDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(id, cancellationToken);
        return driver is null ? null : driver!.AsDto();
    }

    public async Task AddDriverAsync(DriverDto driverDto, CancellationToken cancellationToken)
    {
        var names = driverDto.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var firstName = names[0];

        var lastName = names.Length > 1 ? names[1] : string.Empty;

        var driver = Driver.Factory.Create(firstName, lastName, driverDto.Nationality, driverDto.ConstructorId,
            driverDto.RacingNumber, driverDto.DateOfBirth, driverDto.Acronym, driverDto.Id);

        await _driverRepository.AddDriverAsync(driver, cancellationToken);
    }

    public async Task DeleteDriverAsync(Guid id, CancellationToken cancellationToken)
    {
        await _driverRepository.DeleteDriverAsync(id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _driverRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateDriverAsync(DriverDto driverDto, CancellationToken cancellationToken)
    {
        var driver = await GetByIdAsync(driverDto.Id, cancellationToken);
        if (driver is not null)
        {
            await _driverRepository.UpdateDriverAsync(driverDto.AsEntity(), cancellationToken);
        }
    }
}