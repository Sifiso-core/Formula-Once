using FormulaOnce.Events.Domain.Circuit;

namespace FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;

public record CreateLandmarkRequest(
    string Label,
    LandmarkType Type,
    int NearTurn);