using AutoMapper;
using UniMgmt.Domain.Entities;
using UniMgmt.Api.Dtos.Student;

namespace UniMgmt.Api.Profiles;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>();
        CreateMap<CreateStudentDto, Student>();
        CreateMap<UpdateStudentDto, Student>();
    }
}