using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace FormulaOnce.Events.Tests;

public class InfrastructureDependencyTest
{
    private static readonly Architecture Architecture =
        new ArchLoader().LoadAssemblies(typeof(AssemblyInfo).Assembly).Build();

    [Fact]
    public void DomainTypes_ShouldNot_ReferenceInfrastructure()
    {
        var domainTypes = Types().That().ResideInNamespaceMatching("FormulaOnce.Events.Domain.*")
            .As("FormulaOnce Domain Types");

        var infrastructureTypes = Types().That().ResideInNamespaceMatching("FormulaOnce.Events.Infrastructure.*")
            .As("FormulaOnce Infrastructure Types");

        var rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);

        rule.Check(Architecture);
    }
}