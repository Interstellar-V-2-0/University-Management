using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Course>> GetCoursesByProfessorIdAsync(int professorId)
    {
        return await _dbSet.Where(c => c.ProfessorId == professorId).ToListAsync();
    }
}