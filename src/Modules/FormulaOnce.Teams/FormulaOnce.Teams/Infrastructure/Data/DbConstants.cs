namespace FormulaOnce.Teams.Infrastructure.Data;

public static class DbConstants
{
    public const string TeamSchema = "Teams";
    public const string DriversTable = "Drivers";
    public const string DriverCareerStats = "DriverStatistics";

    public static class ConstructorConstants
    {
        public const string ConstructorsTable = "Constructors";
        public const string ConstructorAllTimeStatsTable = "ConstructorAllTimeStatistics";
        public const string ConstructorSeasonStatsTable = "ConstructorSeasonStatistics";
        public const string ConstructorStatisticsTable = "ConstructorStatistics";
        public const string GrandPrixStatisticsTable = "GrandPrixStatistics";
        public const string SprintStatisticsTable = "SprintStatistics";
    }
}