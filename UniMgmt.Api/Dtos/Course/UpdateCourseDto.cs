namespace UniMgmt.Api.Dtos.Course;

public class UpdateCourseDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ProfessorId { get; set; }
}