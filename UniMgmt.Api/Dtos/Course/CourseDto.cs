namespace UniMgmt.Api.Dtos.Course;

public class CourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProfessorId { get; set; }
}