namespace UniMgmt.Api.Dtos.Course;

public class CreateCourseDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProfessorId { get; set; }
}