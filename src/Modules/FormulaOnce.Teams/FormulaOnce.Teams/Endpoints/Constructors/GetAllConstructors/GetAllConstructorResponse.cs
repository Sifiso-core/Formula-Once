using FormulaOnce.Teams.Endpoints.Constructors._Dtos;

namespace FormulaOnce.Teams.Endpoints.Constructors.GetAllConstructors;

public class GetAllConstructorResponse
{
    public required List<ConstructorDto> Constructors { get; set; } = [];
}