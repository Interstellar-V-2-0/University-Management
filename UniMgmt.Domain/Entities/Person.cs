namespace UniMgmt.Domain.Entities;

public abstract class Person
{
    public int Id { get; }
    public string Name {get; set;}
    public string LastName {get; set;}
    public string Email {get; set;}
    public string PhoneNumber {get; set;}
    
}