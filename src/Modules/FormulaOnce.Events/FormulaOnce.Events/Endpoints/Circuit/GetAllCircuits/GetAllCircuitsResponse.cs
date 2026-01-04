using FormulaOnce.Events.Services.CircuitService.Dto;

namespace FormulaOnce.Events.Endpoints.Circuit.GetAllCircuits;

public class GetAllCircuitsResponse
{
    public required IEnumerable<CircuitDto> Circuits { get; set; }
}