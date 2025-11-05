namespace UniMgmt.Api.Dtos.Section;

public class SectionDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string DaySection { get; set; } = null!;
    public TimeSpan StarTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Classroom { get; set; } = null!;
    public int CapacityMax { get; set; }
}