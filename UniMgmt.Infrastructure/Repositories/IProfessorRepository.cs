using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class IProfessorRepository : IRepository<Professor>
{
    private readonly AppDbContext  _context;
    private IRepository<Professor> _repositoryImplementation;

    public IProfessorRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Professor>> GetAllAsync()
    {
        return await _context.Professors.ToListAsync();
    }

    public Task<Professor> GetByIdAsync(int id)
    {
        return _repositoryImplementation.GetByIdAsync(id);
    }

    public async Task<Professor?> GetByIDAsync(int? professorId)
    {
        return await _context.Professors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == professorId);
    }

    public async Task<Professor> CreateAsync(Professor professor)
    {
        _context.Professors.Add(professor);
        await _context.SaveChangesAsync();
        return professor;
    }

    public async Task<Professor> UpdateAsync(Professor professor)
    {
        _context.Professors.Update(professor);
        await _context.SaveChangesAsync();
        return professor;
    }

    public async Task<Professor> DeleteAsync(Professor professor)
    {
        _context.Professors.Remove(professor);
        await _context.SaveChangesAsync();
        return professor;
    }
}
