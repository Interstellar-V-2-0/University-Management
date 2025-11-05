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
        // grade validation, I wanted to set it between 0 and 5,0
        if (note < 0 || note > 5.0)
            throw new ArgumentOutOfRangeException(nameof(note), "Note must be between 0 and 5.0.");

        // so we first verify the existing inscription
        if (!await _inscriptionRepository.GetByIdAsync(inscriptionId).ContinueWith(t => t.Result != null))
            throw new InvalidOperationException($"Inscription with ID {inscriptionId} not found.");

        // verify if there's already a grade for this inscription
        var qualification = await _qualificationRepository.GetByInscriptionIdAsync(inscriptionId);

        if (qualification == null)
        {
            // Ana Here we create a new grade
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
            // Guys here we update the existing grade
            qualification.Note = note;
            qualification.Observations = observations;
            return await _qualificationRepository.UpdateAsync(qualification);
        }
    }
    
    public async Task<Qualification?> GetQualificationByInscriptionIdAsync(int inscriptionId)
    {
        return await _qualificationRepository.GetByInscriptionIdAsync(inscriptionId);
    }
}