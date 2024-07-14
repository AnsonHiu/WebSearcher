using AutoMapper;
using Domain.Common.Mappings;

namespace Domain.UnitTests.Common.Mappings;

public class DomainMappingTests
{
    [Fact]
    public void GivenDomainMapping_WhenAssert_IsValid()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<DomainMapping>());
        configuration.AssertConfigurationIsValid();
    }
}
