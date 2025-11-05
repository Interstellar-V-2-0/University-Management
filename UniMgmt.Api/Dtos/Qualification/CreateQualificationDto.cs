namespace UniMgmt.Api.Dtos.Qualification;

public class CreateQualificationDto
{
    public int InscriptionId { get; set; }
    public double Note { get; set; }
    public string Observations { get; set; } = null!;
}