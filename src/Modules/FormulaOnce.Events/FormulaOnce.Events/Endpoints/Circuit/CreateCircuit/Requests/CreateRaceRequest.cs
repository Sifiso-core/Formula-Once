namespace FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;

public record CreateRaceRequest(
    string Title,
    int Season,
    int Round,
    Guid CircuitId,
    int NumberOfLaps,
    DateTime MainRaceStartTime,
    bool IsSprintWeekend);