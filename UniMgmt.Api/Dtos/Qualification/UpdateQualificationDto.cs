namespace UniMgmt.Api.Dtos.Qualification;

public class UpdateQualificationDto
{
    public double Note { get; set; }
    public string Observations { get; set; } = null!;
}