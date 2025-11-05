using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class ProfessorRepository : Repository<Professor>, IProfessorRepository
{
    public ProfessorRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Professor?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Email == email);
    }
}