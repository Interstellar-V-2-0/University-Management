using AutoMapper;
using UniMgmt.Domain.Entities;
using UniMgmt.Api.Dtos.Section;

namespace UniMgmt.Api.Profiles;

public class SectionProfile : Profile
{
    public SectionProfile()
    {
        CreateMap<Section, SectionDto>();
        CreateMap<CreateSectionDto, Section>();
        CreateMap<UpdateSectionDto, Section>();
    }
}
