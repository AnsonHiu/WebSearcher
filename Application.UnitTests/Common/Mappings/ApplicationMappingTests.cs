using AutoMapper;
using Application.Common.Mappings;

namespace Application.UnitTests.Common.Mappings;

public class ApplicationMappingTests
{
    [Fact]
    public void GivenApplicationMapping_WhenAssert_IsValid()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ApplicationMapping>());
        configuration.AssertConfigurationIsValid();
    }
}
