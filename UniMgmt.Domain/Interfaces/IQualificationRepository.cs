using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces;

public interface IQualificationRepository : IRepository<Qualification>
{
    Task<Qualification?> GetByInscriptionIdAsync(int inscriptionId);
}