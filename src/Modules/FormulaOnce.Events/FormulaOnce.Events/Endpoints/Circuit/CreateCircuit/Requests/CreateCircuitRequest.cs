namespace FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;

public record CreateCircuitRequest(
    string Name,
    double LengthKm,
    CreateLocationRequest Location,
    List<CreateLandmarkRequest> Landmarks);