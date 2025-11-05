namespace UniMgmt.Api.Dtos.Inscription;

public class InscriptionDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SectionId { get; set; }
    public DateTime RegistrationDate { get; set; }
}