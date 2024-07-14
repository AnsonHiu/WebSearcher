using Application.Models;
using AutoMapper;
using DomainEntity = Domain.Entities;

namespace Application.Common.Mappings;

/// <summary>
/// Maps between Domain Models and Application Models
/// </summary>
class ApplicationMapping : Profile
{
    public ApplicationMapping()
    {
        CreateMap<DomainEntity.UrlLocation, UrlLocation>()
            .ForCtorParam(nameof(UrlLocation.Location), opt => opt.MapFrom(src => src.Location.ToString()));
    }
}
