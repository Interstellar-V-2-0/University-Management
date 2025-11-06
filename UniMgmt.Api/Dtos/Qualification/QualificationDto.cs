namespace UniMgmt.Api.Dtos.Qualification;

public class QualificationDto
{
    public int Id { get; set; }
    public int InscriptionId { get; set; }
    public double Note { get; set; }
    public string Observations { get; set; } = null!;
}