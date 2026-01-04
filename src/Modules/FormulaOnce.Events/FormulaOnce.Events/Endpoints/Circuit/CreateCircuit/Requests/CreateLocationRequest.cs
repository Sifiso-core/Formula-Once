namespace FormulaOnce.Events.Endpoints.Circuit.CreateCircuit.Requests;

public record CreateLocationRequest(
    string Country,
    string City,
    double Latitude,
    double Longitude);