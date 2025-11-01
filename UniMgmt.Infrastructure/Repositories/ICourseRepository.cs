using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class ICourseRepository : IRepository<Course>
{
    private readonly AppDbContext  _context;
    private IRepository<Course> _repositoryImplementation;

    public ICourseRepository(AppDbContext context)
    {
        _context = context;
    }
        
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public Task<Course> GetByIdAsync(int id)
    {
        return _repositoryImplementation.GetByIdAsync(id);
    }

    public async Task<Course?> GetByIDAsync(int? courseId)
    {
        return await _context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == courseId);
    }

    public async Task<Course> CreateAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> DeleteAsync(Course course)
    {
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return course;
    }
}