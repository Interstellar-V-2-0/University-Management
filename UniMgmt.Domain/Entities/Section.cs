namespace UniMgmt.Domain.Entities;

public class Section
{
    public int Id {get; set;}
    
    public int CourseId {get; set;}
    public Course? Course {get; set;}
    
    public string DaySection {get; set;}
    public TimeSpan StarTime {get; set;}
    public TimeSpan EndTime {get; set;}
    public string Classroom {get; set;}
    public int CapacityMax {get; set;}

    public List<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}