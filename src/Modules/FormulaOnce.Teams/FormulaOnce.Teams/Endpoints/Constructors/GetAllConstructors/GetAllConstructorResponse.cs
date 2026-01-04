using FormulaOnce.Teams.Services.ConstructorServices.Dto;

namespace FormulaOnce.Teams.Endpoints.Constructors.GetAllConstructors;

public class GetAllConstructorResponse
{
    public required List<ConstructorDto> Constructors { get; set; } = [];
}