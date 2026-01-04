using Ardalis.Result;
using FormulaOnce.Teams.Domain.Driver;
using FormulaOnce.Teams.Infrastructure.ConstructorRepository;
using FormulaOnce.Teams.Infrastructure.DriverRepository;
using FormulaOnce.Teams.Mappings;
using FormulaOnce.Teams.Services.DriverServices.Dto;

namespace FormulaOnce.Teams.Services.DriverServices;

internal class DriverService : IDriverService
{
    private readonly IConstructorRepository _constructorRepository; // Added for validation
    private readonly IDriverRepository _driverRepository;

    public DriverService(IDriverRepository driverRepository, IConstructorRepository constructorRepository)
    {
        _driverRepository = driverRepository;
        _constructorRepository = constructorRepository;
    }

    public async Task<Result<List<DriverDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var drivers = await _driverRepository.GetAllAsync(cancellationToken);
        return Result.Success(drivers.Select(d => d.ToDto()).ToList());
    }

    public async Task<Result<DriverDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(id, cancellationToken);
        if (driver is null) return Result.NotFound();

        return Result.Success(driver.ToDto());
    }

    public async Task<Result<DriverDto>> AddDriverAsync(DriverDto dto, CancellationToken ct)
    {
        var constructorExists = await _constructorRepository.ExistsAsync(dto.ConstructorId, ct);
        if (!constructorExists)
            return Result.Invalid(new ValidationError
            {
                Identifier = nameof(dto.ConstructorId),
                ErrorMessage = "The specified Constructor does not exist."
            });

        // 2. Create via Factory
        var driver = Driver.Factory.Create(dto.FirstName, dto.LastName, dto.Nationality,
            dto.ConstructorId, dto.RacingNumber, dto.DateOfBirth, dto.Acronym, dto.Id);

        await _driverRepository.AddDriverAsync(driver, ct);
        await _driverRepository.SaveChangesAsync(ct);

        return Result.Success(driver.ToDto());
    }

    public async Task<Result> UpdateDriverAsync(DriverDto dto, CancellationToken ct)
    {
        var existing = await _driverRepository.GetByIdAsync(dto.Id, ct);
        if (existing is null) return Result.NotFound();

        await _driverRepository.UpdateDriverAsync(dto.ToUpdateEntity(), ct);

        await _driverRepository.SaveChangesAsync(ct);

        return Result.Success();
    }


    public async Task<Result> DeleteDriverAsync(Guid id, CancellationToken ct)
    {
        var existingDriver = await _driverRepository.GetByIdAsync(id, ct);
        if (existingDriver is null) return Result.NotFound();

        await _driverRepository.DeleteDriverAsync(id, ct);
        await _driverRepository.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _driverRepository.SaveChangesAsync(cancellationToken);
    }
}