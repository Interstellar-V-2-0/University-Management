using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces
{
    public interface IQualificationRepository : IRepository<Qualification>
    {
        Task<Qualification?> GetByInscriptionIdAsync(int inscriptionId);

        // Nuevo: obtener todas las calificaciones asociadas a un curso (para calcular promedios)
        Task<IEnumerable<Qualification>> GetQualificationsByCourseIdAsync(int courseId);
    }
}