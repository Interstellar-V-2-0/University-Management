using AutoMapper;
using UniMgmt.Domain.Entities;
using UniMgmt.Api.Dtos.Course;

namespace UniMgmt.Api.Profiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<Course, CourseDto>();
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();
    }
}