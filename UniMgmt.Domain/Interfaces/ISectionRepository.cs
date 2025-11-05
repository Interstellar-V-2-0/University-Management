using UniMgmt.Domain.Entities;

namespace UniMgmt.Domain.Interfaces;

public interface ISectionRepository : IRepository<Section>
{
    Task<int> GetCurrentEnrollmentCountAsync(int sectionId);
    Task<int> GetMaxCapacityAsync(int sectionId);
}