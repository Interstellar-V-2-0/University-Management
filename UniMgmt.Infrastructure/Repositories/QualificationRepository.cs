using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class QualificationRepository : Repository<Qualification>, IQualificationRepository
{
    public QualificationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Qualification?> GetByInscriptionIdAsync(int inscriptionId)
    {
        return await _dbSet.FirstOrDefaultAsync(i => i.Id == inscriptionId);
    }
}