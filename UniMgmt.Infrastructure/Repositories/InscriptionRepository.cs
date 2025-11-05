using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class InscriptionRepository : Repository<Inscription>, IInscriptionRepository
{
    public InscriptionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> AnySectionByIdAsync(int sectionId)
    {
        return await _context.Sections.AnyAsync(s => s.Id == sectionId);
    }

    public async Task<bool> AnyStudentByIdAsync(int studentId)
    {
        return await _context.Students.AnyAsync(s => s.Id == studentId);
    }

    public async Task<bool> AnyEnrollmentAsync(int sectionId, int studentId)
    {
        return await _dbSet.AnyAsync(i => i.SectionId == sectionId && i.StudentId == studentId);
    }
    
    public async Task<IEnumerable<Inscription>> GetInscriptionsByStudentIdAsync(int studentId)
    {
        return await _dbSet
            .Where(i => i.StudentId == studentId)
            .Include(i => i.Section)
            .ToListAsync();
    }
}