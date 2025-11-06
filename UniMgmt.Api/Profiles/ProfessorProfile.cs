using AutoMapper;
using UniMgmt.Domain.Entities;
using UniMgmt.Api.Dtos.Professor;

namespace UniMgmt.Api.Profiles;

public class ProfessorProfile : Profile
{
    public ProfessorProfile()
    {
        CreateMap<Professor, ProfessorDto>();
        CreateMap<CreateProfessorDto, Professor>();
        CreateMap<UpdateProfessorDto, Professor>();
    }
}