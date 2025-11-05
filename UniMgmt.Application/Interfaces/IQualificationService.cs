using UniMgmt.Domain.Entities;

namespace UniMgmt.Application.Interfaces;

public interface IQualificationService : IService<Qualification>
{
    Task<Qualification> SetQualificationAsync(int inscriptionId, double note, string observations);
    Task<Qualification?> GetQualificationByInscriptionIdAsync(int inscriptionId);
}