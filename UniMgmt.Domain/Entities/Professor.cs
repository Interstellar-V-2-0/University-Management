namespace UniMgmt.Domain.Entities;

public class Professor : Person
{
    public int Id { get; set; }
    public string specialty {get; set;}

    public List<Course> Courses { get; set; } = new List<Course>();
}