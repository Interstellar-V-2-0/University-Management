using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories
{
    public class QualificationRepository : Repository<Qualification>, IQualificationRepository
    {
        private readonly AppDbContext _context;

        public QualificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Qualification?> GetByInscriptionIdAsync(int inscriptionId)
        {
            return await _context.Qualifications
                .Include(q => q.Inscription)
                    .ThenInclude(i => i.Student)
                .Include(q => q.Inscription)
                    .ThenInclude(i => i.Section)
                        .ThenInclude(s => s.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.InscriptionId == inscriptionId);
        }

        // Obtiene todas las calificaciones con sus relaciones
        public async Task<IEnumerable<Qualification>> GetAllWithDetailsAsync()
        {
            return await _context.Qualifications
                .Include(q => q.Inscription)
                    .ThenInclude(i => i.Student)
                .Include(q => q.Inscription)
                    .ThenInclude(i => i.Section)
                        .ThenInclude(s => s.Course)
                .AsNoTracking()
                .ToListAsync();
        }

        // Implementación requerida por la interfaz: calificaciones por curso
        public async Task<IEnumerable<Qualification>> GetQualificationsByCourseIdAsync(int courseId)
        {
            return await _context.Qualifications
                .Include(q => q.Inscription)
                    .ThenInclude(i => i.Section)
                        .ThenInclude(s => s.Course)
                .Include(q => q.Inscription)
                    .ThenInclude(i => i.Student)
                .Where(q => q.Inscription != null && q.Inscription.Section != null && q.Inscription.Section.CourseId == courseId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
