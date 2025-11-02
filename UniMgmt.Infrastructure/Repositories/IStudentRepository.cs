using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class IStudentRepository : IRepository<Student>
{
    private readonly AppDbContext  _context;
    private IRepository<Student> _repositoryImplementation;

    public IStudentRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Student?> GetByIdAsync(int? studentId)
    {
        return await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == studentId);
    }

    public Task<IEnumerable<Student>> GetAllAsync()
    {
        return _repositoryImplementation.GetAllAsync();
    }

    public Task<Student> GetByIdAsync(int id)
    {
        return _repositoryImplementation.GetByIdAsync(id);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return student;
    }
}

