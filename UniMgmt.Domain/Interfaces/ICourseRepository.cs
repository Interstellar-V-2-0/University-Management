using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<IEnumerable<Course>> GetCoursesByProfessorIdAsync(int professorId);
}