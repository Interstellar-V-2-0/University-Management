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
        // we verify tha student and section exist
        if (!await _studentRepository.GetByIdAsync(studentId).ContinueWith(t => t.Result != null))
            throw new InvalidOperationException($"Student with ID {studentId} not found.");

        if (!await _sectionRepository.GetByIdAsync(sectionId).ContinueWith(t => t.Result != null))
            throw new InvalidOperationException($"Section with ID {sectionId} not found.");

        // verify if a student is already enrolled in a section
        if (await _inscriptionRepository.AnyEnrollmentAsync(sectionId, studentId))
            throw new InvalidOperationException("Student is already enrolled in this section.");

        // verify the section's places availability
        if (!await IsSectionAvailableAsync(sectionId))
            throw new InvalidOperationException("Section has reached its maximum capacity.");

        // create and saved a section
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
}