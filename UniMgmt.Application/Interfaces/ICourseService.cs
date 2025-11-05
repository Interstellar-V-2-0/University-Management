using UniMgmt.Domain.Entities;

namespace UniMgmt.Application.Interfaces;

public interface ICourseService : IService<Course>
{
    Task<IEnumerable<Course>> GetCoursesByProfessorIdAsync(int professorId);
}