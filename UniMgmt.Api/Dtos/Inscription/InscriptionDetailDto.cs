namespace UniMgmt.Api.Dtos.Inscription;

public class InscriptionDetailDto
{
    public int Id { get; set; }
    public string StudentName { get; set; } = null!;
    public string StudentLastName { get; set; } = null!;
    public string CourseName { get; set; } = null!;
    public string SectionDay { get; set; } = null!;
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Classroom { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
}