using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class SectionRepository : Repository<Section>, ISectionRepository
{
    public SectionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> GetCurrentEnrollmentCountAsync(int sectionId)
    {
        return await _context.Inscriptions.CountAsync(i => i.SectionId == sectionId);
    }

    public async Task<int> GetMaxCapacityAsync(int sectionId)
    {
        return await _dbSet
            .Where(s => s.Id == sectionId)
            .Select(s => s.CapacityMax)
            .FirstOrDefaultAsync();
    }
}