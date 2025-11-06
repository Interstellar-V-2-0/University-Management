namespace UniMgmt.Api.Dtos.Professor;

public class ProfessorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Specialty { get; set; } = null!;
}