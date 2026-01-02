using FormulaOnce.Teams.Domain;

namespace FormulaOnce.Teams.Mappings;

internal static class DriverMappingExtensions
{
    public static DriverDto AsDto(this Driver driver)
    {
        return new DriverDto()
        {
            FullName = driver.FullName,
            Nationality = driver.Nationality,
            DateOfBirth = driver.DateOfBirth,
            ConstructorId = driver.ConstructorId,
            RacingNumber = driver.RacingNumber,
            CareerStats = driver.CareerStats,
            Id = driver.Id,
            Acronym = driver.Acronym,
        };
    }

    public static Driver AsEntity(this DriverDto driverDto)
    {
        var names = driverDto.FullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var firstName = names[0];

        var lastName = names.Length > 1 ? names[1] : string.Empty;

        return Driver.Factory.Create(firstName, lastName, driverDto.Nationality, driverDto.ConstructorId,
            driverDto.RacingNumber, driverDto.DateOfBirth, driverDto.Acronym, driverDto.Id);
    }
}