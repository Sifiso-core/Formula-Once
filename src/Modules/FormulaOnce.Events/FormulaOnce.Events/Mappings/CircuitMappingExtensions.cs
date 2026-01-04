using FormulaOnce.Events.Domain.Circuit;
using FormulaOnce.Events.Services.CircuitService.Dto;

namespace FormulaOnce.Events.Mappings;

public static class CircuitMappingExtensions
{
    public static CircuitDto ToDto(this Circuit circuit)
    {
        return new CircuitDto(
            circuit.Id,
            circuit.Name,
            circuit.LengthKm,
            new LocationDto(
                circuit.Location.Country,
                circuit.Location.City,
                circuit.Location.Coordinates.Latitude,
                circuit.Location.Coordinates.Longitude),
            circuit.LapRecord?.ToDto(),
            circuit.Landmarks.Select(l => l.ToDto()).ToList()
        );
    }

    public static CircuitRecordDto ToDto(this CircuitRecord record)
    {
        // Format TimeSpan to "mm:ss.fff" for the UI
        return new CircuitRecordDto(
            record.Time.ToString(@"m\:ss\.fff"),
            record.DriverName,
            record.Year);
    }

    public static LandmarkDto ToDto(this TrackLandmark landmark)
    {
        return new LandmarkDto(landmark.Label, landmark.LandmarkType.ToString(), landmark.NearTurn);
    }
}