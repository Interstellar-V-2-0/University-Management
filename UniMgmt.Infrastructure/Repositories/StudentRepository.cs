using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Student?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(s => s.Email == email);
    }
}