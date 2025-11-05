using UniMgmt.Application.Interfaces;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;

namespace UniMgmt.Application.Services;

public class ProfessorService : Service<Professor>, IProfessorService
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessorService(IProfessorRepository professorRepository) : base(professorRepository)
    {
        _professorRepository = professorRepository;
    }

    public async Task<Professor?> GetProfessorByEmailAsync(string email)
    {
        return await _professorRepository.GetByEmailAsync(email);
    }
}