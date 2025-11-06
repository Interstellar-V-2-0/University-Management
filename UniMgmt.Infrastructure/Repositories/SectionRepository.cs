using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        private readonly AppDbContext _context;

        public SectionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetCurrentEnrollmentCountAsync(int sectionId)
        {
            return await _context.Inscriptions
                .CountAsync(i => i.SectionId == sectionId);
        }

        public async Task<int> GetMaxCapacityAsync(int sectionId)
        {
            var section = await _context.Sections
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == sectionId);

            return section?.CapacityMax ?? 0;
        }

        // Verifica si existe conflicto de horario con otra sección
        public async Task<bool> HasScheduleConflictAsync(string daySection, TimeSpan startTime, TimeSpan endTime, int? excludeSectionId = null)
        {
            return await _context.Sections
                .AnyAsync(s =>
                    s.DaySection == daySection &&
                    ((startTime >= s.StarTime && startTime < s.EndTime) ||
                     (endTime > s.StarTime && endTime <= s.EndTime) ||
                     (startTime <= s.StarTime && endTime >= s.EndTime)) &&
                    (!excludeSectionId.HasValue || s.Id != excludeSectionId.Value));
        }

        // Obtiene una sección con detalles del curso e inscripciones
        public async Task<Section?> GetByIdWithDetailsAsync(int sectionId)
        {
            return await _context.Sections
                .Include(s => s.Course)
                .Include(s => s.Inscriptions)
                    .ThenInclude(i => i.Student)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == sectionId);
        }
    }
}
