using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories;

public class IInscriptionRepository : IRepository<Inscription>
{
    private readonly AppDbContext  _context;
    private IRepository<Inscription> _repositoryImplementation;

    public IInscriptionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Inscription>> GetAllAsync()
    {
        return await _context.Inscriptions.ToListAsync();
    }

    public Task<Inscription> GetByIdAsync(int id)
    {
        return _repositoryImplementation.GetByIdAsync(id);
    }

    public async Task<Inscription?> GetByIDAsync(int? inscriptionId)
    {
        return await _context.Inscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == inscriptionId);
    }

    public async Task<Inscription> CreateAsync(Inscription inscription)
    {
        _context.Inscriptions.Add(inscription);
        await _context.SaveChangesAsync();
        return inscription;
    }

    public async Task<Inscription> UpdateAsync(Inscription inscription)
    {
        _context.Inscriptions.Update(inscription);
        await _context.SaveChangesAsync();
        return inscription;
    }

    public async Task<Inscription> DeleteAsync(Inscription inscription)
    {
        _context.Inscriptions.Remove(inscription);
        await _context.SaveChangesAsync();
        return inscription;
    }
}