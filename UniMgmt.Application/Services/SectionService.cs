using UniMgmt.Application.Interfaces;
using UniMgmt.Domain.Entities;
using UniMgmt.Domain.Interfaces;

namespace UniMgmt.Application.Services;

public class SectionService : Service<Section>, ISectionService
{
    private readonly ISectionRepository _sectionRepository;

    public SectionService(ISectionRepository sectionRepository) : base(sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<int> GetRemainingCapacityAsync(int sectionId)
    {
        var maxCapacity = await _sectionRepository.GetMaxCapacityAsync(sectionId);
        var currentEnrollment = await _sectionRepository.GetCurrentEnrollmentCountAsync(sectionId);

        return Math.Max(0, maxCapacity - currentEnrollment);
    }
}