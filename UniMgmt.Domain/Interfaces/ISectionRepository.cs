using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<int> GetCurrentEnrollmentCountAsync(int sectionId);
        Task<int> GetMaxCapacityAsync(int sectionId);

        // Nuevo: validar conflictos de horario (día y hora)
        Task<bool> HasScheduleConflictAsync(string daySection, TimeSpan startTime, TimeSpan endTime, int? excludeSectionId = null);

        // Nuevo: obtener sección con detalles (curso, inscripciones)
        Task<Section?> GetByIdWithDetailsAsync(int sectionId);
    }
}