using Ardalis.Result;
using FormulaOnce.Teams.Endpoints.Drivers._Dtos;

namespace FormulaOnce.Teams.Services.DriverServices;

internal interface IDriverService
{
    // Returns a Result containing the list, allowing for error states if needed
    Task<Result<List<DriverDto>>> GetAllAsync(CancellationToken cancellationToken);

    // No need for Nullable DTO; Result.Status will be NotFound if the driver doesn't exist
    Task<Result<DriverDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // Returns the created DTO with its generated ID
    Task<Result<DriverDto>> AddDriverAsync(DriverDto driver, CancellationToken cancellationToken);

    // Returns Result to indicate if deletion was successful or if ID was not found
    Task<Result> DeleteDriverAsync(Guid id, CancellationToken cancellationToken);

    // Returns Result to indicate if the driver to update existed and was valid
    Task<Result> UpdateDriverAsync(DriverDto driverDto, CancellationToken cancellationToken);

    // Standard SaveChanges (often called internally by the Service methods)
    Task SaveChangesAsync(CancellationToken cancellationToken);
}