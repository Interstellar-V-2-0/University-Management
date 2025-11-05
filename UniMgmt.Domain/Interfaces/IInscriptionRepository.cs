using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces;


public interface IInscriptionRepository : IRepository<Inscription>
{
    Task<bool> AnySectionByIdAsync(int sectionId);
    Task<bool> AnyStudentByIdAsync(int studentId);
    Task<bool> AnyEnrollmentAsync(int sectionId, int studentId);
    Task<IEnumerable<Inscription>> GetInscriptionsByStudentIdAsync(int studentId);
}