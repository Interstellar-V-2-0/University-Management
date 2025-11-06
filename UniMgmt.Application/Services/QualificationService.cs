using UniMgmt.Application.Interfaces;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;

namespace UniMgmt.Application.Services;

public class QualificationService : Service<Qualification>, IQualificationService
{
    private readonly IQualificationRepository _qualificationRepository;
    private readonly IInscriptionRepository _inscriptionRepository;

    public QualificationService(
        IQualificationRepository qualificationRepository,
        IInscriptionRepository inscriptionRepository)
        : base(qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
        _inscriptionRepository = inscriptionRepository;
    }

    public async Task<Qualification> SetQualificationAsync(int inscriptionId, double note, string observations)
    {
        // grade validation between 0.0 and 5.0
        if (note < 0 || note > 5.0)
            throw new ArgumentOutOfRangeException(nameof(note), "Note must be between 0 and 5.0.");

        // verify that the inscription exists
        if (!await _inscriptionRepository.GetByIdAsync(inscriptionId).ContinueWith(t => t.Result != null))
            throw new InvalidOperationException($"Inscription with ID {inscriptionId} not found.");

        // check if a qualification already exists for this inscription
        var qualification = await _qualificationRepository.GetByInscriptionIdAsync(inscriptionId);

        if (qualification == null)
        {
            // create a new qualification
            qualification = new Qualification
            {
                InscriptionId = inscriptionId,
                Note = note,
                Observations = observations
            };
            return await _qualificationRepository.CreateAsync(qualification);
        }
        else
        {
            // update existing qualification
            qualification.Note = note;
            qualification.Observations = observations;
            return await _qualificationRepository.UpdateAsync(qualification);
        }
    }

    public async Task<Qualification?> GetQualificationByInscriptionIdAsync(int inscriptionId)
    {
        return await _qualificationRepository.GetByInscriptionIdAsync(inscriptionId);
    }

    // NEW: calculate the average grade for a course
    public async Task<double> GetAverageByCourseAsync(int courseId)
    {
        var allQualifications = await _qualificationRepository.GetAllAsync();

        var courseGrades = allQualifications
            .Where(q => q.Inscription != null &&
                        q.Inscription.Section != null &&
                        q.Inscription.Section.CourseId == courseId)
            .Select(q => q.Note)
            .ToList();

        if (!courseGrades.Any())
            return 0.0;

        return courseGrades.Average();
    }
}
