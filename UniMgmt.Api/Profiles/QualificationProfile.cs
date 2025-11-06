using AutoMapper;
using UniMgmt.Domain.Entities;
using UniMgmt.Api.Dtos.Qualification;

namespace UniMgmt.Api.Profiles;

public class QualificationProfile : Profile
{
    public QualificationProfile()
    {
        CreateMap<Qualification, QualificationDto>();
        CreateMap<CreateQualificationDto, Qualification>();
        CreateMap<UpdateQualificationDto, Qualification>();
    }
}