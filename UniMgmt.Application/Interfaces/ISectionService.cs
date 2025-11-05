using System.Collections.Specialized;
using UniMgmt.Domain.Entities;

namespace UniMgmt.Application.Interfaces;

public interface ISectionService : IService<Section>
{
    Task<int> GetRemainingCapacityAsync(int sectionId);
}