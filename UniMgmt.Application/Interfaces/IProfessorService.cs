using UniMgmt.Domain.Entities;

namespace UniMgmt.Application.Interfaces;

public interface IProfessorService : IService<Professor>
{
    Task<Professor?> GetProfessorByEmailAsync(string email);
}