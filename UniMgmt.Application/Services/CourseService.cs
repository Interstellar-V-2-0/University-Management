using UniMgmt.Application.Interfaces;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;

namespace UniMgmt.Application.Services;

public class CourseService : Service<Course>, ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository) : base(courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> GetCoursesByProfessorIdAsync(int professorId)
    {
        return await _courseRepository.GetCoursesByProfessorIdAsync(professorId);
    }
}