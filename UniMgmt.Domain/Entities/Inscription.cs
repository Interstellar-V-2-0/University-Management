namespace UniMgmt.Domain.Entities;

public class Inscription
{
    public int Id {get; set;}
    
    public int StudentId {get; set;}
    public Student Student {get; set;}
    
    public int SectionId {get; set;}
    public Section Section {get; set;}
    
    public DateTime RegistrationDate  {get; set;}
    
    public Qualification  Qualification {get; set;}
}