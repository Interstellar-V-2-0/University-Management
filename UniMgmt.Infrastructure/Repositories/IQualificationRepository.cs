using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class IQualificationRepository : IRepository<Qualification>
{
    private readonly AppDbContext  _context;
    private IRepository<Qualification> _repositoryImplementation;

    public IQualificationRepository(AppDbContext context)
    {
        _context = context;
    }
        
    public async Task<IEnumerable<Qualification>> GetAllAsync()
    {
        return await _context.Qualifications.ToListAsync();
    }

    public Task<Qualification> GetByIdAsync(int id)
    {
        return _repositoryImplementation.GetByIdAsync(id);
    }

    public async Task<Qualification?> GetByIDAsync(int? qualificationId)
    {
        return await _context.Qualifications
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == qualificationId);
    }

    public async Task<Qualification> CreateAsync(Qualification qualification)
    {
        _context.Qualifications.Add(qualification);
        await _context.SaveChangesAsync();
        return qualification;
    }

    public async Task<Qualification> UpdateAsync(Qualification qualification)
    {
        _context.Qualifications.Update(qualification);
        await _context.SaveChangesAsync();
        return qualification;
    }

    public async Task<Qualification> DeleteAsync(Qualification qualification)
    {
        _context.Qualifications.Remove(qualification);
        await _context.SaveChangesAsync();
        return qualification;
    } 
}
