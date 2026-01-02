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
            Age = driver.Age,
            CareerStats = driver.CareerStats,
        };
    }
}