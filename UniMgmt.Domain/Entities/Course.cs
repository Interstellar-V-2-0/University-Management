namespace UniMgmt.Domain.Entities;

public class Course
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string Description {get; set;}
    
    public int ProfessorId {get; set;}
    public Professor Professor {get; set;}

    public List<Section> Sections { get; set; } = new List<Section>();
}