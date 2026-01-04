namespace FormulaOnce.Events.Infrastructure.Data;

public static class DbConstants
{
    public const string Schema = "Events";

    public static class LocationConstants
    {
        public const string LatitudeColumnName = "Latitude";
        public const string LongitudeColumnName = "Longitude";
        public const string CountryColumnName = "Country";
        public const string CityColumnName = "City";
    }

    public static class LapRecordConstants
    {
        public const string TableName = "CircuitLapRecords";
        public const string TimeColumnName = "LapTime";
        public const string DriverColumnName = "DriverName";
    }

    public static class CircuitLandmarkConstants
    {
        public const string TableName = "CircuitLandmarks";
    }


    public static class RaceConstants
    {
        public const string TableName = "Races";
    }

    public static class SessionConstants
    {
        public const string TableName = "RaceSessions";
    }
}