namespace UniMgmt.Domain.Entities;

public class Qualification
{
    public int Id {get; set;}
    
    public int InscriptionId {get; set;}
    public Inscription? Inscription {get; set;}
    
    public double Note {get; set;}
    public string Observations  {get; set;}
}