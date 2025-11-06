using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces
{
    public interface IInscriptionRepository : IRepository<Inscription>
    {
        Task<bool> AnySectionByIdAsync(int sectionId);
        Task<bool> AnyStudentByIdAsync(int studentId);
        Task<bool> AnyEnrollmentAsync(int sectionId, int studentId);
        Task<IEnumerable<Inscription>> GetInscriptionsByStudentIdAsync(int studentId);

        // Nuevo: obtener secciones donde está inscrito un estudiante (para validar choques de horario)
        Task<IEnumerable<Section>> GetSectionsByStudentAsync(int studentId);

        // Nuevo: obtener inscripción con detalles (student, section, qualification)
        Task<Inscription?> GetByIdWithDetailsAsync(int inscriptionId);
    }
}