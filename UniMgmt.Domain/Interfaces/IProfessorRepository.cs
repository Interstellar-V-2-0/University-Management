using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces;

public interface IProfessorRepository : IRepository<Professor>
{
    Task<Professor?> GetByEmailAsync(string email);
}