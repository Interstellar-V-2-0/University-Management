using UniMgmt.Domain.Entities;

namespace UniMgmt.Application.Interfaces;

public interface IInscriptionService: IService<Inscription>
{
    Task<Inscription> EnrollStudentAsync(int studentId, int sectionId);
    Task<bool> IsSectionAvailableAsync(int sectionId);
    Task<IEnumerable<Inscription>> GetInscriptionsByStudentIdAsync(int studentId);
}