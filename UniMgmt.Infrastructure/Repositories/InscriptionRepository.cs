using Microsoft.EntityFrameworkCore;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;
using UniMgmt.Infrastructure.Data;

namespace UniMgmt.Infrastructure.Repositories
{
    public class InscriptionRepository : Repository<Inscription>, IInscriptionRepository
    {
        private readonly AppDbContext _context;

        public InscriptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AnySectionByIdAsync(int sectionId)
        {
            return await _context.Sections.AnyAsync(s => s.Id == sectionId);
        }

        public async Task<bool> AnyStudentByIdAsync(int studentId)
        {
            return await _context.Students.AnyAsync(s => s.Id == studentId);
        }

        public async Task<bool> AnyEnrollmentAsync(int sectionId, int studentId)
        {
            return await _context.Inscriptions
                .AnyAsync(i => i.SectionId == sectionId && i.StudentId == studentId);
        }

        public async Task<IEnumerable<Inscription>> GetInscriptionsByStudentIdAsync(int studentId)
        {
            return await _context.Inscriptions
                .Include(i => i.Section)
                    .ThenInclude(s => s.Course)
                .Include(i => i.Qualification)
                .Where(i => i.StudentId == studentId)
                .AsNoTracking()
                .ToListAsync();
        }

        // Devuelve una inscripción con todos los detalles relacionados
        public async Task<Inscription?> GetByIdWithDetailsAsync(int inscriptionId)
        {
            return await _context.Inscriptions
                .Include(i => i.Student)
                .Include(i => i.Section)
                    .ThenInclude(s => s.Course)
                .Include(i => i.Qualification)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == inscriptionId);
        }

        // Nuevo método implementado: obtiene todas las secciones en las que está inscrito un estudiante
        public async Task<IEnumerable<Section>> GetSectionsByStudentAsync(int studentId)
        {
            return await _context.Inscriptions
                .Where(i => i.StudentId == studentId)
                .Include(i => i.Section)
                    .ThenInclude(s => s.Course)
                .Select(i => i.Section)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
