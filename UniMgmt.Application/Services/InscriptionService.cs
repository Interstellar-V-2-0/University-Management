using UniMgmt.Application.Interfaces;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;

namespace UniMgmt.Application.Services;

public class InscriptionService : Service<Inscription>, IInscriptionService
{
    private readonly IInscriptionRepository _inscriptionRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly IStudentRepository _studentRepository;

    public InscriptionService(
        IInscriptionRepository inscriptionRepository,
        ISectionRepository sectionRepository,
        IStudentRepository studentRepository)
        : base(inscriptionRepository)
    {
        _inscriptionRepository = inscriptionRepository;
        _sectionRepository = sectionRepository;
        _studentRepository = studentRepository;
    }

    public async Task<Inscription> EnrollStudentAsync(int studentId, int sectionId)
    {
        // verify that the student and section exist
        if (!await _studentRepository.GetByIdAsync(studentId).ContinueWith(t => t.Result != null))
            throw new InvalidOperationException($"Student with ID {studentId} not found.");

        if (!await _sectionRepository.GetByIdAsync(sectionId).ContinueWith(t => t.Result != null))
            throw new InvalidOperationException($"Section with ID {sectionId} not found.");

        // verify if the student is already enrolled in this section
        if (await _inscriptionRepository.AnyEnrollmentAsync(sectionId, studentId))
            throw new InvalidOperationException("Student is already enrolled in this section.");

        // verify the section's capacity availability
        if (!await IsSectionAvailableAsync(sectionId))
            throw new InvalidOperationException("Section has reached its maximum capacity.");

        // NEW: verify that the student is not enrolled in another section with overlapping schedule
        if (await HasScheduleConflictAsync(studentId, sectionId))
            throw new InvalidOperationException("Student is already enrolled in a section with overlapping schedule.");

        // create and save the inscription
        var newInscription = new Inscription
        {
            StudentId = studentId,
            SectionId = sectionId,
            RegistrationDate = DateTime.Now
        };

        return await _inscriptionRepository.CreateAsync(newInscription);
    }

    public async Task<bool> IsSectionAvailableAsync(int sectionId)
    {
        var currentEnrollment = await _sectionRepository.GetCurrentEnrollmentCountAsync(sectionId);
        var maxCapacity = await _sectionRepository.GetMaxCapacityAsync(sectionId);

        return currentEnrollment < maxCapacity;
    }

    public async Task<IEnumerable<Inscription>> GetInscriptionsByStudentIdAsync(int studentId)
    {
        return await _inscriptionRepository.GetInscriptionsByStudentIdAsync(studentId);
    }

    // NEW: helper method to check schedule conflicts for a student
    private async Task<bool> HasScheduleConflictAsync(int studentId, int targetSectionId)
    {
        var targetSection = await _sectionRepository.GetByIdAsync(targetSectionId);
        if (targetSection == null) return false;

        var studentInscriptions = await _inscriptionRepository.GetInscriptionsByStudentIdAsync(studentId);

        foreach (var inscription in studentInscriptions)
        {
            var existingSection = inscription.Section;
            if (existingSection == null) continue;

            // compare day and time overlap
            bool sameDay = existingSection.DaySection == targetSection.DaySection;
            bool overlap = targetSection.StarTime < existingSection.EndTime &&
                           existingSection.StarTime < targetSection.EndTime;

            if (sameDay && overlap)
                return true;
        }

        return false;
    }
}
