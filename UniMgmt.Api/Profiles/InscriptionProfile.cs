using AutoMapper;
using UniMgmt.Domain.Entities;
using UniMgmt.Api.Dtos.Inscription;

namespace UniMgmt.Api.Profiles;

public class InscriptionProfile : Profile
{
    public InscriptionProfile()
    {
        CreateMap<Inscription, InscriptionDto>();
        CreateMap<CreateInscriptionDto, Inscription>();
        CreateMap<Inscription, InscriptionDetailDto>();
    }
}