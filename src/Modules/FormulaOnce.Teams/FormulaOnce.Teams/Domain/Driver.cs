namespace FormulaOnce.Teams.Domain;

public class Driver
{
    private Driver()
    {
        //For EF Core
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Surname { get; private set; } = string.Empty;
    public string FullName => $"{Name} {Surname}";

    public string Abbreviation { get; private set; } = string.Empty;
    public int PermanentNumber { get; private set; }
    public int? RacingNumber { get; private set; }

    public string Nationality { get; private set; } = string.Empty;
    public string? PlaceOfBirth { get; private set; }
    public DateTime DateOfBirth { get; init; }
    public Guid ConstructorId { get; private set; }
    public int Age { get; private set; }
    public Stats CareerStats { get; private set; } = null!;

    
}