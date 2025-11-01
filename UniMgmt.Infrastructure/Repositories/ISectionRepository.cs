using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class ISectionRepository : IRepository<Section>
{
    private readonly AppDbContext  _context;
    private IRepository<Section> _repositoryImplementation;

    public ISectionRepository(AppDbContext context)
    {
        _context = context;
    }
        
    public async Task<IEnumerable<Section>> GetAllAsync()
    {
        return await _context.Sections.ToListAsync();
    }

    public Task<Section> GetByIdAsync(int id)
    {
        return _repositoryImplementation.GetByIdAsync(id);
    }

    public async Task<Section?> GetByIDAsync(int? sectionId)
    {
        return await _context.Sections
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == sectionId);
    }

    public async Task<Section> CreateAsync(Section section)
    {
        _context.Sections.Add(section);
        await _context.SaveChangesAsync();
        return section;
    }

    public async Task<Section> UpdateAsync(Section section)
    {
        _context.Sections.Update(section);
        await _context.SaveChangesAsync();
        return section;
    }

    public async Task<Section> DeleteAsync(Section section)
    {
        _context.Sections.Remove(section);
        await _context.SaveChangesAsync();
        return section;
    }
}
