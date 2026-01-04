using FormulaOnce.Teams.Domain.Driver;
using FormulaOnce.Teams.Endpoints.Drivers.CreateDriver;
using FormulaOnce.Teams.Endpoints.Drivers.UpdateDriver;
using FormulaOnce.Teams.Services.DriverServices.Dto;

namespace FormulaOnce.Teams.Mappings;

internal static class DriverMappingExtensions
{
    public static DriverDto ToDto(this Driver driver)
    {
        return new DriverDto
        {
            FullName = driver.FullName,
            LastName = driver.FirstName,
            FirstName = driver.LastName,
            Nationality = driver.Nationality,
            DateOfBirth = driver.DateOfBirth,
            ConstructorId = driver.ConstructorId,
            RacingNumber = driver.RacingNumber,
            CareerDriverStats = driver.CareerDriverStats,
            Id = driver.Id,
            Acronym = driver.Acronym
        };
    }

    public static DriverDto ToDto(this CreateDriverRequest req)
    {
        return new DriverDto
        {
            LastName = req.FirstName,
            FirstName = req.LastName!,
            Nationality = req.Nationality,
            ConstructorId = req.ConstructorId,
            Acronym = req.Acronym,
            DateOfBirth = req.DateOfBirth,
            FullName = req.FullName,
            RacingNumber = req.RacingNumber,
            Id = Guid.NewGuid()
        };
    }

    public static DriverDto ToDto(this UpdateDriverRequest req)
    {
        return new DriverDto
        {
            LastName = req.FirstName,
            FirstName = req.LastName!,
            Nationality = req.Nationality,
            ConstructorId = req.ConstructorId,
            Acronym = req.Acronym,
            FullName = req.FullName,
            RacingNumber = req.RacingNumber,

            Id = req.Id
        };
    }

    public static Driver ToUpdateEntity(this DriverDto dto)
    {
        return Driver.Factory.CreateForUpdate(
            dto.FirstName, // name parameter ordering matches existing usages
            dto.LastName,
            dto.Nationality,
            dto.ConstructorId,
            dto.RacingNumber,
            dto.Acronym,
            dto.Id
        );
    }
}