namespace UniMgmt.Domain.Entities;

public class Student : Person
{
    public int Id {get; set;}

    public List<Inscription> Inscriptions { get; set; } = new List<Inscription>();
}